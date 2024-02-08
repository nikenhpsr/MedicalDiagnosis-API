namespace MedicalDiagnosis_API.Models
{
    public class InspectionShortModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime Date { get; set; }
        public DiagnosisModel Diagnosis { get; set; }
    }

}
