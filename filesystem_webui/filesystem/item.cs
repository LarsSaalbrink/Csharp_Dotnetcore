public interface IWebItem
{
    string Name { get; }
    string Path { get; }
    long Size { get; }
    string type { get; }
    void Delete();
}