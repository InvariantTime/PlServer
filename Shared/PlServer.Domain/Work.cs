namespace PlServer.Domain;

public class Work
{
    public int Payload { get; init; }

    public string Name { get; init; } = string.Empty;

    public Work(int payload, string name)
    {
        Payload = payload;
        Name = name;
    }

    public Work()
    {
        Payload = 0;
        Name = string.Empty;
    }
}
