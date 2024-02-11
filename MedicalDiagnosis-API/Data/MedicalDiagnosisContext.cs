namespace MedicalDiagnosis_API.Models
{
    public class MedicalDiagnosisContext : DbContext
    {
        public MedicalDiagnosisContext (DbContextOptions<MedicalDiagnosisContext> options)
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
            modelBuilder.Entity<InspectionConsultationModel>()
                .HasKey(ic => new { ic.InspectionId, ic.ConsultationId });

            modelBuilder.Entity<InspectionConsultationModel>()
                .HasOne(ic => ic.Inspection)
                .WithMany(i => i.Consultations)
                .HasForeignKey(ic => ic.InspectionId);

            modelBuilder.Entity<InspectionConsultationModel>()
                .HasOne(ic => ic.Consultation)
                .WithMany() // If ConsultationModel doesn't have a navigation property back to InspectionConsultationModel, leave this blank
                .HasForeignKey(ic => ic.ConsultationId);

            // Configuring the one-to-many relationship between DiagnosisModel and MedicalInspection
            modelBuilder.Entity<DiagnosisModel>()
                .HasOne(d => d.Inspection)
                .WithMany(m => m.Diagnoses)
                .HasForeignKey(d => d.InspectionModelId);

            // Configuring the one-to-many relationship between MedicalInspection and PatientModel
            modelBuilder.Entity<InspectionModel>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Inspections)
                .HasForeignKey(m => m.PatientId);

            // Configuring the one-to-many relationship between MedicalInspection and DoctorModel
            modelBuilder.Entity<InspectionModel>()
                .HasOne(m => m.Doctor)
                .WithMany(d => d.Inspections)
                .HasForeignKey(m => m.DoctorId);

            // Configuring the one-to-one relationship between MedicalInspectionModel and ConsultationModel
            modelBuilder.Entity<InspectionModel>()
                .HasOne(ic => ic.Consultation)
                .WithOne(c => c.Inspection)
                .HasForeignKey<InspectionConsultationModel>(ic => ic.ConsultationId);

            // Configuring the one-to-one relationship between ConsultationModel and SpecialityModel
            modelBuilder.Entity<ConsultationModel>()
                .HasOne(c => c.Speciality)
                .WithMany(s => s.Consultations)
                .HasForeignKey(c => c.SpecialityId);

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
