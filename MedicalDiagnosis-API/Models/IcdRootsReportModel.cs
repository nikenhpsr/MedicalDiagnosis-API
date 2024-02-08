namespace MedicalDiagnosis_API.Models
{
    public class IcdRootsReportModel
    {
        public IcdRootsReportFiltersModel Filters { get; set; }
        public List<IcdRootsReportRecordModel> Records { get; set; } = new List<IcdRootsReportRecordModel>();
        public Dictionary<string, int> SummaryByRoot { get; set; } = new Dictionary<string, int>();
    }

}
