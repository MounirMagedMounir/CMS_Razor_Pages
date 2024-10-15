
using CMS_UI.Dto;

namespace CMS_UI.Dto
{
	public class RolePermissionDto
	{
		public string Id { get; set; }
		public string RoleId { get; set; } = "1";
		public string PermissionId { get; set; } = "1";
		public virtual RoleDto Role { get; set; }
		public virtual PremissionDto Permission { get; set; }
	}
}
