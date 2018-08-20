using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Core;
using Test.Core.Domains;
using Test.Helpers;
using Test.Persistence;
using Test.Persistence.Contexts;

namespace Test.Controllers
{
    public class StudentsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public StudentsController()
        {
            TestContext db = new TestContext();
            _unitOfWork = new UnitOfWork(db);
        }

        // GET: Students
        public ActionResult Index()
        {

            return View(_unitOfWork.Students.GetAll().ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id.Value < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student student = _unitOfWork.Students.Get(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var classes = _unitOfWork.Classes.GetAll().ToList();
            ViewData.Add(Utility.const_ClassesData, classes);

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,GPA,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {                 
                _unitOfWork.Students.Add(student);
                if (_unitOfWork.Commit() == 1)
                    Utility.AddMessageToPage(this, "Data added successfully", MessageType.Success);
                else
                    Utility.AddMessageToPage(this, "Data not added", MessageType.Error);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
                    Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id.Value < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student student = _unitOfWork.Students.Get(id.Value);
            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,GPA,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var obj = _unitOfWork.Students.Get(student.Id);
                obj.Age = student.Age;
                obj.Name = student.Name.Trim();
                obj.GPA = student.GPA;

                _unitOfWork.Students.Edit(student);
                if (_unitOfWork.Commit() == 1)
                    Utility.AddMessageToPage(this, "Data updated successfully", MessageType.Success);
                else
                    Utility.AddMessageToPage(this, "Data not updated", MessageType.Error);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
                    Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
            }

            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id.Value < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Student student = _unitOfWork.Students.Get(id.Value);
            if (student == null)
                return HttpNotFound();

            _unitOfWork.Students.Remove(student);
            if (_unitOfWork.Commit() == 1)
                Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
            else
                Utility.AddMessageToPage(this, "Data not deleted", MessageType.Error);

            return View(student);
        }

        // POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id < 1)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student student = _unitOfWork.Students.Get(id);
            if (student == null)
                return HttpNotFound();

            _unitOfWork.Students.Remove(student);
            if (_unitOfWork.Commit() == 1)
                Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
            else
                Utility.AddMessageToPage(this, "Data not deleted", MessageType.Error);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
