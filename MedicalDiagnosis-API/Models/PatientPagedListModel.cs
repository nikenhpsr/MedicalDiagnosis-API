namespace MedicalDiagnosis_API.Models
{
    public class PatientPagedListModel
    {
        public List<PatientModel> Patients { get; set; } = new List<PatientModel>();
        public PageInfoModel Pagination { get; set; }
    }

}
