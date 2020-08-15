using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcore_reactapi.Models
{
    public class Employee
    {
        [Key]
        public int empid { get; set; }
        [Column (TypeName ="varchar(100)")]
        public string empname { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string address { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string mobile { get; set; }
        public int age { get; set; }
    }
}
