using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardiasAPI.Jobs.Models.Context
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions options) : base(options)
        {
        }

        //Entidades
        public DbSet<Job> Jobs { get; set; }
    }
}
