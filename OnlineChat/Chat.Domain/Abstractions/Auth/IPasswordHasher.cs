namespace Chat.Domain.Abstractions.Auth
{
    public interface IPasswordHasher
    {
        string Generate(string password);

        public bool Verify(string password, string passwordHash);
    }
}