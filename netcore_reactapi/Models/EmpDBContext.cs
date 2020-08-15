using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore_reactapi.Models
{
    public class EmpDBContext : DbContext
    {
        public EmpDBContext(DbContextOptions<EmpDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employeedb {get; set;}
        
    }
}
