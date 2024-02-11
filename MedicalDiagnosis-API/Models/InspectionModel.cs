namespace MedicalDiagnosis_API.Models
{
    public class InspectionModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime Date { get; set; }

        public string Anamnesis { get; set; }

        public string Complaints { get; set; }

        public string Treatment { get; set; }

        [Required]
        public Conclusion Conclusion { get; set; }

        public DateTime? NextVisitDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public Guid? BaseInspectionId { get; set; }

        public Guid? PreviousInspectionId { get; set; }

        [Required]
        public PatientModel Patient { get; set; }

        public Guid DoctorId { get; set; }
        [Required]
        public DoctorModel Doctor { get; set; }
        public Guid PatientId { get; set; }

        public virtual ConsultationModel Consultation { get; set; }

        public List<DiagnosisModel> Diagnoses { get; set; } = new List<DiagnosisModel>();

        public List<InspectionConsultationModel> Consultations { get; set; } = new List<InspectionConsultationModel>();
    }

}
