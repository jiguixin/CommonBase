/*
 *名称：TreeGridResult
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-15 09:27:59
 *修改时间：
 *备注：
 */

using System;

namespace Web.Models.EasyUi
{
    public class TreeGridResult
    {
        public string id { get; set; }

        public string text { get; set; }

        public string state { get; set; }

        public string iconCls { get; set; }

        public string link { get; set; }

        public int? order { get; set; }

        public TreeGridResult[] children { get; set; }  
    }
}