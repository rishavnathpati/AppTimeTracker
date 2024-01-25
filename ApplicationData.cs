public class ApplicationData
{
    public string Name { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public ApplicationData(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}