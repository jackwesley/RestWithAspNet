using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Model;
using RestWithAspNet.Business;
using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Data.VO;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // GET api/values
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public ActionResult Get()
        {

            return Ok(_personBusiness.FindAll());
        }

        // GET api/values
        [HttpGet("find-by-name")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public ActionResult GetByName([FromQuery] string firstName, string lastName)
        {
            var persons = _personBusiness.FindByName(firstName, lastName);
            if (persons != null && persons.Any())
                return Ok(persons);

            return NotFound();
        }

        // GET api/values
        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public ActionResult GetPagedSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            var persons = _personBusiness.FindWithPagedSearch(name: name,
                                                              sortDirection: sortDirection,
                                                              page: page,
                                                              pageSize: pageSize);
            if (persons.List != null && persons.List.Any())
                return Ok(persons);

            return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        [ProducesResponseType(typeof(PersonVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public ActionResult<string> Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(PersonVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();

            return new ObjectResult(_personBusiness.Create(person));

        }

        // PUT api/values/5
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(PersonVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();
            var response = _personBusiness.Update(person);
            if (response == null)
                return NoContent();

            return new ObjectResult(response);
        }

        // PATCH api/values/5
        [HttpPatch]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(PersonVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public IActionResult Patch([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();
            var response = _personBusiness.Update(person);
            if (response == null)
                return NoContent();

            return new ObjectResult(response);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);

            return NoContent();
        }
    }
}
