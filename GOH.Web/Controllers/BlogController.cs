using GOH.Data.Repositories;
using GOH.Data.Repositories.Interfaces;
using GOH.Entities.BlogEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Web.Hosting;
using GOH.Data.Contexts;
using GOH.Entities.BlogEntities.ViewModels;

namespace GOH.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private IUnitOfWork _uow;

        public BlogController()
        {
            _uow = new UnitOfWork(new BlogContext());
        }

        public BlogController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Blogs
        [AllowAnonymous]
        public ActionResult Index()
        {
            BlogLanding blogLanding = new BlogLanding();

            blogLanding.Blog = _uow.Blogs.GetLatest();


            return View(blogLanding);
        }

        // GET: Blogs
        public ActionResult List()
        {
            return View(_uow.Blogs.GetAll().OrderByDescending(b => b.CreatedOn));
        }

        // GET: Blogs/Details/5
        [HttpGet]
        public ActionResult Details(int id, Comment comments)
        {
            Blog blog = _uow.Blogs.Find(b => b.BlogId == id, b => b.Tags, b => b.Comments) as Blog;

            if (blog == null)
            {
                return HttpNotFound();
            }

            ViewBag.BlogId = blog.BlogId.ToString();

            if (comments != null)
            {
                ViewBag.commentserName = comments.CommenterName;
                ViewBag.Value = comments.Value;
            }
            return View(blog);
        }


        //// GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,TagsText")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                // get tags with same values from db
                IEnumerable<Tag> tagsAlreadyExist = _uow.Tags.GetWhere(t => blog.TagsText.Contains(t.Value));

                // replace tags with db originals
                for (int i = 0; i < blog.Tags.Count(); i++)
                {
                    Tag existingTag = tagsAlreadyExist.FirstOrDefault(t => t.Value == blog.Tags[i].Value);
                    if (existingTag != null)
                        blog.Tags[i] = existingTag;
                }

                _uow.Blogs.Add(blog);
                _uow.Complete();

                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int id)
        {
            Blog blog = _uow.Blogs.Find(b => b.BlogId == id) as Blog;

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Created")] Blog postedBlog)
        {
            if (ModelState.IsValid)
            {
                Blog blog = _uow.Blogs.Find(b => b.BlogId == postedBlog.BlogId) as Blog;
                blog.Title = postedBlog.Title;
                blog.Content = postedBlog.Content;
                _uow.Complete();

                return RedirectToAction("Index");
            }
            return View(postedBlog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int id)
        {
            Blog blog = _uow.Blogs.Find(b => b.BlogId == id) as Blog;
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = _uow.Blogs.Find(b => b.BlogId == id) as Blog;
            _uow.Blogs.Remove(blog);
            _uow.Complete();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
