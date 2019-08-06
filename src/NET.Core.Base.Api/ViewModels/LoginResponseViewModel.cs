namespace NET.Core.Base.Api.ViewModels
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public UserTokenViewModel UserToken { get; set; }
    }
}
