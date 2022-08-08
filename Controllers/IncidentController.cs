using BartsolutionsWebAPI.DTOs;
using BartsolutionsWebAPI.Models;
using BartsolutionsWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BartsolutionsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        [Route("createContact")]
        public async Task<IActionResult> CreateContactAsync([FromBody] NewContact request)
        {
            return Ok(await _incidentService.CreateContactAsync(request));
        }      

        [HttpPost]
        [Route("createAccount")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] NewAccount request)
        {
            return Ok(await _incidentService.CreateAccountAsync(request));
        }

        [HttpPost]
        [Route("createIncident")]
        public async Task<IActionResult> CreateIncedentAsync([FromBody] IncidentRequest request)
        {
            var result = await _incidentService.CreateIncidentAsync(request);
            if (result.Response is null)
                return NotFound("Account is not in the system! Enter an existing account name!");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllContacts")]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            return Ok(await _incidentService.GetAllContactsAsync());
        }

        [HttpGet]
        [Route("getAllAccounts")]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            return Ok(await _incidentService.GetAllAccountsAsync());
        }

        [HttpGet]
        [Route("GetAllIncidents")]
        public async Task<IActionResult> GetAllIncidentsAsync()
        {
            return Ok(await _incidentService.GetAllIncidentsAsync());
        }
    }
}
