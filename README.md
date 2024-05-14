# Freestyle Slalom Competition Manager

The Freestyle Slalom Competition Manager is a command-line application designed to help manage freestyle slalom competitions. It allows users to create competitions, add new skaters, and manage existing skaters within the competition.

## Features

- **Create Competition**: Create a new competition with details such as name, dates, description, address, and organizer.
- **Add New Skater**: Add a new skater to the system with details like WSID, name, and country.
- **Add Existing Skater**: Add an existing skater to the current competition using their WSID.
- **Import Data**: Import skater or competition data from files or folders.
- **Help**: Display a list of available commands.

## Getting Started

### Prerequisites

- .NET Core SDK 3.1 or later

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/FreestyleSlalomCompetitionManager.git
   cd FreestyleSlalomCompetitionManager
   ```

2. Restore the dependencies:

   ```bash
   dotnet restore
   ```

### Building the Project

To build the project, run:

```bash
dotnet build
```

### Running the Application

To run the application, use:

```bash
dotnet run --project FreestyleSlalomCompetitionManager
```

### Running Tests

To run the tests, use:

```bash
dotnet test
```

## Usage

The application is command-line based and supports the following commands:

### Help

Displays a list of available commands.

```bash
help
```

### Create Competition

Creates a new competition.

```bash
createcompetition <Name> <StartDate> <EndDate> <Description> <Address> <OrganizerName> <OrganizerWSID>
```

### Add New Skater

Adds a new skater.

```bash
newskater <WSID> <Name> <Country>
```

### Add Existing Skater

Adds an existing skater to the current competition.

```bash
addexistingskater <WSID>
```

### Import Folder

Imports data from a folder.

```bash
importfolder <FolderPath>
```

### Import File

Imports data from a file.

```bash
importfile <FilePath>
```

### Exit

Exits the application.

```bash
exit
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any questions or inquiries, please contact [yourname@example.com](mailto:yourname@example.com).
```

This `README.md` provides an overview of the project, how to get started, usage instructions, and information on contributing and licensing. Be sure to replace placeholders like `yourusername` and `yourname@example.com` with your actual GitHub username and email address.
