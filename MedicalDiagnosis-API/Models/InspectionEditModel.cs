namespace MedicalDiagnosis_API.Models
{
    public class InspectionEditModel
    {
        public string Anamnesis { get; set; } // Nullable for optional anamnesis

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

        [Required]
        [MinLength(1)]
        public List<DiagnosisCreateModel> Diagnoses { get; set; } = new List<DiagnosisCreateModel>();
    }

}
