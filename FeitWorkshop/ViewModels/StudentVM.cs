using FeitWorkshop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeitWorkshop.ViewModels
{
    public class StudentVM
    {
        public IList<Student> Studens { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Id { get; set; }
    }
}
