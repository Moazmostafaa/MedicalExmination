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
    public class DiseasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diseases
        public ActionResult Index()
        {
            var result = new List<DiseasesViewModel>();
            var diseases = db.Diseases.ToList();

            foreach (var disease in diseases)
            {
                var viewModel = new DiseasesViewModel()
                {
                    Disease = disease,
                    CategoryNameAr = db.Categories.FirstOrDefault(x => x.Id == disease.CategoryId).CategoryName,
                };
                result.Add(viewModel);
            }

            return View(result);
        }

        // GET: Diseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DiseasesViewModel()
            {
                Disease = disease,
                CategoryNameAr = db.Categories.FirstOrDefault(x => x.Id == disease.CategoryId).CategoryName,
            };

            return View(viewModel);
        }

        // GET: Diseases/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,CreationDate,CategoryId")] Disease disease)
        {
            disease.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var diseaseIsExist = db.Diseases.Any(x => x.NameAr == disease.NameAr || x.NameEn == disease.NameEn);
                if (diseaseIsExist)
                {
                    ViewBag.ErrorMessage = "هذا المرض موجود من قبل من فضلك اضف مرض جديد.";
                    return View();
                }

                db.Diseases.Add(disease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();

            return View(disease);
        }

        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories = db.Categories.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,CreationDate,CategoryId")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                var diseaseIsExist = db.Diseases.Any(x => (x.NameAr == disease.NameAr || x.NameEn == disease.NameEn) && x.Id != disease.Id);
                if (diseaseIsExist)
                {
                    ViewBag.ErrorMessage = "هذا المرض موجود من قبل من فضلك اضف مرض جديد.";
                    return View();
                }

                db.Entry(disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();

            return View(disease);
        }
        // POST: Diseases/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Disease disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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
