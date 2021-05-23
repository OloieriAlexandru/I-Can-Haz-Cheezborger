namespace DataAccess.Abstractions
{
    public interface IFileRepository
    {
        public byte[] Get(string fileName);

        public void Create(string fileName, byte[] fileContent);
    }
}
