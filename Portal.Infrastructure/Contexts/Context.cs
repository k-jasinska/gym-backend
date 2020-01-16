using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Carnet> Carnets { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<CarnetType> CarnetTypes { get; set; }
        public DbSet<Entrance> Entrances { get; set; }
        public DbSet<PersonTraining> PersonTrainings { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
    }

}
