using BookstoreApp.Models;
using BookstoreApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BookstoreApp.Controllers
{
    public class ServiceAPIController : ApiController
    {
        private string HostingPrefix = Convert.ToString(ConfigurationManager.AppSettings["hostingPrefix"], CultureInfo.InvariantCulture);
        private string BookImagePath = ConfigurationManager.AppSettings["BookImage"];
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To get book list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult BookList()
        {
            var _service = new BookService();
            var objService = new ServiceResponseModel<List<BookListModel>>();
            try
            {
                var getBookList = _service.GetBookList();
                if (getBookList.Count() > 0)
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.SuccessResponse, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                    objService.Data = getBookList;
                }
                else
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.NoRecord, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                    objService.Data = null;
                }
            }
            catch (Exception ex)
            {
                objService.Message = ex.Message;
                objService.Response = Convert.ToInt32(ServiceResponse.ExeptionResponse, CultureInfo.CurrentCulture);
                objService.Data = null;
            }

            return Ok(objService);
        }

        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To save or update book
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SaveBook(BookListModel obj)
        {
            var _service = new BookService();
            var objService = new ServiceResponseModel<string>();
            string ImagePath = string.Empty;
            string ImageUniqueName = string.Empty;
            string url = string.Empty;
            string ImageURL = string.Empty; 
            try
            {
                if (obj != null)
                {
                    if (obj.BookPic != null)
                    {
                        ImagePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BookImage"].ToString());
                        ImageUniqueName = obj.BookName+"_"+DateTime.Now.Ticks.ToString();
                        url = HostingPrefix + BookImagePath.Replace("~", "") + ImageUniqueName + ".jpg";
                        ImageURL = ImageUniqueName + ".jpg";
                        if (!Directory.Exists(ImagePath))
                        {
                            Directory.CreateDirectory(ImagePath);
                        }
                        var ImageLocation = ImagePath + ImageURL;
                        var convertbase = obj.BookPic.Replace("data:image/jpeg;base64,", "");
                        convertbase = obj.BookPic.Replace("data:image/png;base64,", "");
                        //Save the image to directory
                        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(convertbase)))
                        {
                            using (Bitmap bm2 = new Bitmap(ms))
                            {
                                bm2.Save(ImageLocation);
                                obj.BookPic = ImageURL;
                            }
                        }
                        //string ImageName = obj.BookName + "_" + DateTime.Now.Ticks.ToString() + "_" + obj.BookPicImage.FileName.ToString();
                        //_service.UploadImage(obj.BookPicImage, HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BookImage"]), ImageName);
                        //obj.BookPic = ImageName;
                    }
                    var save = _service.SaveBookData(obj);

                    if (save)
                    {
                        objService.Response = Convert.ToInt32(ServiceResponse.SuccessResponse, CultureInfo.CurrentCulture);
                        objService.Message = CommonMessage.Success();
                    }
                    else
                    {
                        objService.Response = Convert.ToInt32(ServiceResponse.NoRecord, CultureInfo.CurrentCulture);
                        objService.Message = CommonMessage.Success();
                    }
                }
            }
            catch (Exception ex)
            {
                objService.Message = ex.Message;
                objService.Response = Convert.ToInt32(ServiceResponse.ExeptionResponse, CultureInfo.CurrentCulture);
                objService.Data = null;
            }

            return Ok(objService);
        }

        /// <summary>
        /// Created By  :Ashwajit Bansod
        /// Created For : To soft delete book
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteBook(long BookId)
        {
            var _service = new BookService();
            var objService = new ServiceResponseModel<string>();
            try
            {
               var  isDeleted = _service.DeleteBookById(BookId);
                if (isDeleted)
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.SuccessResponse, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                }
                else
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.NoRecord, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                }
            }
            catch (Exception ex)
            {
                objService.Message = ex.Message;
                objService.Response = Convert.ToInt32(ServiceResponse.ExeptionResponse, CultureInfo.CurrentCulture);
                objService.Data = null;
            }

            return Ok(objService);
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To get details of book
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDetails(long BookId)
        {
            var _service = new BookService();
            var objService = new ServiceResponseModel<BookListModel>();
            try
            {
                var details = _service.GetDetailsById(BookId);
                if (details != null)
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.SuccessResponse, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                    objService.Data = details;
                }
                else
                {
                    objService.Response = Convert.ToInt32(ServiceResponse.NoRecord, CultureInfo.CurrentCulture);
                    objService.Message = CommonMessage.Success();
                    objService.Data = null;
                }
            }
            catch (Exception ex)
            {
                objService.Message = ex.Message;
                objService.Response = Convert.ToInt32(ServiceResponse.ExeptionResponse, CultureInfo.CurrentCulture);
                objService.Data = null;
            }

            return Ok(objService);
        }
    }
}
