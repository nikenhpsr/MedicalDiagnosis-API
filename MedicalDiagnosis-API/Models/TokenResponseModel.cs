namespace MedicalDiagnosis_API.Models
{
    public class TokenResponseModel
    {
        [Required]
        [MinLength(1)]
        public string Token { get; set; }
    }

}
