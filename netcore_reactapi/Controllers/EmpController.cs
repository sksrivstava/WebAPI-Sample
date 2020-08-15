using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using netcore_reactapi.Models;

namespace netcore_reactapi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EmpController : Controller
    {
        private EmpDBContext _context;

        public EmpController(EmpDBContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var employeedb = _context.Employeedb.Select(i => new {
                i.empid,
                i.empname,
                i.address,
                i.mobile,
                i.age
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "empid" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(employeedb, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Employee();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Employeedb.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.empid });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Employeedb.FirstOrDefaultAsync(item => item.empid == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Employeedb.FirstOrDefaultAsync(item => item.empid == key);

            _context.Employeedb.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Employee model, IDictionary values) {
            string EMPID = nameof(Employee.empid);
            string EMPNAME = nameof(Employee.empname);
            string ADDRESS = nameof(Employee.address);
            string MOBILE = nameof(Employee.mobile);
            string AGE = nameof(Employee.age);

            if(values.Contains(EMPID)) {
                model.empid = Convert.ToInt32(values[EMPID]);
            }

            if(values.Contains(EMPNAME)) {
                model.empname = Convert.ToString(values[EMPNAME]);
            }

            if(values.Contains(ADDRESS)) {
                model.address = Convert.ToString(values[ADDRESS]);
            }

            if(values.Contains(MOBILE)) {
                model.mobile = Convert.ToString(values[MOBILE]);
            }

            if(values.Contains(AGE)) {
                model.age = Convert.ToInt32(values[AGE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}