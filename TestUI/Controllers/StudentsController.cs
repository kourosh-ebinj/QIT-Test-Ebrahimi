using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using TestUI.Rest;
using TestUI.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Common.Helpers;

namespace Test.Controllers
{
    public class StudentsController : Controller
    {
        const string const_APIControllerName = "students";

        // GET: Students
        public ActionResult Index(int? id)
        {
            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/classes", Method.GET);
            var response = rest.Send();

            var classes = JsonConvert.DeserializeObject<List<ClassModel>>(response.Content);
            ViewData.Add(TestUI.Helpers.Utility.const_ClassesData, classes);

            if (classes.Count < 1)
            {
                Utility.AddMessageToPage(this, "No Classes found.", MessageType.Warning);
                return RedirectToAction("Index", "Classes");
            }

            if (!id.HasValue)
                return RedirectToAction("GoToClass", new { classId = classes.First().Id });

            rest = null;
            response = null;

            rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}", Method.GET);
            response = rest.Send();

            var students = JsonConvert.DeserializeObject<List<StudentModel>>(response.Content);

            return View(students);
        }

        public ActionResult GoToClass(int classId)
        {
            return RedirectToAction("Index", new { id = classId });
        }

        // GET: Students/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Student student = _unitOfWork.Students.Get(id.Value);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        // GET: Students/Create
        public ActionResult Create()
        {
            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/classes", Method.GET);
            var response = rest.Send();

            var classes = JsonConvert.DeserializeObject<List<ClassModel>>(response.Content);
            ViewData.Add(TestUI.Helpers.Utility.const_ClassesData, classes);

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "Id,Name,Age,GPA,ClassId")] StudentModel model)
        {
            if (ModelState.IsValid)
            {
                var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}", Method.POST);
                rest.Body = model;

                var response = rest.Send();

                if (response.StatusCode == HttpStatusCode.Created)
                    Utility.AddMessageToPage(this, "Data added successfully", MessageType.Success);
                else if (response.StatusCode == HttpStatusCode.Accepted)
                    Utility.AddMessageToPage(this, "Data not added", MessageType.Error);
                else if (response.StatusCode == HttpStatusCode.Conflict)
                    Utility.AddMessageToPage(this, "This student already exists", MessageType.Warning);
                else
                    Utility.AddMessageToPage(this, "An error has occured", MessageType.Error);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
                    Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
            }

            return View(model);

        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id.Value < 1)
                return BadRequest();

            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}/{id.ToString()}", Method.GET);
            var response = rest.Send();

            var student = JsonConvert.DeserializeObject<StudentModel>(response.Content);

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "Id,Name,Age,GPA,ClassId")] StudentModel model)
        {
            if (ModelState.IsValid)
            {
                var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}/{model.Id.ToString()}", Method.PUT);
                rest.Body = model;

                var response = rest.Send();

                if (response.StatusCode == HttpStatusCode.OK)
                    Utility.AddMessageToPage(this, "Data edited successfully", MessageType.Success);
                else if (response.StatusCode == HttpStatusCode.Conflict)
                    Utility.AddMessageToPage(this, "This student already exists", MessageType.Warning);
                else if (response.StatusCode == HttpStatusCode.Accepted)
                    Utility.AddMessageToPage(this, "Data not edited", MessageType.Error);
                else
                    Utility.AddMessageToPage(this, "An error has occured", MessageType.Error);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
                    Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
            }

            return View(model);
        }

        // GET: Students/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Student student = _unitOfWork.Students.Get(id.Value);
        //    if (student == null)
        //        return HttpNotFound();

        //    _unitOfWork.Students.Remove(student);
        //    if (_unitOfWork.Commit() == 1)
        //        Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
        //    else
        //        Utility.AddMessageToPage(this, "Data not deleted", MessageType.Error);

        //    return View(student);
        //}

        // POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id < 1)
                return BadRequest();

            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}/{id.ToString()}", Method.DELETE);

            var response = rest.Send();

            if (response.StatusCode == HttpStatusCode.NoContent)
                Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
            else if (response.StatusCode == HttpStatusCode.Accepted)
                Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
            else
                Utility.AddMessageToPage(this, "Data not deleted", MessageType.Error);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
