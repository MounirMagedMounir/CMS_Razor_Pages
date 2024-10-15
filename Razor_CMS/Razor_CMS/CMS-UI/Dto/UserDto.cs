
namespace CMS_UI.Dto
{
	public class UserDto : BaseEntityDto
	{
		public string name { get; set; }

		public string userName { get; set; }



		public string email { get; set; }



		public string phone { get; set; }



		public string password { get; set; }



		public string profileImage { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";



		public bool isActive { get; set; } = true;

		public string role { get; set; }

	}
}
