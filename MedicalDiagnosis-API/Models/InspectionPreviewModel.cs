namespace MedicalDiagnosis_API.Models
{
    public class InspectionPreviewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid? PreviousId { get; set; }
        public DateTime Date { get; set; }
        public Conclusion Conclusion { get; set; }
        public Guid DoctorId { get; set; }
        public string Doctor { get; set; }
        public Guid PatientId { get; set; }
        public string Patient { get; set; }
        public DiagnosisModel Diagnosis { get; set; }
        public bool HasChain { get; set; }
        public bool HasNested { get; set; }
    }

}
