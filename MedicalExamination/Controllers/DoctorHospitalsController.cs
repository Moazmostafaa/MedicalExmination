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
using Microsoft.AspNet.Identity;
using MedicalExamination.ViewModels;

namespace MedicalExamination.Controllers
{
    [Authorize(Roles = "Admin,دكتور")]
    public class DoctorHospitalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DoctorHospitals
        public ActionResult Index()
        {
            var viewModel = new DoctorViewModel();
            var doctorId = User.Identity.GetUserId();
            ViewBag.DoctorId = doctorId;
            viewModel.DoctorHospitals = db.DoctorHospitals.Where(d => d.DoctorId == doctorId).ToList();
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "Name");
            return View(viewModel);
        }

        // GET: DoctorHospitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorHospital doctorHospital = db.DoctorHospitals.Find(id);
            if (doctorHospital == null)
            {
                return HttpNotFound();
            }
            return View(doctorHospital);
        }

        // GET: DoctorHospitals/Create
        public ActionResult Create()
        {
            
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "Name");
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة"});
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص"});
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص"});
            return View();
        }

        private List<DateTime> getWeekDayDates(int month, int year)
        {
            List<DateTime> weekdays = new List<DateTime>();
            DateTime basedt = new DateTime(year, month, 1);
            while ((basedt.Month == month) && (basedt.Year == year))
            {
                if (basedt.DayOfWeek == (DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday))
                {
                    weekdays.Add(new DateTime(basedt.Year, basedt.Month, basedt.Day));
                }
                basedt = basedt.AddDays(1);
            }
            return weekdays;
        }

        // POST: DoctorHospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DayName,From,To,DoctorId,HospitalId")] DoctorHospital doctorHospital)
        {
            var DoctorId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                doctorHospital.DoctorId = DoctorId;
                db.DoctorHospitals.Add(doctorHospital);
                db.SaveChanges();
                return RedirectToAction("Index", "DoctorHospitals");
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "Name",doctorHospital.HospitalId);
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة"});
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            return View(doctorHospital);
        }

        // GET: DoctorHospitals/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "Name");
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorHospital doctorHospital = db.DoctorHospitals.Find(id);
            if (doctorHospital == null)
            {
                return HttpNotFound();
            }
            return View(doctorHospital);
        }

        // POST: DoctorHospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DayName,From,To,DoctorId,HospitalId")] DoctorHospital doctorHospital)
        {
            var DoctorId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                doctorHospital.DoctorId = DoctorId;
                db.Entry(doctorHospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "Name", doctorHospital.HospitalId);
            ViewBag.DayName = new SelectList(new[] { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" });
            ViewBag.From = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            ViewBag.To = new SelectList(new[] { "1ص", "2ص", "3ص", "4ص", "5ص", "6ص", "7ص", "8ص", "9ص", "10ص", "11ص", "12م", "1م", "2م", "3م", "4م", "5م", "6م", "7م", "8م", "9م", "10م", "11م", "12ص" });
            return View(doctorHospital);
        }

        // GET: DoctorHospitals/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorHospital doctorHospital = db.DoctorHospitals.Find(id);
            if (doctorHospital == null)
            {
                return HttpNotFound();
            }
            return View(doctorHospital);
        }

        // POST: DoctorHospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var DoctorId = User.Identity.GetUserId();
            DoctorHospital doctorHospital = db.DoctorHospitals.Find(id);
            doctorHospital.DoctorId = DoctorId;
            db.DoctorHospitals.Remove(doctorHospital);
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
