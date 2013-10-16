using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Crosscutting.Security.Model;

    using Web.Utility;

    public class AppController : Controller
    {
        public SysUser UserData
        {
            get
            {
                var us = HttpContext.User as MyFormsPrincipal<SysUser>;
             
                return us != null ? us.UserData : null;
            }
        }

        /// <summary>
        /// 创建记录信息
        /// </summary>
        public string CreateRecordMsg
        {
            get
            {
                return string.Format("创建时间：{0},创建人：{1}", DateTime.Now, UserData.UserName);
            }
        }

        /// <summary>
        /// 修改记录信息
        /// </summary>
        public string UpdateRecordMsg
        {
            get
            {
                return string.Format("修改时间：{0},修改人：{1}", DateTime.Now, UserData.UserName);
            }
        }


        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">行为</param>
        /// <returns>JsonReuslt</returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 返回JsonResult.24         
         /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult42         
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">数据格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }
    }
}
