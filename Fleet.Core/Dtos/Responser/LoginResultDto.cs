namespace Fleet.Core.Dtos.Responser
{
    public class LoginResultDto : BaseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}