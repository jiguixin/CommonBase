/*
 *名称：EasyUiTreeResult
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-26 16:10:21
 *修改时间：
 *备注：
 */

using System;

namespace Web.Models
{
    public class EasyUiTreeResult
    {
        public string id { get; set; }

        public string text { get; set; }
         
        public string state { get; set; }

        public string iconCls { get; set; }

        public  bool  @checked{get;set;}

        public EasyUiTreeResult[] children { get; set; }  
    }
}