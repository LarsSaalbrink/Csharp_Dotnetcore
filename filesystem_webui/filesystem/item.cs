public interface IWebItem
{
    string Name { get; }
    string Path { get; }
    long Size { get; }
    void Delete();
}