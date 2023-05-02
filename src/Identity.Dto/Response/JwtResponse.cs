namespace Identity.Dto.Response
{
    public class JwtResponse
    {
        public string Id { get; set; }
        public string Auth_Token { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }
    }
}
