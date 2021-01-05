using System.Net;
using System.Web.Mvc;
using BlogApp.Validation;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Web.Hosting;
using GOH.Data.Repositories.Interfaces;
using GOH.Data.Repositories;
using GOH.Data.Contexts;
using GOH.Entities.BlogEntities;

namespace WebUI.Controllers
{
    public class CommentsController : Controller
    {
        private IUnitOfWork _uow;

        public CommentsController()
        {
            _uow = new UnitOfWork(new BlogContext());
        }

        public CommentsController(UnitOfWork uow)
        {
            _uow = new UnitOfWork(new BlogContext());
        }

        // GET: Comments
        //public ActionResult Index()
        //{
        //    return View(db.Comments.ToList());
        //}

        // GET: Comments/Details/5
        public ActionResult BlogComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            XmlDocument commentDoc = _uow.Comments.GetCommentTree(id.Value);

            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter tx = new XmlTextWriter(sw))
                {
                    commentDoc.WriteTo(tx);
                    ViewBag.Comments = sw.ToString();

                }
            }

            ViewBag.XsltPath = @"~/App_Data/Comments.xslt";


            return PartialView("_BlogComments");
        }

        // GET: Comments
        //public ActionResult Index()
        //{
        //    return View(db.Comments.ToList());
        //}

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = _uow.Comments.Find(c => c.CommentId == id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Details", comment);
        }

        //GET Reply Comment
        public ActionResult Reply([Bind(Include = "ReplyTo,ParentId,BlogId")] int blogId, int parentId, string replyTo)
        {
            ViewBag.BlogId = blogId;
            ViewBag.ParentId = parentId;
            ViewBag.ReplyTo = replyTo;

            return View();
        }

        // GET: Comments/Create
        public ActionResult Create(int blogId)
        {
            if (TempData.ContainsKey("ModelState"))
            {
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
            }
            ViewBag.BlogId = blogId;

            return PartialView("_Create");
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommenterName,Value,BlogId,ParentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _uow.Comments.Add(comment);
                _uow.Complete();

                if (Request.IsAjaxRequest())
                {
                    return RedirectToAction("BlogComments", new { id = comment.BlogId });

                    //return PartialView("_Details", comment);
                }
                else
                {
                    return RedirectToAction("Details", "Blogs", new { id = comment.BlogId });
                }
            }

            // parentId is 0, must be from details page
            if (comment.ParentId == null)
            {
                TempData["ModelState"] = ModelState;
                return RedirectToAction("Details", "Blogs", new { id = comment.BlogId, commenterName = comment.CommenterName, value = comment.Value });
            }
            else
            {
                return View("Reply", comment);
            }

        }


        //// GET: Comments/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comment);
        //}

        //// POST: Comments/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CommentId,CommenterName,Value,CreatedOn,ChangedOn")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(comment).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(comment);
        //}

        //// GET: Comments/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comment);
        //}

        //// POST: Comments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Comment comment = db.Comments.Find(id);
        //    db.Comments.Remove(comment);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
