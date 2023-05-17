using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace project
{
    public class Student
    {
        [Key]
        public int? Stuid { get; set; }

        public string? Stuname { get; set; }

        public string? Stucourse { get; set; }

        public string? Section { get; set; }

        public string? Gender { get; set; }

        public string? Mail { get; set; }


        public Faculty ? Faculty { get; set; }


    }
}
