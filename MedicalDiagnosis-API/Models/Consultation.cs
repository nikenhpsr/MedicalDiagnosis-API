namespace MedicalDiagnosis_API.Models
{
    public class Consultation
    {
        [Key]
        public required string ConsultationId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public required MedicalInspection MedicalInspection { get; set; }
        public string MedicalInspectionId { get; set; }
        public virtual MedicalInspection MedicalInspections { get; set; }

    }

}
