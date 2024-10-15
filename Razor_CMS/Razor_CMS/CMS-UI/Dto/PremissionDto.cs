
namespace CMS_UI.Dto
{
    public class PremissionDto : BaseEntityDto
    {

        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
