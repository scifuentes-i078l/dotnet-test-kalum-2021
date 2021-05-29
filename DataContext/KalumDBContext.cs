using System.IO;
using Kalum2021.Models;
using Kalum2021.ModelView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kalum2021.DataContext
{
    public class KalumDBContext :  DbContext
    {
        
        public DbSet<Alumno> Alumnos {get;set;}
        public DbSet<Instructor> Instructores {get;set;}
        public DbSet<Clase> Clases {get;set;}

         public DbSet<Horario> Horarios {get;set;}
        public DbSet<Salon> Salones {get;set;}
        public DbSet<CarreraTecnica> CarrerasTecnicas {get;set;}
        public KalumDBContext(DbContextOptions<KalumDBContext> options)
            :base(options)
        {
            
        }

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration  = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
            .ToTable(name: "Alumnos")
            .HasKey(c => new {c.Carne});
            modelBuilder.Entity<Instructor>()
            .ToTable(name: "Instructores")
            .HasKey(f => new {f.InstructorId});
            modelBuilder.Entity<Clase>()
            .ToTable(name: "Clases")
            .HasKey(g => new {g.ClaseId});

            modelBuilder.Entity<Horario>()
            .ToTable(name: "Horarios")
            .HasKey(h => new {h.HorarioId});
            modelBuilder.Entity<Salon>()
            .ToTable(name: "Salones")
            .HasKey(b => new {b.SalonId});
            modelBuilder.Entity<CarreraTecnica>()
            .ToTable(name: "CarrerasTecnicas")
            .HasKey(e => new {e.CarreraId});
        }

        public KalumDBContext()
        {
            
        }
    }
}