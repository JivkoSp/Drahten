namespace DrahtenWeb.Dtos
{
    public class AuthInfoDto
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string grant_type { get; set; } = string.Empty;
    }
}
