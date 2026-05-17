using Cwiczenia_10.Data;
using Cwiczenia_10.DTOs;
using Cwiczenia_10.Exceptions;
using Cwiczenia_10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia_10.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PcsController : ControllerBase {
        private readonly IDbService _dbService;

        public PcsController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var pcs = await _dbService.getAllPcs();
            return Ok(pcs);
        }

        [HttpGet]
        [Route("{id}/components")]
        public async Task<IActionResult> GetPc([FromRoute] int id) {
            try {
                var pc = await _dbService.GetPc(id);
                return Ok(pc);
            }
            catch (NotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostRequest request) {
            var response = await _dbService.Post(request);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PostRequest request) {
            try {
                await _dbService.Put(id, request);
                return Ok();
            }
            catch (NotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            try {
                await _dbService.deletePcs(id);
                return NoContent();
            }
            catch (NotFoundException e) {
                return NotFound(e.Message);
            }
        }
    }
}