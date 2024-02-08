namespace MedicalDiagnosis_API.Models
{
    public class SpecialtiesPagedListModel
    {
        public List<SpecialityModel> Specialties { get; set; } = new List<SpecialityModel>();
        public PageInfoModel Pagination { get; set; }
    }

}
