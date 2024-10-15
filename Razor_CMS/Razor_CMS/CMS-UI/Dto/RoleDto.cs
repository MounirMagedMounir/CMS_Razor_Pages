namespace CMS_UI.Dto
{
	public class RoleDto : BaseEntityDto
	{
		public string name { get; set; }

		public ICollection<PremissionDto>? permissions { get; set; }
	}
}
