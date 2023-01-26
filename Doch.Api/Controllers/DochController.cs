using Doch.Data;
using Doch.Data.Exceptions;
using Doch.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Doch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DochController : Controller
    {
        private readonly IDochRepository _repository;
        public DochController(IDochRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _repository.GetEmployees();
        }
        [HttpGet("positions")]
        public async Task<IEnumerable<Position>> GetPositions()
        {
            return await _repository.GetPositions();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee employee;
            try
            {
                employee = await _repository.GetEmployee(id);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            return employee;

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> Create([FromBody] Employee value)
        {
            Employee employee;
            try
            {
                employee = await _repository.AddEmployee(value);
            }
            catch (DataConflictException ex)
            {
                return Conflict($"{ex.FieldName}:{ex.Message}");
            }
            catch (DataValidationException ex)
            {
                return ValidationProblem($"{ex.FieldName}:{ex.Message}");
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Employee value)
        {
            Employee employee;
            try
            {
                employee = await _repository.UpdateEmployee(value);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DataConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (DataValidationException ex)
            {
                return ValidationProblem($"{ex.FieldName}:{ex.Message}");
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteEmployee(id);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}
