/// <summary>
/// Represents data about an application that is being tracked, including the application name and the total time spent using it.
/// </summary>

public class ApplicationData
{
    public string Name { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public ApplicationData(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}