using BookstoreApp.Models;
using BookstoreApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookstoreApp.Controllers
{
    public class BookStoreController : Controller
    {
        private string HostingPrefix = Convert.ToString(ConfigurationManager.AppSettings["hostingPrefix"], CultureInfo.InvariantCulture);
        private string BookImagePath = ConfigurationManager.AppSettings["BookImage"];
        // GET: BookStore
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBookList()
        {
            return View();
        }
        /// <summary>
        /// Craeted By : Ashwajit Bansod
        /// Created For : To get list of books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBookListData()
        {
            var _service = new BookService();
            try
            {
                var getBookList = _service.GetBookList();
                return Json(getBookList, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created Date : To view book adding form
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBook()
        {
            return View(new BookListModel());
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : TO save and update book data by id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBookData(BookListModel obj)
        {
            var _service = new BookService();
            try
            {
                if (obj != null)
                {
                    if (obj.BookPicImage != null)
                    {
                        string ImageName = obj.BookName + "_" + DateTime.Now.Ticks.ToString() + "_" + obj.BookPicImage.FileName.ToString();
                        _service.UploadImage(obj.BookPicImage, Server.MapPath(ConfigurationManager.AppSettings["BookImage"]), ImageName);
                        obj.BookPic = ImageName;
                    }
                    var save = _service.SaveBookData(obj);
                    if (save)
                        return View("GetBookList");
                    else
                        return RedirectToAction("AddBook");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View("GetBookList");
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : TO get book details to view 
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public ActionResult GetBookDataById(long BookId)
        {
            var details = new BookListModel();
            var _service = new BookService();
            try
            {
                if(BookId > 0)
                {
                    details = _service.GetDetailsById(BookId);
                    return View("BookDetails", details);
                }
                else
                {
                    return Json(false,JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View("BookDetails", details);
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To delete book record by id
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteBookData(long BookId)
        {
            var _service = new BookService();
            bool isDeleted = false;
            try
            {
                if (BookId > 0)
                {
                     isDeleted = _service.DeleteBookById(BookId);
                    return Json(isDeleted, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(isDeleted, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To get details for update
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public ActionResult GetDetailsForEdit(long BookId)
        {
            var _service = new BookService();
            try
            {
                if (BookId > 0)
                {
                    var details = _service.GetDetailsById(BookId);
                    return View("AddBook", details);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}