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
using MedicalExamination.Models.TestAndDisease;
using MedicalExamination.ViewModels;
using MedicalExamination.ViewModels.DiseasesAndSymptoms;
using Microsoft.AspNet.Identity;
using IronPython.Hosting;
using System.Diagnostics;
using System.IO;

namespace MedicalExamination.Controllers
{
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tests
        public ActionResult Index()
        {
            var patientId = User.Identity.GetUserId(); ;
            return View(db.Tests.Where(x => x.UserId == patientId).ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }

            var testSymptoms = db.TestSymptoms.Where(x => x.TestId == id).ToList();

            var diseaseCategoryId = db.Diseases.FirstOrDefault(x => x.Id == test.DiseaseId).CategoryId;

            var symptoms = new List<Symptoms>();

            foreach (var testSymptom in testSymptoms)
            {
                var symptom = db.Symptoms.FirstOrDefault(x => x.Id == testSymptom.SymptomId);
                symptoms.Add(symptom);

            }
            var doctors = db.Doctors.Where(x => x.CategoryId == diseaseCategoryId).ToList();

            var viewModel = new TestViewModel()
            {
                Test = test,
                Symptoms = symptoms,
                Doctors = doctors
            };

            return View(viewModel);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
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

        [HttpGet]
        public ActionResult TakeTest()
        {
            var symptoms = db.Symptoms.ToList();

            return View(symptoms);
        }

        [HttpPost]
        public ActionResult TakeTest(int[] symptomsIDs)
        {

            if (symptomsIDs.Length == 0)
            {
                ViewBag.ErrorMessage = "من فضلك اختر الأعراض";
                return View();
            }

            string[] symptomsArNames = new string[symptomsIDs.Length];

            for (int i = 0; i < symptomsIDs.Length; i++)
            {
                var symptomId = symptomsIDs[i];
                var symptom = db.Symptoms.FirstOrDefault(x => x.Id == symptomId);
                symptomsArNames[i] = symptom.NameAr;
            }

            //var engine = Python.CreateEngine();
            //dynamic py = engine.ExecuteFile(@"E:\Medical Examination\Medical_Diagnostic\Medical_Diagnostic.py");
            //dynamic diagnosticModel = py.Diagnostic_Model();
            //var line = diagnosticModel.Symptoms_Data(symptomsArNames).Tostring();

            var diseaseName = "عدوى فطرية";

            var disease = db.Diseases.FirstOrDefault(x => x.NameAr == diseaseName);
            var patientId = User.Identity.GetUserId();

            var test = new Test()
            {
                CreationDate = DateTime.Now,
                DiseaseId = disease.Id,
                DiseaseNameAr = disease.NameAr,
                DiseaseNameEn = disease.NameEn,
                UserId = patientId,
            };
            db.Tests.Add(test);

            var testSymptoms = new List<TestSymptoms>();
            db.SaveChanges();
            for (int i = 0; i < symptomsIDs.Length; i++)
            {
                var symptomId = symptomsIDs[i];
                var symptom = db.Symptoms.FirstOrDefault(x => x.Id == symptomId);

                var testSymptom = new TestSymptoms()
                {
                    TestId = test.Id,
                    SymptomId = symptom.Id,
                };
                testSymptoms.Add(testSymptom);
            }

            db.TestSymptoms.AddRange(testSymptoms);

            db.SaveChanges();


            return Json(Url.Action("Index"));
        }
    }
}
