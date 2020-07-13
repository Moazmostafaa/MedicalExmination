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
        public ActionResult RateDoctor(string docId, decimal rate)
        {
            Rating addrate = new Rating();
            string Patient = User.Identity.GetUserId();
            var patientrates = db.Ratings.Where(c => c.DoctorId == docId).Any(x => x.PatientId == Patient);

            if (patientrates)
            {
                var rateModel = db.Ratings.FirstOrDefault(p => p.PatientId == Patient && p.DoctorId == docId);
                rateModel.RatingValue = rate;
                db.Entry(rateModel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var newRatingModel = new Rating()
                {
                    DoctorId = docId,
                    PatientId = Patient,
                    RatingValue = rate,
                };
                db.Ratings.Add(newRatingModel);
                db.SaveChanges();
            }

            var totalRate = db.Ratings.Where(x => x.DoctorId == docId).Sum(x => x.RatingValue);
            var numOfUsers = db.Ratings.Where(x => x.DoctorId == docId).Count();

            var avarage = totalRate / numOfUsers;

            var doctorModel = db.Doctors.FirstOrDefault(p => p.Id == docId);
            doctorModel.Total_Rate = avarage;
            doctorModel.UsersRated = numOfUsers;
            db.Entry(doctorModel).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ViewProfile_Patient", "Doctors", new { profId = docId });
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
