
/// <summary>
/// Represents data about an application that is being tracked, including the application name and the total time spent using it.
/// </summary>
public class ApplicationData
{
    /// <summary>
    /// Gets or sets the name of the application.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the time spent on the application.
    /// </summary>
    public TimeSpan TimeSpent { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationData"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the application.</param>
    public ApplicationData(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}