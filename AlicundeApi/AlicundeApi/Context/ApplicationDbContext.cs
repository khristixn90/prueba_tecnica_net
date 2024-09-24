using AlicundeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AlicundeApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bank> Bank { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
