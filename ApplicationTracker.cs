using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class ApplicationTracker
{
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("user32.dll")]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

    public string GetActiveWindowTitle()
    {
        IntPtr handle = GetForegroundWindow();
        if (handle == IntPtr.Zero) return "";

        uint processId;
        GetWindowThreadProcessId(handle, out processId);

        Process foregroundProcess = Process.GetProcessById((int)processId);
        if (foregroundProcess.MainModule != null)
        {
            return Path.GetFileNameWithoutExtension(foregroundProcess.MainModule.FileName);
        }
        else
        {
            return "";
        }    }
}

[StructLayout(LayoutKind.Sequential)]
public struct LASTINPUTINFO
{
    public uint cbSize;
    public uint dwTime;
}