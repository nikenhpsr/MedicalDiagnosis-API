using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MedicalDiagnosis_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for each entity
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<SpecialityModel> Specialities { get; set; }
        public DbSet<InspectionModel> Inspections { get; set; }
        public DbSet<DiagnosisModel> Diagnoses { get; set; }
        public DbSet<ConsultationModel> Consultations { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the CommentModel to have a self-referencing one-to-many relationship
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.ParentComment)
                .WithMany(p => p.InnerComments)
                .HasForeignKey(c => c.ParentCommentId) // Use the correct property name here
                .OnDelete(DeleteBehavior.Restrict); // Optional: to prevent cascade delete

            // Configuring the relationship between CommentModel and DoctorModel
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.Author)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.AuthorId);

            // Configuring the one-to-many relationship between ConsultationModel and SpecialityModel
            modelBuilder.Entity<ConsultationModel>()
                .HasOne(c => c.Speciality)
                .WithMany() // Assuming no navigation property back to ConsultationModel
                .HasForeignKey(c => c.SpecialityId);

            // Configuring the one-to-many relationship between ConsultationModel and InspectionModel
            modelBuilder.Entity<ConsultationModel>()
                .HasOne(c => c.MedicalInspection)
                .WithMany(i => i.Consultations)
                .HasForeignKey(c => c.InspectionId);

            // Configuring the one-to-many relationship between DiagnosisModel and MedicalInspection
            modelBuilder.Entity<DiagnosisModel>()
                .HasOne(d => d.MedicalInspection)
                .WithMany(m => m.Diagnoses)
                .HasForeignKey(d => d.MedicalInspectionId);

            // Configuring the one-to-many relationship between MedicalInspection and PatientModel
            modelBuilder.Entity<InspectionModel>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Inspection)
                .HasForeignKey(m => m.PatientId);

            // Configuring the one-to-many relationship between MedicalInspection and DoctorModel
            modelBuilder.Entity<InspectionModel>()
                .HasOne(m => m.Author)
                .WithMany(d => d.Inspection)
                .HasForeignKey(m => m.AuthorId);

            // Enum conversions
            modelBuilder.Entity<DiagnosisModel>()
                .Property(d => d.Type)
                .HasConversion<string>();

            modelBuilder.Entity<InspectionModel>()
                .Property(m => m.Conclusion)
                .HasConversion<string>();

            // Configuring enums for Gender
            modelBuilder.Entity<DoctorModel>()
                .Property(d => d.Gender)
                .HasConversion<string>();
        }
    }
}
