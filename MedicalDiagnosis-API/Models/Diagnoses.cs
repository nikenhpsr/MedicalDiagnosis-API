namespace MedicalDiagnosis_API.Models
{
    public class Diagnoses
    {
        [Key]
        public required string DiagnosesId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public TypeInInspection Type { get; set; }
        public string MedicalInspectionId { get; set; }
        public virtual MedicalInspection MedicalInspection { get; set; }

        public ICollection<MedicalInspectionDiagnosis> MedicalInspectionDiagnoses { get; set; }

        public Diagnoses()
        {
            MedicalInspectionDiagnoses = new HashSet<MedicalInspectionDiagnosis>();
        }
    }
}
