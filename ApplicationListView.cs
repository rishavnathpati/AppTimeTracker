/// <summary>
/// ApplicationListView handles updating a ListView with application data.
/// </summary>
public class ApplicationListView
{
    private ListView _listView;

    /// <summary>
    /// Represents a view for displaying applications in a ListView control.
    /// </summary>
    public ApplicationListView(ListView listView)
    {
        _listView = listView;
    }

    /// <summary>
    /// Updates the application list view with the provided list of application data.
    /// </summary>
    /// <param name="apps">The list of application data to update the view with.</param>
    public void Update(List<ApplicationData> apps)
    {
        _listView.BeginUpdate();
        _listView.Items.Clear();

        foreach (var app in apps)
        {
            ListViewItem item = new ListViewItem(app.Name);
            item.SubItems.Add(app.TimeSpent.ToString(@"hh\:mm\:ss"));
            _listView.Items.Add(item);
        }

        _listView.EndUpdate();
    }
}