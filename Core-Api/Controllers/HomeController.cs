using BAL.Models;
using DAL.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using BAL;

namespace Core_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        public HomeController(IEmployeeRepository employee, IDepartmentRepository department)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            APIResponse output = new APIResponse();
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employee.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employee.InsertEmployee(emp);
            if (result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpPost]
        [Route("DeleteEmployee")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int[] id)
        {
            var result = _employee.DeleteEmployee(id);
            return new JsonResult("Deleted Successfully");
        }

        //[Route("SaveFile")]
        //[HttpPost]
        //public JsonResult SaveFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            stream.CopyTo(stream);
        //        }

        //        return new JsonResult(filename);
        //    }
        //    catch (Exception)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartment());
        }
    }
}
