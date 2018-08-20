using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using TestDAL.Core;
using TestDAL.Core.Domains;
using TestDAL.Persistence;
using TestDAL.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TestAPI.Filters;
using Common.Models;
using Common.Dto;

namespace Test.Controllers
{
    [ValidateModel]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet()]
        // GET: Classes
        public IActionResult Index()
        {
            try
            {
                var list = _unitOfWork.Students.GetAll().ToList();
                var result = _mapper.Map<List<StudentDto>>(list);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException("Listing students failed.", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        // GET: Students/5
        public IActionResult Get(int? id)
        {
            if (id == null || id.Value < 1)
                return Index();

            try
            {
                Student student = _unitOfWork.Students.Get(id.Value);
                if (student == null)
                    return NotFound();

                var result = _mapper.Map<StudentDto>(student);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Getting student {id.Value} failed.", ex);
                return StatusCode(500, id.Value);
            }
        }


        [HttpGet("ByClassId/{id}")]
        // GET: Students/5
        public IActionResult GetByClassId(int? id)
        {
            if (id == null || id.Value < 1)
                return BadRequest();

            try
            {
                IEnumerable<Student> students = _unitOfWork.Students.Find(x => x.ClassId == id);

                var result = _mapper.Map<IEnumerable<StudentDto>>(students);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Getting students for ClassId {id.Value} failed.", ex);
                return StatusCode(500, id.Value);
            }
        }


        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([FromBody] StudentModel model)
        {
            try
            {
                var student = _mapper.Map<Student>(model);

                var exists = _unitOfWork.Students.Find(x => x.Name == model.Name);
                if (exists != null)
                    return StatusCode((int)HttpStatusCode.Conflict, model);

                _unitOfWork.Students.Add(student);
                if (_unitOfWork.Commit() == 1)
                    return Created("", student);
                else
                    return Accepted(student);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException("Creating student failed.", ex, model);
                return StatusCode(500, model);
            }
        }

        [HttpPut("{id}")]
        // PUT: Students/5
        public IActionResult Edit(int id, [FromBody] StudentModel model)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                var exists = _unitOfWork.Students.Find(x => x.Name == model.Name);
                if (exists != null)
                    return StatusCode((int)HttpStatusCode.Conflict, model);

                Student student = _unitOfWork.Students.Get(id);
                if (student == null)
                    return NotFound();

                _mapper.Map(model, student);
                student.Id = id;

                _unitOfWork.Students.Edit(student);
                if (_unitOfWork.Commit() == 1)
                    return Ok(student);
                else
                    return Accepted(student);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Editing student {id} failed.", ex, model);
                return StatusCode(500, model);
            }
        }

        [HttpDelete("{id}")]
        // Delete: Students/5
        public IActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                Student student = _unitOfWork.Students.Get(id);
                if (student == null)
                    return NotFound();

                _unitOfWork.Students.Remove(student);
                if (_unitOfWork.Commit() == 1)
                    return NoContent();
                else
                    return Accepted(student);
            }
            catch (Exception ex)
            {
                //Log
                return StatusCode(500, id);
            }
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
