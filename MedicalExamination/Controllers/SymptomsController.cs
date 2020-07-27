using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalExamination.Models;
using MedicalExamination.Models.TestAndDisease;
using MedicalExamination.ViewModels.DiseasesAndSymptoms;

namespace MedicalExamination.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SymptomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Symptoms
        public ActionResult Index()
        {
            var result = new List<SymptomsViewModel>();
            var symptoms = db.Symptoms.ToList();

            foreach(var symptom in symptoms)
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == symptom.CategoryId);

                var viewModel = new SymptomsViewModel()
                {
                    Symptom = symptom,
                    CategoryNameAr = category.CategoryName,
                };
                result.Add(viewModel);
            }
            return View(result);
        }

        // GET: Symptoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptoms symptom = db.Symptoms.Find(id);
            if (symptom == null)
            {
                return HttpNotFound();
            }

            var category = db.Categories.FirstOrDefault(x => x.Id == symptom.CategoryId);

            var viewModel = new SymptomsViewModel()
            {
                Symptom = symptom,
                CategoryNameAr = category.CategoryName,
            };
            return View(viewModel);
        }

        // GET: Symptoms/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();

            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,CreationDate,CategoryId")] Symptoms symptoms)
        {
            symptoms.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Symptoms.Add(symptoms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();

            return View(symptoms);
        }

        // GET: Symptoms/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories = db.Categories.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Symptoms symptoms = db.Symptoms.Find(id);
            if (symptoms == null)
            {
                return HttpNotFound();
            }
            return View(symptoms);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,CreationDate,CategoryId")] Symptoms symptoms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptoms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = db.Categories.ToList();

            return View(symptoms);
        }
        
        // POST: Symptoms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Symptoms symptoms = db.Symptoms.Find(id);
            db.Symptoms.Remove(symptoms);
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
