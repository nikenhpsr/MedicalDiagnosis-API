namespace MedicalDiagnosis_API.Models
{
    public class InspectionPagedListModel
    {
        public List<InspectionPreviewModel> Inspections { get; set; } = new List<InspectionPreviewModel>();
        public PageInfoModel Pagination { get; set; }
    }

}
