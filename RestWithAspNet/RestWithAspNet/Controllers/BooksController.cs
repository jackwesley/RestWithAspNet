using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Business;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Base;
using Tapioca.HATEOAS;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(List<BookVO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookVO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<string> Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(BookVO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();

            return new ObjectResult(_bookBusiness.Create(book));
        }

        // PUT api/values/5
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(BookVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();
            var response = _bookBusiness.Update(book);
            if (response == null)
                return NoContent();

            return new ObjectResult(response);
        }

        // Patch api/values/5
        [HttpPatch]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType(typeof(BookVO), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(429)]
        [ProducesResponseType(500)]
        public IActionResult Patch([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();
            var response = _bookBusiness.Update(book);
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
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
    }
}