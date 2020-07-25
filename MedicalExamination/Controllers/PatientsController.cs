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
using MedicalExamination.Models.Doctor;

namespace MedicalExamination.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Patients
        public ActionResult Index()
        {
            var viewModel = new PostsViewModel();
            var patientId = User.Identity.GetUserId();
            var userType = db.Users.FirstOrDefault(x => x.Id == patientId).UserType;
            ViewBag.PatientId = patientId;
            ViewBag.UserType = userType;
            viewModel.Categories = db.Categories.Include(p => p.Posts).ToList();

            viewModel.CommentsViewModel = new List<CommentsViewModel>();
            foreach (var category in viewModel.Categories)
            {
                foreach (var post in category.Posts)
                {

                    var postComments = db.Comments.Where(x => x.PostId == post.Id).ToList();
                    foreach (var comment in postComments)
                    {
                        var commentViewModel = new CommentsViewModel()
                        {
                            Comment = comment,
                            OwnerName = db.Doctors.FirstOrDefault(x => x.Id == comment.DoctorId).UserName,
                        };
                        viewModel.CommentsViewModel.Add(commentViewModel);
                    }
                }
            }

            return View(viewModel);
        }

        // View Profile
        public ActionResult ViewProfile_Patient(string profId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            var viewModel = new PostsViewModel();
            viewModel.Posts = db.Posts.Where(p => p.PatientId == profId).ToList();
            viewModel.Patient = db.Patients.FirstOrDefault(p => p.Id == profId);

            return View(viewModel);
        }

        //public ActionResult GetPostsByPatient()
        //{
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
        //    var viewModel = new PostsViewModel();
        //    var patientId = User.Identity.GetUserId();
        //    viewModel.Posts = db.Posts.Where(p => p.PatientId == patientId).ToList();
        //    viewModel.Patients = db.Patients.Where(c => c.Id == patientId).ToList();
        //    if (viewModel != null)
        //    {
        //        return View(viewModel);
        //    }
        //    else
        //    {
        //        return Content("Patient = null");
        //    }

        //}

        //// POST: Patients/RateDoctor/{rate}
        [HttpPost]
        [Authorize(Roles = ("مريض"))]
        public JsonResult RateDoctor(string docId, int rate)
        {
            string patientId = User.Identity.GetUserId();
            var patientrates = db.Ratings.Where(c => c.DoctorId == docId).Any(x => x.PatientId == patientId);
            var oldRate = 0;
            if (patientrates)
            {
                var rateModel = db.Ratings.FirstOrDefault(p => p.PatientId == patientId && p.DoctorId == docId);
                oldRate = rateModel.RatingValue;
                rateModel.RatingValue = rate;
                db.Entry(rateModel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var newRatingModel = new Rating()
                {
                    DoctorId = docId,
                    PatientId = patientId,
                    RatingValue = rate,
                };
                db.Ratings.Add(newRatingModel);
                db.SaveChanges();
            }

            var doctorModel = db.Doctors.FirstOrDefault(p => p.Id == docId);

            doctorModel.Total_Rate = db.Ratings.Where(x => x.DoctorId == docId)?.Average(x => x.RatingValue) ?? 0;

            if (!patientrates)
            {
                doctorModel.UsersRated += 1;
            }
            db.Entry(doctorModel).State = EntityState.Modified;
            db.SaveChanges();

            return Json(Url.Action("Details", "Doctors", new { doctorid = docId }));
        }

        // GET: Patients/Details/5
        public ActionResult Details(string patientid)
        {
            if (patientid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(patientid);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CityId,Gender,BirthDate,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Id_Patient,UserId_Paatient")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", patient.CityId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", patient.CityId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CityId,Gender,BirthDate,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Id_Patient,UserId_Paatient")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "CityName", patient.CityId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Posts.Where(a => a.PostContant.Contains(searchName) || a.Category.CategoryName.Contains(searchName)).ToList() ;
            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
