namespace ApiJWTManual.Dtos.Custom
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public string Msg { get; set; }
    }
}