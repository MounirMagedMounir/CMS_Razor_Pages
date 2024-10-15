namespace CMS_UI.FilterModels
{
    public class RolesFilter : BaseFilterEntity
    {
        public string? Name { get; set; }
        public ICollection<PermissionsFilter>? Permissions { get; set; } = new List<PermissionsFilter>();
    }
}
