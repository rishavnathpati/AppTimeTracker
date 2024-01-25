# AppTimeTracker

AppTimeTracker is a Windows Forms application that tracks the time spent on different applications on your computer. It uses P/Invoke to call native Windows APIs to get the currently active window and calculate idle time.

## Project Structure

Here's a brief overview of the key files in this project:

- [`AppTimeTracker.csproj`](AppTimeTracker.csproj): The project file containing configuration details for the build.
- [`AppTimeTracker.sln`](AppTimeTracker.sln): The solution file that groups the project and its dependencies.
- [`MainForm.cs`](MainForm.cs): The main form of the application. It contains the logic for tracking application usage and displaying it in a ListView.
- [`MainForm.Designer.cs`](MainForm.Designer.cs): The designer-generated code for `MainForm`. It contains the InitializeComponent method which sets up the form's controls.
- [`Program.cs`](Program.cs): The entry point of the application. It initializes the application configuration and runs `MainForm`.
- [`ApplicationData.cs`](ApplicationData.cs): Represents the data for a single application, such as the name of the application and the total time spent on it.
- [`ApplicationListView.cs`](ApplicationListView.cs): Responsible for displaying the application data in a ListView.
- [`ApplicationTracker.cs`](ApplicationTracker.cs): Tracks the time spent on different applications.

## How It Works

The application uses a timer to periodically check the currently active window. It uses the Windows API function `GetForegroundWindow` to get the handle of the active window, and `GetWindowThreadProcessId` to get the process ID of the window. It then uses the `Process` class to get the name of the application.

Idle time is calculated using the `GetLastInputInfo` function, which retrieves the time of the last input event.

When the application is closed, it stops the timer and logs the time spent on each application to the debug output.

## Building the Project

To build this project, you need Visual Studio with .NET 8.0 SDK installed. Open the solution file `AppTimeTracker.sln` in Visual Studio, and build the solution.

## Running the Project

After building the project, you can run the application by starting it in Visual Studio, or by running the built executable in the `bin/Debug` or `bin/Release` directory.

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

This project is licensed under the MIT License.