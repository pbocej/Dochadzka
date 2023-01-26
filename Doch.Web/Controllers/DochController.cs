using Doch.Models;
using Doch.Web.Code;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doch.Models.Exceptions;

namespace Doch.Web.Controllers
{
    [Controller]
    public class DochController : Controller
    {
        private readonly IApiClient _apiClient;
        public DochController(IApiClient client) 
        {
            _apiClient = client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Index()
        {
            ViewData["Title"] = "Dochadzka";
            ViewData["APIBaseUrl"] = _apiClient.APIBaseUrl;
            try
            {
                IEnumerable<Employee> workHours = await _apiClient.GetEmployeeList();
                return View(workHours);
            }
            catch (DataException ex)
            {
                TempData["error"] = ex.Message;
                return BadRequest();
            }
        }
    }
}
