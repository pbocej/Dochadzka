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

        [HttpGet("create")]
        public ActionResult<Employee> Create()
        {
            ViewData["Title"] = "Create new Employee";
            ViewData["Positions"] = _apiClient.GetPositions();
            return View(new Employee());
        }
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            int employeeId;
            try
            {
                if (ModelState.IsValid)
                {
                    employeeId = await _apiClient.CreateEmployee(employee);
                    if (employeeId != 0)
                    {
                        TempData["success"] = $"Employee {employee.Name} {employee.SurName} created.";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (DataException ex)
            {
                Utils.ParseRequestException(ex, ModelState);
            }

            return View(employee);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Employee? employee = await _apiClient.GetEmployee(id);
                return employee == null ? NotFound() : View(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
