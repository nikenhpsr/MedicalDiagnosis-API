namespace MedicalDiagnosis_API.Models
{
    public class Consultation
    {
        public DateTime CreateDateTime { get; set; }
        public required MedicalInspection MedicalInspection { get; set; }
    }

}
