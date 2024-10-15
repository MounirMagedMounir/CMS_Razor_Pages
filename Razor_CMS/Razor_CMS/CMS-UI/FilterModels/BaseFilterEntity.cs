
namespace CMS_UI.FilterModels
{
    public class BaseFilterEntity 
    {
        public string? Id { get; set; }

        public DateTime? CreatedDateFrom { get; set; }

        public DateTime? CreatedDateTo { get; set; } 

        public DateTime? LastUpdatedDateFrom { get; set; } 

        public DateTime? LastUpdatedDateTo { get; set; }

        public string? CreatedbyId { get; set; }

        public string? LastUpdatedbyId { get; set; }



    }
}
