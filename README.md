# AppTimeTracker

AppTimeTracker is a Windows Forms application that tracks the time spent on different applications on your computer. It uses P/Invoke to call native Windows APIs to get the currently active window and calculate idle time.

## Project Structure

Here's a brief overview of the key files in this project:

- [`AppTimeTracker.csproj`](command:_github.copilot.openSymbolInFile?%5B%22AppTimeTracker.csproj%22%2C%22AppTimeTracker.csproj%22%5D "AppTimeTracker.csproj"): The project file containing configuration details for the build.
- [`AppTimeTracker.sln`](command:_github.copilot.openSymbolInFile?%5B%22AppTimeTracker.sln%22%2C%22AppTimeTracker.sln%22%5D "AppTimeTracker.sln"): The solution file that groups the project and its dependencies.
- [`Form1.cs`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22Form1.cs%22%5D "Form1.cs"): The main form of the application. It contains the logic for tracking application usage and displaying it in a ListView.
- [`Form1.Designer.cs`](command:_github.copilot.openSymbolInFile?%5B%22Form1.Designer.cs%22%2C%22Form1.Designer.cs%22%5D "Form1.Designer.cs"): The designer-generated code for [`Form1`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22Form1%22%5D "Form1.cs"). It contains the InitializeComponent method which sets up the form's controls.
- [`Program.cs`](command:_github.copilot.openSymbolInFile?%5B%22Program.cs%22%2C%22Program.cs%22%5D "Program.cs"): The entry point of the application. It initializes the application configuration and runs [`Form1`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22Form1%22%5D "Form1.cs").

## How It Works

The application uses a timer to periodically check the currently active window. It uses the Windows API function [`GetForegroundWindow`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22GetForegroundWindow%22%5D "Form1.cs") to get the handle of the active window, and [`GetWindowThreadProcessId`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22GetWindowThreadProcessId%22%5D "Form1.cs") to get the process ID of the window. It then uses the `Process` class to get the name of the application.

Idle time is calculated using the [`GetLastInputInfo`](command:_github.copilot.openSymbolInFile?%5B%22Form1.cs%22%2C%22GetLastInputInfo%22%5D "Form1.cs") function, which retrieves the time of the last input event.

When the application is closed, it stops the timer and logs the time spent on each application to the debug output.

## Building the Project

To build this project, you need Visual Studio with .NET 8.0 SDK installed. Open the solution file [`AppTimeTracker.sln`](command:_github.copilot.openRelativePath?%5B%22AppTimeTracker.sln%22%5D "AppTimeTracker.sln") in Visual Studio, and build the solution.

## Running the Project

After building the project, you can run the application by starting it in Visual Studio, or by running the built executable in the [`bin/Debug`](command:_github.copilot.openRelativePath?%5B%22bin%2FDebug%22%5D "bin/Debug") or `bin/Release` directory.

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

This project is licensed under the MIT License.