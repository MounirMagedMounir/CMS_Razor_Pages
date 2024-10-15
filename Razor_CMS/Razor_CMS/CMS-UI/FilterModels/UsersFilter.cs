namespace CMS_UI.FilterModels
{
    public class UsersFilter : BaseFilterEntity
    {
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? IsActive { get; set; }

        public string? RoleName { get; set; }

    }
}
