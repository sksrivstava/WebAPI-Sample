using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcore_reactapi.Models;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netcore_reactapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository ERepository;
        public EmployeeController(IEmployeeRepository _ERepository)
        {
            ERepository = _ERepository;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        
        {
            try
            {
                var employees = await ERepository.GetEmployeelst();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        // GET api/<EmployeeController>/5
        [HttpGet]
        [Route("GetEmp")]
        public async Task<IActionResult> GetEmp(int ? Empid)
        {
            if (Empid == null)
            {
                return BadRequest();
            }

            try
            {
                var Employeeobj = await ERepository.GetEmployee(Empid);

                if (Employeeobj == null)
                {
                    return NotFound();
                }

                return Ok(Employeeobj);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var EmpId = await ERepository.AddEmployee(model);
                    if (EmpId > 0)
                    {
                        return Ok(EmpId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }


        // PUT api/<EmployeeController>/5
        [HttpPut]
        [Route("updateemployee")]
        public async Task<IActionResult> updateemployee([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ERepository.UpdateEmployee(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE api/<EmployeeController>/5
       [HttpDelete]
        [Route("deleteemployee")]
        public async Task<IActionResult> deleteemployee(int? id)
        {
            int result = 0;

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await ERepository.DeleteEmployee(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
