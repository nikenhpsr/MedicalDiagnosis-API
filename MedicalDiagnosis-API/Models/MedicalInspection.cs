namespace MedicalDiagnosis_API.Models
{
    public class MedicalInspection
    {
        public DateTime? InspectionDateTime { get; set; }
        public List<string>? Anamnesis { get; set; }
        public List<string>? Complaints { get; set; }
        public List<string>? Recommendations { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public required Patient Patient { get; set; }
        public required Doctor Author { get; set; }
        public Conclusion Conclusion { get; set; }
        public List<Diagnoses>? Diagnoses { get; set; }
    }
}
