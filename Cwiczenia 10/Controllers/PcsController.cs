using Cwiczenia_10.Data;
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
            var pcs = await  _dbService.getAllPcs();
            return Ok(pcs);
        }
    }
}