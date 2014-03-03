/*
 *名称：AsposeReplaceHelper
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-01-16 17:57:30
 *修改时间：
 *备注：
 */

using System;

namespace Web.Utility
{
    using System.Collections;
    using System.Text.RegularExpressions;

    using Aspose.Words;

    public class AsposeReplaceHelper
    {
        public AsposeReplaceHelper(Document doc)
        {
            mDoc = doc;
        }

        /// <summary>
        /// Replaces old text with new. New text can contain any special characters. Old text cannot contain special charactes.
        /// </summary>
        /// <param name="oldText"></param>
        /// <param name="newText"></param>
        public void Replace(string oldText, string newText)
        {
            mDoc.Range.Replace(new Regex(Regex.Escape(oldText)), new ReplaceEvaluatorFindAndInsertText(newText), false);
        }

        private class ReplaceEvaluatorFindAndInsertText : IReplacingCallback
        {
            public ReplaceEvaluatorFindAndInsertText(string text)
            {
                mText = text;
            }

            /// <summary>
            /// This method is called by the Aspose.Words find and replace engine for each match.
            /// This method replaces the match string, even if it spans multiple runs.
            /// </summary>
            ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
            {
                // This is a Run node that contains either the beginning or the complete match.
                Node currentNode = e.MatchNode;

                // The first (and may be the only) run can contain text before the match,
                // in this case it is necessary to split the run.
                if (e.MatchOffset > 0)
                    currentNode = SplitRun((Run)currentNode, e.MatchOffset);

                // This array is used to store all nodes of the match for further removing.
                ArrayList runs = new ArrayList();

                // Find all runs that contain parts of the match string.
                int remainingLength = e.Match.Value.Length;
                while (
                    (remainingLength > 0) &&
                    (currentNode != null) &&
                    (currentNode.GetText().Length <= remainingLength))
                {
                    runs.Add(currentNode);
                    remainingLength = remainingLength - currentNode.GetText().Length;

                    // Select the next Run node.
                    // Have to loop because there could be other nodes such as BookmarkStart etc.
                    do
                    {
                        currentNode = currentNode.NextSibling;
                    }
                    while ((currentNode != null) && (currentNode.NodeType != NodeType.Run));
                }

                // Split the last run that contains the match if there is any text left.
                if ((currentNode != null) && (remainingLength > 0))
                {
                    SplitRun((Run)currentNode, remainingLength);
                    runs.Add(currentNode);
                }

                // Create Document Buidler and insert text.
                DocumentBuilder builder = new DocumentBuilder(e.MatchNode.Document as Document);
                builder.MoveTo((Run)runs[runs.Count - 1]);
                builder.Write(mText);

                // Now remove all runs in the sequence.
                foreach (Run run in runs)
                    run.Remove();

                // Signal to the replace engine to do nothing because we have already done all what we wanted.
                return ReplaceAction.Skip;
            }

            /// <summary>
            /// Splits text of the specified run into two runs.
            /// Inserts the new run just after the specified run.
            /// </summary>
            private static Run SplitRun(Run run, int position)
            {
                Run afterRun = (Run)run.Clone(true);
                afterRun.Text = run.Text.Substring(position);
                run.Text = run.Text.Substring(0, position);
                run.ParentNode.InsertAfter(afterRun, run);
                return afterRun;
            }

            private string mText;
        }

        private Document mDoc; 
    }
}