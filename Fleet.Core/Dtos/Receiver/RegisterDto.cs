namespace Fleet.Core.Dtos
{
    public class RegisterDto : BaseDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}