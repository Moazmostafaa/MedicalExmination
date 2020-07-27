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
using OfficeOpenXml;
using Microsoft.Office.Interop;

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


        public ActionResult GuestDetails(string diseaseName , List<Symptoms> symptomsList)
        {
            symptomsList = TempData["list"] as List<Symptoms>;
            var viewModel = new TestViewModel()
            {
                Test = new Test() { CreationDate = DateTime.Now, DiseaseNameAr = diseaseName },
                Symptoms = symptomsList
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [HttpGet]
        public ActionResult TakeTest()
        {
            var symptoms = db.Symptoms.ToList();
            ViewBag.Categories = db.Categories.ToList();
            TestViewModel viewModel = new TestViewModel()
            {
                Symptoms = symptoms
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TakeTest(TestViewModel viewModel)
        {
            var symptoms = db.Symptoms.ToList();

            if (viewModel.TestFilter.CategoryId > 0)
                symptoms = symptoms.Where(x => x.CategoryId == viewModel.TestFilter.CategoryId).ToList();
            if (!string.IsNullOrEmpty(viewModel.TestFilter.SymptomName))
                symptoms = symptoms.Where(x => x.NameAr.Contains(viewModel.TestFilter.SymptomName)).ToList();

            viewModel.Symptoms = symptoms;
            ViewBag.Categories = db.Categories.ToList();

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult TakeTestSymptoms(int[] symptomsIDs)
        {

            if (symptomsIDs.Length == 0)
            {
                ViewBag.ErrorMessage = "من فضلك اختر الأعراض";
                return View();
            }
            List<int> categoriesIds = new List<int>();
            string[] symptomsArNames = new string[symptomsIDs.Length];
            List<Symptoms> symptoms = new List<Symptoms>();
            for (int i = 0; i < symptomsIDs.Length; i++)
            {
                var symptomId = symptomsIDs[i];
                var symptom = db.Symptoms.FirstOrDefault(x => x.Id == symptomId);
                symptoms.Add(symptom);
                categoriesIds.Add(symptom.CategoryId);
                symptomsArNames[i] = symptom.NameAr;
            }

            //var engine = Python.CreateEngine();
            //dynamic py = engine.ExecuteFile(@"E:\Medical Examination\Medical_Diagnostic\Medical_Diagnostic.py");
            //dynamic diagnosticModel = py.Diagnostic_Model();
            //var line = diagnosticModel.Symptoms_Data(symptomsArNames).Tostring();

            var anyDuplicate = categoriesIds.GroupBy(x => x).FirstOrDefault(g => g.Count() > 1);
            if (anyDuplicate == null)
            {
                TempData["ErrorMessage"] = "من فضلك اختر المزيد من الاعراض لضمان نتيجة اوضح.";
                return Json(Url.Action("TakeTest"));
            }
            var disease = db.Diseases.FirstOrDefault(x => x.CategoryId == anyDuplicate.Key);

            var diseaseName = disease.NameAr;
            var test = new Test();
            //var disease = db.Diseases.FirstOrDefault(x => x.NameAr == diseaseName);
            var patientId = User.Identity.GetUserId();

            if (patientId != null)
            {
                test = new Test()
                {
                    CreationDate = DateTime.Now,
                    DiseaseId = disease.Id,
                    DiseaseNameAr = disease.NameAr,
                    DiseaseNameEn = disease.NameEn,
                    UserId = patientId,
                };
                db.Tests.Add(test);
                db.SaveChanges();

                var testSymptoms = new List<TestSymptoms>();
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
                return Json(Url.Action("Details", new { id = test.Id }));
            }
            else
            {
                TempData["list"] = symptoms;
                return Json(Url.Action("GuestDetails", new { diseaseName = disease.NameAr }));
            }


        }


        //public void newroute()
        //{
        //    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Open(filePath);
        //    int[] Cols = new int[132];
        //    string[] symptoms = new string[5];
        //    for (int r = 1; r <= 132; r++)
        //    {
        //        Cols[r] = r;
        //    }

        //    string data = string.Empty;
        //    int i = 0;
        //    foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in workBook.Worksheets)
        //    {
        //        foreach (Microsoft.Office.Interop.Excel.Range row in sheet.UsedRange.Rows)
        //        {
        //            foreach (int c in Cols)
        //            {
        //                var symptomName = symptoms[i];
        //                if(sheet.Cells[row.Row, c].Value2.Tostrin() == symptomName && sheet.Cells[row.Row + 1, c].Value2)
        //                {

        //                }
        //            }
        //        }
        //    }

        //}
    }
}
