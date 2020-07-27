using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalExamination.Models;
using MedicalExamination.Models.Doctor;
using MedicalExamination.ViewModels;
using Microsoft.AspNet.Identity;

namespace MedicalExamination.Controllers
{
    [Authorize(Roles = "Admin,دكتور")]
    public class ClinicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clinics
        public ActionResult Index()
        {
            var viewModel = new DoctorViewModel();
            var doctorId = User.Identity.GetUserId();
            ViewBag.DoctorId = doctorId;
            viewModel.Clinics = db.Clinics.Where(d => d.DoctorId == doctorId).ToList();
            return View(viewModel);
        }

        // GET: Clinics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // GET: Clinics/Create

        public ActionResult Create()
        {
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,DayName,From,To")] Clinic clinic)
        {
            var DoctorId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                clinic.DoctorId = DoctorId;
                db.Clinics.Add(clinic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DayName,From,To,Address")] Clinic clinic)
        {
            var DoctorId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                clinic.DoctorId = DoctorId;
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clinic clinic = db.Clinics.Find(id);
            db.Clinics.Remove(clinic);
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
