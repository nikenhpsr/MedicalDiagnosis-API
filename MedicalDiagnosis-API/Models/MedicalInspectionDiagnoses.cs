namespace MedicalDiagnosis_API.Models
{
    public class MedicalInspectionDiagnosis
    {
        [Key]
        public string MedicalInspectionDiagnosisId { get; set; }
        public string MedicalInspectionId { get; set; }
        public virtual MedicalInspection MedicalInspection { get; set; }

        public string DiagnosesId { get; set; }
        public virtual Diagnoses Diagnoses { get; set; }
    }
}
