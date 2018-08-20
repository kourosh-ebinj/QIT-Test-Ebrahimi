using RestSharp;
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
using Test.Rest;
using Test.Rest.Models;

namespace Test.Controllers
{
    public class ClassesController : Controller
    {

        public ClassesController()
        {
        
        }

        // GET: Classes
        public ActionResult Index()
        {
            var rest = new RestHelper(Utility.GetAPIUrl(), "Classes", Method.GET);
            //rest.AddParameters(new List<RestParameter>
            //{
            //    new RestParameter("token", model.Token),
            //    new RestParameter("userId", model.PhoneNumber),
            //    new RestParameter("transactionId", model.TransactionId),
            //    new RestParameter("productList", model.ProductList)
            //});

            var response = rest.Send();

            return View();
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

        //// GET: Classes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Classes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Location,TeacherName")] Class _class)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Classes.Add(_class);
        //        if (_unitOfWork.Commit() == 1)
        //            Utility.AddMessageToPage(this, "Data added successfully", MessageType.Success);
        //        else
        //            Utility.AddMessageToPage(this, "Data not added", MessageType.Error);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
        //            Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
        //    }

        //    return View(_class);
        //}

        //// GET: Classes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Class _class = _unitOfWork.Classes.Get(id.Value);
        //    if (_class == null)
        //        return HttpNotFound();

        //    return View(_class);
        //}

        //// POST: Classes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Location,TeacherName")] Class _class)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var obj = _unitOfWork.Classes.Get(_class.Id);
        //        obj.Location = _class.Location.Trim();
        //        obj.Name = _class.Name.Trim();
        //        obj.TeacherName = _class.TeacherName.Trim();

        //        _unitOfWork.Classes.Edit(obj);
        //        if (_unitOfWork.Commit() == 1)
        //            Utility.AddMessageToPage(this, "Data updated successfully", MessageType.Success);
        //        else
        //            Utility.AddMessageToPage(this, "Data not updated", MessageType.Error);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        foreach (ModelError error in ModelState.SelectMany(x => x.Value.Errors))
        //            Utility.AddMessageToPage(this, error.ErrorMessage, MessageType.Error);
        //    }

        //    return View(_class);
        //}

        //// GET: Classes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null || id.Value < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Class _class = _unitOfWork.Classes.Get(id.Value);
        //    if (_class == null)
        //        return HttpNotFound();

        //    return View(_class);
        //}

        //// POST: Classes/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    if (id < 1)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    Class _class = _unitOfWork.Classes.Get(id);
        //    if (_class == null)
        //        return HttpNotFound();

        //    _unitOfWork.Classes.Remove(_class);
        //    if (_unitOfWork.Commit() == 1)
        //        Utility.AddMessageToPage(this, "Data deleted successfully", MessageType.Success);
        //    else
        //        Utility.AddMessageToPage(this, "Data not deleted", MessageType.Error);

        //    return RedirectToAction("Index");
        //}

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
