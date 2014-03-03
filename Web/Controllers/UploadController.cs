using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Services;
using Ninject.Activation;
using Web.Models;

namespace Web.Controllers
{
    using Infrastructure.Crosscutting.Security.Common;

    public class UploadController : Controller
    {
        private SysFileService fileService = ServiceFactory.FileService;
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// uploadify控件上传文件
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile(HttpPostedFileBase fileData, string sysId)
        {
            if (fileData != null)
            {
                if (sysId == null)
                {
                    sysId = Util.NewId();
                }
                try
                {
                    ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    ControllerContext.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    ControllerContext.HttpContext.Response.Charset = "UTF-8";

                    string filePath = fileData.FileName;
                    string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    byte[] mydata = new byte[fileData.ContentLength + 1];
                    fileData.InputStream.Read(mydata, 0, (int)fileData.ContentLength);

                    SysFile sysFile = new SysFile()
                        {
                            RelID = sysId,
                            FileName = fileName,
                            FileSize = mydata.Length / (1024 * 1024.0),
                            CreateDT = DateTime.Now,
                            ModifyDT = DateTime.Now,
                            FileData = mydata
                        };
//
                    //if (fileService.FileRepository.Add(sysFile) > 0)
                    //{
                    //    return Json("上传成功");
                    //}
                    //else
                    //{
                    //    return Json("上传失败");
                    //}
                    bool result = fileService.UploadFile(sysFile);

                    if (result)
                    {
                        return Json("上传成功");
                    }
                    else
                    {
                        return Json("上传失败");
                    }

                }
                catch (Exception ex)
                {
                    return Json("上传失败，" + ex.Message);
                }

            }
            else
            {
                return Json("上传失败,文件为空");
            } 
        }


        /// <summary>
        /// 上传文件至oracle数据库
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public JsonResult Upload(SysFile sysFile)
        {
            bool result = fileService.UploadFile(sysFile);
            return Json(result);
        }

        /// <summary>
        /// 修改附件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sysId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyFile(HttpPostedFileBase fileData, string sysId)
        {
            if (fileData != null)
            {
                try
                {
                    ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    ControllerContext.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    ControllerContext.HttpContext.Response.Charset = "UTF-8";

                    string filePath = fileData.FileName;
                    string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    byte[] mydata = new byte[fileData.ContentLength + 1];
                    fileData.InputStream.Read(mydata, 0, (int)fileData.ContentLength);

                    SysFile sysFile = fileService.GetFile(sysId);

                    sysFile.FileSize = mydata.Length / (1024 * 1024.0);
                    sysFile.ModifyDT = DateTime.Now;
                    sysFile.FileData = mydata;
                    sysFile.FileName = fileName;

                    bool result = fileService.ModifyFile(sysFile);

                    if (result)
                    {
                        return Json("修改成功");
                    }
                    else
                    {
                        return Json("修改失败");
                    }

                }
                catch (Exception ex)
                {
                    return Json("修改失败，" + ex.Message);
                }

            }
            else
            {
                return Json("修改失败,文件为空");
            }

        }

        /// <summary>
        /// 获取所有附件
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFiles()
        {
            var result = fileService.GetFiles().OrderBy(x => x.ModifyDT);
            return Json(result);
        }

        /// <summary>
        /// 根据项目编号获取附件列表
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public JsonResult GetFilesByRelId(string relId)
        {
            var files = fileService.GetFilesByRelID(relId);
            List<EasyUiTreeResult> lstResult = new List<EasyUiTreeResult>();
            if (files!=null&&files.Any())
            {
                foreach (SysFile file in files)
                {
                    lstResult.Add(new EasyUiTreeResult()
                        {
                            id = file.SysId,
                            text = file.FileName
                        });
                }
            }
            return Json(lstResult);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public JsonResult DeleteFile(string sysId)
        {
            var result = fileService.DeleteFile(sysId);
            return Json(result);
        }

        /// <summary>
        /// mvc下载文件
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public ActionResult DownloadFile(string sysId)
        {
            SysFile sysFile = fileService.GetFile(sysId);
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";
            //解决文件名乱码问题            
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(sysFile.FileName));

            Response.BinaryWrite(sysFile.FileData);
            Response.Flush();
            Response.End();

            return new EmptyResult();
        }

    }
}
