using Microsoft.EntityFrameworkCore;
using project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace code1stapproach
{
    public  class FacStuContext : DbContext
    {
        public DbSet<Faculty> fac { get; set; }

        public DbSet<Student > stu  { get; set; }
        public DbSet<User> Users { get; set; }

        public FacStuContext(DbContextOptions<FacStuContext> options) : base(options)
        {
           
        }


    }
}
