namespace Alpheus_API.Helpers.Crypto.Interfaces
{
    public interface ICrypto
    {
        string Encrypt(string word);
        string Decrypt(string word);   
    }
}
