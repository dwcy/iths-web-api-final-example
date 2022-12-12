using ITHS.Application.Services;
using ITHS.Application.ViewModels.Users;
using ITHS.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITHS.Webapi.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        PersonService _personService;
        
        public PersonsController()
        {
         
        }

        [HttpGet]
        public IActionResult Get(string firstName)
        {
            var person = _personService.FindPersonsByFirstName(firstName);
            
            return Ok(person);
        }

        [HttpPost]
        public IActionResult AddPerson(PersonCreateRequest person)
        {
            _personService.AddPerson(person);
            
            return Ok();
        }
    }
}
