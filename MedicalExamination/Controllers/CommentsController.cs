using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalExamination.Models;
using MedicalExamination.ViewModels;
using Microsoft.AspNet.Identity;

namespace MedicalExamination.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var viewModel = new Comment();
            var comments = db.Comments.Include(c => c.Doctor).Include(c => c.post);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.PostId = new SelectList(db.Posts, "Id", "PostContant");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentContent,CommentDate,PostId,DoctorId")] Comment comment)
        {
            var DoctorId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                comment.DoctorId = DoctorId;
                comment.CommentDate = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Doctors");
            }


            ViewBag.PostId = new SelectList(db.Posts, "Id", "PostContant", comment.PostId);
            return View();
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Users, "Id", "UserType", comment.DoctorId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "PostContant", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentContent,CommentDate,PostId,DoctorId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentDate = DateTime.Now;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Users, "Id", "UserType", comment.DoctorId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "PostContant", comment.PostId);
            return View(comment);
        }
        

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Comments","Comments",new { postId = comment.PostId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Comments(int postId)
        {
            var comments = new List<CommentsViewModel>();
            var postComments = db.Comments.Where(x => x.PostId == postId).ToList();
            foreach (var comment in postComments)
            {
                var commentViewModel = new CommentsViewModel()
                {
                    Comment = comment,
                    OwnerName = db.Doctors.FirstOrDefault(x => x.Id == comment.DoctorId).UserName,
                };
                comments.Add(commentViewModel);
            }

            return PartialView(comments);
        }
    }
}
