using HomeApp.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeApp.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// Object Relational Mapper (ORM)
    /// Use migration command for updating the database: Add-Migration Initial -OutputDir "Repository/EntityFramework/Migrations"
    /// For execute the migrations to the database: Update-Database
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
