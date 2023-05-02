namespace Identity.Sdk.Lib.Session
{
    public interface IUserSession
    {
        String Id { get; }
        String Email { get; }
        IEnumerable<String> Roles { get; }        
        string GetToken();
    }
}
