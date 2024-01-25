/// <summary>
/// ApplicationListView handles updating a ListView with application data.
/// </summary>
public class ApplicationListView
{
    private ListView _listView;

    public ApplicationListView(ListView listView)
    {
        _listView = listView;
    }

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