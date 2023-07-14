using BooksCrud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BooksCrud.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Book_table.ToList()); 
            }
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Book_table.Where(x=>x.bookId == id).FirstOrDefault());
            }
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book_table books, HttpPostedFileBase imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        // Obtener el nombre del archivo
                        books.image = Path.GetFileName(imageFile.FileName);

                        // Guardar la imagen en la carpeta Images
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), books.image);
                        imageFile.SaveAs(imagePath);
                    }

                    using (DbModels context = new DbModels())
                    {
                        context.Book_table.Add(books);
                        context.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                return View(books);
            }
            catch
            {
                return View(books);
            }
        }


        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Book_table.Where(x => x.bookId == id).FirstOrDefault());
            }
        }

        // POST: Book/Edit/5
        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book_table book, HttpPostedFileBase imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DbModels context = new DbModels())
                    {
                        var existingBook = context.Book_table.Find(id);

                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            // Obtener el nombre del archivo
                            book.image = Path.GetFileName(imageFile.FileName);

                            // Guardar la imagen en la carpeta "Images"
                            string imagePath = Path.Combine(Server.MapPath("~/Images"), book.image);
                            imageFile.SaveAs(imagePath);
                        }
                        else
                        {
                            book.image = existingBook.image;
                        }
                            existingBook.name = book.name;
                            existingBook.author = book.author;
                            existingBook.date = book.date;
                            existingBook.image = book.image;
                            context.SaveChanges();
                        }

                    return RedirectToAction("Index");
                }

                return View(book);
            }
            catch
            {
                return View(book);
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Book_table.Where(x => x.bookId == id).FirstOrDefault());
            }
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DbModels context = new DbModels()) { 
                    Book_table book = context.Book_table.Where(x => x.bookId == id).FirstOrDefault();
                    context.Book_table.Remove(book);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
