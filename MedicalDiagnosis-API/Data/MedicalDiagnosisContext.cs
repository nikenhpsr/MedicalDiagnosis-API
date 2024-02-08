using Microsoft.EntityFrameworkCore;
using MedicalDiagnosis_API.Models;

namespace MedicalDiagnosis_API.Data
{
    public class MedicalDiagnosisContext : DbContext
    {
        public MedicalDiagnosisContext(DbContextOptions<MedicalDiagnosisContext> options)
            : base(options)
        {
        }

        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnoses> Diagnoses { get; set; }
        public DbSet<MedicalInspection> MedicalInspections { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints, relationships, and other configurations are set up here.

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId);

            // Relationship between Patient and MedicalInspection
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.MedicalInspections)
                .WithOne(mi => mi.Patient)
                .HasForeignKey(mi => mi.PatientId);

            // Relationship between Doctor and MedicalInspection (as the author)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.MedicalInspections)
                .WithOne(mi => mi.Author)
                .HasForeignKey(mi => mi.AuthorId);

            // Relationship between MedicalInspection and Diagnoses
            modelBuilder.Entity<MedicalInspection>()
                 .HasMany(mi => mi.MedicalInspectionDiagnoses)
                 .WithOne(mid => mid.MedicalInspection)
                 .HasForeignKey(mid => mid.MedicalInspectionId);

            modelBuilder.Entity<Diagnoses>()
                .HasMany(d => d.MedicalInspectionDiagnoses)
                .WithOne(mid => mid.Diagnoses)
                .HasForeignKey(mid => mid.DiagnosesId);

            // Relationship between MedicalInspection and Consultation
            modelBuilder.Entity<MedicalInspection>()
                .HasMany(mi => mi.Consultations)
                .WithOne(c => c.MedicalInspection)
                .HasForeignKey(c => c.MedicalInspectionId);

            // Relationship between Doctor and Consultation (as the commenter)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Comments)
                .WithOne(c => c.CommentAuthor)
                .HasForeignKey(c => c.CommentAuthorId);


            // Configure the self-reference for MedicalInspection if needed
            modelBuilder.Entity<MedicalInspection>()
                .HasOne(mi => mi.ParentInspection)
                .WithMany()
                .HasForeignKey(mi => mi.ParentInspectionId)
                .OnDelete(DeleteBehavior.Restrict); // To prevent cascade delete issues

            modelBuilder.Entity<Diagnoses>()
                .HasKey(d => d.DiagnosesId);

            modelBuilder.Entity<MedicalInspectionDiagnosis>()
                .HasKey(mid => new { mid.MedicalInspectionId, mid.DiagnosesId });

            modelBuilder.Entity<MedicalInspectionDiagnosis>()
                .HasOne(mid => mid.MedicalInspection)
                .WithMany(mi => mi.MedicalInspectionDiagnoses)
                .HasForeignKey(mid => mid.MedicalInspectionId);

            modelBuilder.Entity<MedicalInspectionDiagnosis>()
                 .HasOne(mid => mid.Diagnoses)
                 .WithMany(d => d.MedicalInspectionDiagnoses)
                 .HasForeignKey(mid => mid.DiagnosesId);

            // Constraint for MedicalInspection to have exactly one main diagnosis
            modelBuilder.Entity<MedicalInspection>()
                .HasCheckConstraint("CK_MedicalInspection_MainDiagnosis",
                    "SELECT COUNT(*) FROM Diagnoses WHERE TypeInInspection = 'Main' AND MedicalInspectionId = Id GROUP BY MedicalInspectionId HAVING COUNT(*) = 1");

            // Constraint for MedicalInspection based on Conclusion
            modelBuilder.Entity<MedicalInspection>()
                .HasCheckConstraint("CK_MedicalInspection_Conclusion_NextVisit",
                    "([Conclusion] = 'Disease' AND [NextVisitDate] IS NOT NULL) OR ([Conclusion] <> 'Disease')")
                .HasCheckConstraint("CK_MedicalInspection_Conclusion_DeathDate",
                    "([Conclusion] = 'Death' AND [DeathDate] IS NOT NULL) OR ([Conclusion] <> 'Death')");
        }
    }
}
