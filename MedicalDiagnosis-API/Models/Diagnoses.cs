namespace MedicalDiagnosis_API.Models
{
    public class Diagnoses
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public TypeInInspection Type { get; set; }
    }
}
