using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using TestUI.Rest;
using TestUI.Rest.Models;
using TestUI.Helpers;
using Newtonsoft.Json;
using Common.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test.Controllers
{
    public class ClassesController : Controller
    {
        const string const_APIControllerName = "classes";

        public ClassesController()
        {

        }

        // GET: Classes
        public ActionResult Index()
        {
            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}", Method.GET);
            var response = rest.Send();

            var classes = JsonConvert.DeserializeObject<List<ClassModel>>(response.Content);

            return View(classes);
        }

        //// GET: Classes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Class _class = _unitOfWork.Classes.Get(id.Value);
        //    if (_class == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(_class);
        //}

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "Id,Name,Location,TeacherName")] ClassModel model)
        {
            if (ModelState.IsValid)
            {
                var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}", Method.POST);
                rest.Body = model;
                //rest.AddParameters(new List<RestParameter>
                //{
                //    new RestParameter(nameof(ClassModel.Name), model.Name),
                //    new RestParameter(nameof(ClassModel.Location), model.Location),
                //    new RestParameter(nameof(ClassModel.TeacherName), model.TeacherName),
                //});

                var response = rest.Send();

                if (response.StatusCode == HttpStatusCode.Created)
                    Utility.AddMessageToPage(this, "Data added successfully", MessageType.Success);
                else if (response.StatusCode == HttpStatusCode.Accepted)
                    Utility.AddMessageToPage(this, "Data not added", MessageType.Error);
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

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id.Value < 1)
                return BadRequest();

            var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}/{id.ToString()}", Method.GET);
            var response = rest.Send();

            var _class = JsonConvert.DeserializeObject<ClassModel>(response.Content);

            return View(_class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "Id,Name,Location,TeacherName")] ClassModel model)
        {
            if (ModelState.IsValid)
            {
                var rest = new RestHelper(TestUI.Helpers.Utility.GetAPIUrl(), $"/{const_APIControllerName}/{model.Id.ToString()}", Method.PUT);
                rest.Body = model;

                var response = rest.Send();

                if (response.StatusCode == HttpStatusCode.OK)
                    Utility.AddMessageToPage(this, "Data edited successfully", MessageType.Success);
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

        // GET: Classes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Class _class = _unitOfWork.Classes.Get(id.Value);
        //    if (_class == null)
        //        return HttpNotFound();

        //    return View(_class);
        //}

        // POST: Classes/Delete/5
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
                //_unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
