namespace SequenceGenerator.Services.Interfaces
{
    public interface IGenerateRandomString
    {
        string GenerateRandomAplhaNumericString(int length = 30);
    }
}
