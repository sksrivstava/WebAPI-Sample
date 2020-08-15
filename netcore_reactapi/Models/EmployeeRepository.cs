using Microsoft.EntityFrameworkCore;
using netcore_reactapi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace netcore_reactapi.Models
{
    public class EmployeeRepository : IEmployeeRepository

    {
        EmpDBContext db;
        public EmployeeRepository(EmpDBContext _db)
        {
            db = _db;
        }
        public async Task<int> AddEmployee(Employee Employeeinfo)
        {
            if (db != null)
            {
                await db.Employeedb.AddAsync(Employeeinfo);
                await db.SaveChangesAsync();

                return Employeeinfo.empid;
            }

            return 0;
        }

        public async Task<int> DeleteEmployee(int? Empid)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var Employeeobj = await db.Employeedb.FirstOrDefaultAsync(x => x.empid == Empid);

                if (Employeeobj != null)
                {
                    //Delete that post
                    db.Employeedb.Remove(Employeeobj);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<List<Employee>> GetEmployeelst()
        {
            if (db != null)
            {
                List<Employee>  elist = await db.Employeedb.ToListAsync();
                return elist;
            }

            return null;

        }

        public async Task<Employee> GetEmployee(int? Empid)
        {
            if (db != null)
            {
                Employee  elist = await db.Employeedb.FirstOrDefaultAsync(x => x.empid == Empid);
                return elist;
                //    return await (from p in db.Employeedb
                //                  where p.empid == Empid
                //                  select new Employee
                //                  {
                //                      empid = p.empid,
                //                      empname = p.empname,
                //                      address = p.address,
                //                      mobile = p.mobile,
                //                      age = p.age
                //                     }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task UpdateEmployee(Employee Employeeinfo)
        {
            if (db != null)
            {
                //Delete that post
                db.Employeedb.Update(Employeeinfo);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
