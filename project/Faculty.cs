using System.ComponentModel.DataAnnotations;

namespace project
{
    public class Faculty
    {
        [Key]
        public int ? Facid { get; set; }

        public string ?Facname { get; set;}

        public string ?Salary { get; set;}

        public string? Subject { get; set;}



        public ICollection<Student>?Student { get; set;}
    }
}
