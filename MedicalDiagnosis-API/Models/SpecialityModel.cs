namespace MedicalDiagnosis_API.Models
{
    public class SpecialityModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<ConsultationModel> Consultations { get; set; } = new List<ConsultationModel>();
    }
}


