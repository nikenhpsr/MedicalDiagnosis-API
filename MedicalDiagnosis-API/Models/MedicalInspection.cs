namespace MedicalDiagnosis_API.Models
{
    public class MedicalInspection
    {
        [Key]
        public string MedicalInspectionId { get; set; }
        public DateTime? InspectionDateTime { get; set; }
        public List<string>? Anamnesis { get; set; }
        public List<string>? Complaints { get; set; }
        public List<string>? Recommendations { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public required Patient Patient { get; set; }
        public required Doctor Author { get; set; }
        public Conclusion Conclusion { get; set; }
        //public List<Diagnoses>? Diagnoses { get; set; }
        public string PatientId { get; set; }
        public string AuthorId { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }

        // Foreign key property for the parent inspection. Nullable, since an inspection might not have a parent.
        public string? ParentInspectionId { get; set; }

        // Navigation property for the parent inspection. This is the self-referencing relationship.
        public virtual MedicalInspection? ParentInspection { get; set; }

        // Collection for child inspections. This represents the one-to-many relationship in the self-reference.
        public virtual ICollection<MedicalInspection> ChildInspections { get; set; }
        public ICollection<MedicalInspectionDiagnosis> MedicalInspectionDiagnoses { get; set; }

        public MedicalInspection()
        {
            // Initialize the collections to prevent null reference exceptions
            Consultations = new HashSet<Consultation>();
            ChildInspections = new HashSet<MedicalInspection>();
            MedicalInspectionDiagnoses = new HashSet<MedicalInspectionDiagnosis>();
            //Diagnoses = new HashSet<Diagnoses>();
        }
    }
}
