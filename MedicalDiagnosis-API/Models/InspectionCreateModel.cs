using MedicalDiagnosisAPI.Models;

namespace MedicalDiagnosis_API.Models
{
    public class InspectionCreateModel
    {
        [Required]
        public DateTime Date { get; set; } // Assuming date is the date and time of the inspection in UTC

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string Anamnesis { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string Complaints { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string Treatment { get; set; }

        [Required]
        public Conclusion Conclusion { get; set; }

        public DateTime? NextVisitDate { get; set; } // Nullable for optional next visit date

        public DateTime? DeathDate { get; set; } // Nullable for optional death date

        public Guid? PreviousInspectionId { get; set; } // Nullable for optional previous inspection

        [Required]
        [MinLength(1)]
        public List<DiagnosisCreateModel> Diagnoses { get; set; } = new List<DiagnosisCreateModel>();

        public List<ConsultationCreateModel> Consultations { get; set; } = new List<ConsultationCreateModel>();
    }

}
