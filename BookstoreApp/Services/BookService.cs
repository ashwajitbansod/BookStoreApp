using BookstoreApp.Entity;
using BookstoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace BookstoreApp.Services
{
    public class BookService
    {
        BookstoreEntities _db = new BookstoreEntities();
        private string HostingPrefix = Convert.ToString(ConfigurationManager.AppSettings["hostingPrefix"], CultureInfo.InvariantCulture);
        private string BookImagePath = ConfigurationManager.AppSettings["BookImage"];
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To get List of book
        /// </summary>
        /// <returns></returns>
        public List<BookListModel> GetBookList()
        {
            try
            {
                var getList = _db.Tbl_BookStore.Where(x => x.IsDelete == false).Select(x => new BookListModel() { 
                AuthorName = x.AuthorName,
                BookDate = x.BookDate,
                BookDescription = x.BookDescription,
                IsDelete = x.IsDelete,
                BookId = x.BookId,
                BookISBN = x.BookISBN,
                BookName = x.BookName,
                BookPic = x.BookPic == null ? HostingPrefix + BookImagePath.Replace("~", "") + "noImage.jpg" : HostingPrefix + BookImagePath.Replace("~", "") + x.BookPic,
                BookPrice = x.BookPrice,
                BookPublication = x.BookPublication
                }).ToList();
                return getList;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Created By : Ashwajit Bansod 
        /// Created For  :  To save and update book data by using book id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SaveBookData(BookListModel obj)
        {
            bool isSave = false;
            try
            {
                if (obj.BookId == 0)
                {
                    var tableData = new Tbl_BookStore();
                    tableData.BookName = obj.BookName;
                    tableData.BookPic = obj.BookPic;
                    tableData.BookPrice = obj.BookPrice;
                    tableData.BookISBN = obj.BookISBN;
                    tableData.BookPublication = obj.BookPublication;
                    tableData.BookDate = obj.BookDate;
                    tableData.AuthorName = obj.AuthorName;
                    tableData.BookDescription = obj.BookDescription;
                    tableData.IsDelete = false;
                    _db.Tbl_BookStore.Add(tableData);
                    _db.SaveChanges();
                    isSave = true;
                }
                else
                {
                    var getData = _db.Tbl_BookStore.Where(x => x.BookId == obj.BookId).FirstOrDefault();
                    getData.BookName = obj.BookName;
                    getData.BookPic = obj.BookPic;
                    getData.BookPrice = obj.BookPrice;
                    getData.BookISBN = obj.BookISBN;
                    getData.BookPublication = obj.BookPublication;
                    getData.BookDate = obj.BookDate;
                    getData.AuthorName = obj.AuthorName;
                    getData.BookDescription = obj.BookDescription;
                    getData.IsDelete = false;
                    _db.Entry(getData).State = EntityState.Modified;
                    _db.SaveChanges();
                    isSave = true;
                }
            }
            catch(Exception ex)
            {
                isSave = false;
                throw;
            }
            return isSave;
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To get details of book
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public BookListModel GetDetailsById(long BookId)
        {
            try
            {
                var getList = _db.Tbl_BookStore.Where(x => x.BookId == BookId && x.IsDelete == false).Select(x => new BookListModel()
                {
                    AuthorName = x.AuthorName,
                    BookDate = x.BookDate,
                    BookDescription = x.BookDescription,
                    IsDelete = x.IsDelete,
                    BookId = x.BookId,
                    BookISBN = x.BookISBN,
                    BookName = x.BookName,
                    BookPic = x.BookPic == null ? HostingPrefix + BookImagePath.Replace("~", "") + "noImage.jpg" : HostingPrefix + BookImagePath.Replace("~", "") + x.BookPic,
                    BookPrice = x.BookPrice,
                    BookPublication = x.BookPublication
                }).FirstOrDefault();
                return getList;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public bool UploadImage(HttpPostedFileBase myFile, string path, string imageName)
        {
            //message = string.Empty;
            if (myFile != null && myFile.ContentLength != 0)
            {
                if (this.CreateFolderIfNeeded(path))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(path, imageName));
                        return true;

                    }
                    catch (Exception)
                    {

                        return false;
                        //message = ex.Message;
                        throw;
                    }
                }

            }
            return false;
        }
        /// <summary>
        /// Created By : Ashwajit Bansod
        /// Created For : To delete book record by id
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public bool DeleteBookById(long BookId)
        {
            bool isDeleted = false;
            try
            {
                var getDetails = _db.Tbl_BookStore.Where(x => x.BookId == BookId).FirstOrDefault();
                if(getDetails != null)
                {
                    getDetails.IsDelete = true;
                    _db.Entry(getDetails).State = EntityState.Modified;
                    _db.SaveChanges();
                    isDeleted = true;
                }
                else
                    isDeleted = false;
            }
            catch(Exception ex)
            {
                isDeleted = false;
                throw;
            }
            return isDeleted;
        }
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {

                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                    /*TODO: You must process this exception.*/
                }
            }
            return result;
        }
        
    }
}