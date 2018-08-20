using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using TestDAL.Core;
using TestDAL.Persistence.Contexts;
using TestDAL.Persistence;
using AutoMapper;
using TestDAL.Core.Domains;
using Common.Dto;
using TestAPI.Filters;

namespace TestAPI.Controllers
{
    [ValidateModel]
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet()]
        // GET: Classes
        public IActionResult Index()
        {
            try { 
            var list = _unitOfWork.Classes.GetAll().ToList();
            var result = _mapper.Map<List<ClassDto>>(list);

            return Ok(result);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException("Listing classes failed.", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        // GET: Classes/5
        public IActionResult Get(int? id)
        {
            if (id == null || id.Value < 1)
                return Index();

            try { 
            Class _class = _unitOfWork.Classes.Get(id.Value);
            if (_class == null)
                return NotFound();

            var result = _mapper.Map<ClassDto>(_class);

            return Ok(result);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Getting class {id.Value} failed.", ex);
                return StatusCode(500, id.Value);
            }
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([FromBody] ClassModel model)
        {
            try
            {
                var _class = _mapper.Map<Class>(model);

                _unitOfWork.Classes.Add(_class);
                if (_unitOfWork.Commit() == 1)
                    return Created("", _class);
                else
                    return Accepted(_class);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Creating class failed.", ex, model);
                return StatusCode(500, model);
            }
        }

        [HttpPut("{id}")]
        // PUT: Classes/5
        public IActionResult Edit(int id, [FromBody] ClassModel model)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                Class _class = _unitOfWork.Classes.Get(id);
                if (_class == null)
                    return NotFound();

                _mapper.Map(model, _class);
                _class.Id = id;

                _unitOfWork.Classes.Edit(_class);
                if (_unitOfWork.Commit() == 1)
                    return Ok(_class);
                else
                    return Accepted(_class);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Editing class {id} failed .", ex, model);
                return StatusCode(500, model);
            }
        }
        
        [HttpDelete("{id}")]
        // Delete: Classes/5
        public IActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                Class _class = _unitOfWork.Classes.Get(id);
                if (_class == null)
                    return NotFound();

                _unitOfWork.Classes.Remove(_class);
                if (_unitOfWork.Commit() == 1)
                    return NoContent();
                else
                    return Accepted(_class);
            }
            catch (Exception ex)
            {
                Common.Logger.LogException($"Deleting class {id} failed.", ex);
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
