namespace Fleet.Core.Dtos
{
    /// <summary>
    /// Zwracany obiekt logowania API
    /// </summary>
    public class LoginDto : BaseDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}