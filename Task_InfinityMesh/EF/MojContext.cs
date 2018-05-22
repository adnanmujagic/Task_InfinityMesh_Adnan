using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Task_InfinityMesh.Models;

namespace Task_InfinityMesh.EF
{
    public class MojContext : DbContext
    {
        public MojContext() : base("MojConnectionString")
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Blogs> Blogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}