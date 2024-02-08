namespace MedicalDiagnosis_API.Models
{
    public class IcdRootsReportRecordModel
    {
        public string PatientName { get; set; }
        public DateTime? PatientBirthdate { get; set; }
        public Gender Gender { get; set; }
        public Dictionary<string, int> VisitsByRoot { get; set; } = new Dictionary<string, int>();
    }

}
