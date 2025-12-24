# Verse Visual Blueprint Editor - Development Guide

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Development Setup](#development-setup)
4. [Building and Running](#building-and-running)
5. [Code Structure](#code-structure)
6. [Adding New Features](#adding-new-features)
7. [Testing](#testing)
8. [Deployment](#deployment)

## Project Overview

The Verse Visual Blueprint Editor is a desktop application for creating and managing UEFN (Unreal Editor for Fortnite) Verse code visually. It provides:

- Visual blueprint node graph editor (inspired by UE5 Blueprint Editor)
- Variable and property management system
- Verse digest file parsing for class exploration
- Code generation to valid `.verse` files
- Blueprint graph persistence (save/load)

## Architecture

### Technology Stack

- **Runtime**: .NET 8.0
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Architecture**: MVVM-based (with Community Toolkit)
- **Serialization**: System.Text.Json

### Project Structure

```
VerseVisualBlueprintEditor/
├── src/
│   ├── VerseVisualBlueprintEditor.Core/          # Data models and types
│   │   ├── Models/
│   │   │   ├── BlueprintGraph.cs                 # Main graph structure
│   │   │   ├── GraphNode.cs                      # Node and pin definitions
│   │   │   ├── VerseDigest.cs                    # Verse API types
│   │   │   └── VerseVariable.cs                  # Variable definition
│   │   └── VerseVisualBlueprintEditor.Core.csproj
│   │
│   ├── VerseVisualBlueprintEditor.Services/      # Business logic
│   │   ├── BlueprintGraphService.cs              # Graph CRUD operations
│   │   ├── VerseCodeGenerator.cs                 # Code generation engine
│   │   ├── VerseDigestParser.cs                  # Digest file parsing
│   │   ├── WorkspaceService.cs                   # Workspace management
│   │   └── VerseVisualBlueprintEditor.Services.csproj
│   │
│   └── VerseVisualBlueprintEditor.UI/            # WPF Application
│       ├── App.xaml                              # Application entry point
│       ├── Windows/
│       │   ├── MainWindow.xaml                   # Main editor window
│       │   ├── MainWindow.xaml.cs                # Main window logic
│       │   ├── NewGraphWindow.xaml               # New blueprint dialog
│       │   └── NewGraphWindow.xaml.cs
│       ├── Views/                                # Custom XAML controls
│       ├── ViewModels/                           # MVVM ViewModels
│       ├── Themes/                               # UI styling
│       │   └── UnrealTheme.xaml                  # Unreal Engine dark theme
│       └── VerseVisualBlueprintEditor.UI.csproj
│
├── .github/workflows/
│   └── dotnet.yml                                # GitHub Actions CI/CD
├── .editorconfig                                 # Code style settings
├── VerseVisualBlueprintEditor.sln                # Visual Studio solution
├── README.md                                     # User documentation
├── DEVELOPMENT.md                                # This file
└── LICENSE
```

## Development Setup

### Prerequisites

- **Windows 10+ or Windows Server 2016+**
- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (Community Edition or higher) - [Download](https://visualstudio.microsoft.com/)
- **Git** - [Download](https://git-scm.com/)

### Initial Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/VerseVisualBlueprintEditor.git
cd VerseVisualBlueprintEditor

# Restore NuGet packages
dotnet restore

# Verify build
dotnet build
```

### IDE Setup (Visual Studio 2022)

1. Open `VerseVisualBlueprintEditor.sln`
2. Set `VerseVisualBlueprintEditor.UI` as startup project
3. Press `F5` to run

### IDE Setup (JetBrains Rider)

1. Open the solution file
2. Set run configuration to `VerseVisualBlueprintEditor.UI`
3. Run with `Ctrl+F5`

## Building and Running

### Debug Build

```bash
dotnet build VerseVisualBlueprintEditor.sln --configuration Debug
```

### Release Build

```bash
dotnet build VerseVisualBlueprintEditor.sln --configuration Release
```

### Running the Application

```bash
# From project directory
dotnet run --project src/VerseVisualBlueprintEditor.UI

# Or publish as standalone executable
dotnet publish src/VerseVisualBlueprintEditor.UI/VerseVisualBlueprintEditor.UI.csproj -c Release
./publish/VerseVisualBlueprintEditor.exe
```

### Running Tests

```bash
# Run all tests
dotnet test VerseVisualBlueprintEditor.sln --configuration Release --verbosity normal
```

## Code Structure

### Models (Core Layer)

All data structures inherit from base classes in the Core project:

```csharp
// BlueprintGraph.cs - Represents the entire visual blueprint
public class BlueprintGraph
{
    public Guid Id { get; set; }
    public List<GraphNode> Nodes { get; set; }
    public List<GraphConnection> Connections { get; set; }
    public List<VerseVariable> Variables { get; set; }
}

// GraphNode.cs - Individual node in the graph
public class GraphNode
{
    public Guid Id { get; set; }
    public string NodeType { get; set; } // event, function, variable
    public List<NodePin> InputPins { get; set; }
    public List<NodePin> OutputPins { get; set; }
}

// VerseVariable.cs - Blueprint variable definition
public class VerseVariable
{
    public string Name { get; set; }
    public string Type { get; set; } // string, bool, or custom class
    public string DefaultValue { get; set; }
    public bool IsEditable { get; set; }
}
```

### Services (Business Logic)

Services contain the core business logic for the application:

#### BlueprintGraphService
- CRUD operations on graphs
- Node management
- Connection handling
- Serialization/deserialization

```csharp
public class BlueprintGraphService
{
    public void SaveGraph(BlueprintGraph graph, string filePath);
    public BlueprintGraph LoadGraph(string filePath);
    public GraphNode AddNode(BlueprintGraph graph, string nodeType, ...);
    public void ConnectNodes(BlueprintGraph graph, Guid fromPinId, Guid toPinId);
}
```

#### VerseCodeGenerator
- Converts visual graphs to Verse code
- Generates proper imports and syntax
- Ensures code validation

```csharp
public class VerseCodeGenerator
{
    public string GenerateVerseCode(BlueprintGraph graph);
    public void ExportToFile(BlueprintGraph graph, string outputPath);
}
```

#### VerseDigestParser
- Parses `.digest.verse` files
- Extracts classes, methods, properties
- Provides intellisense data

```csharp
public class VerseDigestParser
{
    public VerseDiagent ParseDigestFile(string filePath);
    private List<VerseClass> ExtractClasses(string content);
    private List<VerseFunction> ExtractMethods(string content);
}
```

#### WorkspaceService
- Loads `.code-workspace` files
- Manages folder structure
- Locates digest files

```csharp
public class WorkspaceService
{
    public WorkspaceMetadata LoadWorkspace(string workspacePath);
    public List<string> FindVerseDdigestFiles(string basePath);
}
```

### UI (Presentation Layer)

WPF XAML and C# code-behind:

#### MainWindow
- Blueprint canvas for node editing
- Details panel for variables
- Menu bar for file operations

#### App.xaml
- Application-level resources
- Theme brushes and colors
- Global styles

### Themes

Unreal Engine-inspired dark theme in `UnrealTheme.xaml`:

```xaml
<!-- Color Definitions -->
<Color x:Key="DarkBackground">#1a1a1a</Color>
<Color x:Key="AccentColor">#00a0ff</Color>
<Color x:Key="TextColor">#ffffff</Color>

<!-- Control Styles -->
<Style x:Key="UnrealButton" TargetType="Button">
  <!-- Blue accent on hover -->
</Style>
```

## Adding New Features

### Adding a New Node Type

1. **Create the Model** (in Core):
```csharp
// Models/CustomNodeType.cs
public class CustomNodeType
{
    public string Property { get; set; }
}
```

2. **Update Node Creation** (in Services):
```csharp
// BlueprintGraphService.cs
case "custom":
    node.InputPins.Add(new NodePin { Name = "In", PinType = "exec" });
    node.OutputPins.Add(new NodePin { Name = "Out", PinType = "exec" });
    break;
```

3. **Add Code Generation** (in Services):
```csharp
// VerseCodeGenerator.cs
private void AddNodeCode(StringBuilder sb, GraphNode node, BlueprintGraph graph)
{
    if (node.NodeType == "custom")
    {
        // Generate custom code
    }
}
```

4. **Update UI** (in UI):
```xaml
<!-- MainWindow.xaml -->
<Button Content="Add Custom Node" Click="AddCustomNode_Click"/>
```

### Adding Variable Type Support

1. **Update VerseVariable**:
```csharp
// Supported types list
public static readonly List<string> SupportedTypes = new()
{
    "string", "bool", "int", "float",
    "?int", "?float", "agent", "vector3"
};
```

2. **Update Code Generator**:
```csharp
private void AddVariable(StringBuilder sb, VerseVariable variable)
{
    // Handle type-specific code generation
}
```

## Testing

### Unit Tests Structure

Create test projects following naming convention:
```
VerseVisualBlueprintEditor.Services.Tests/
├── VerseCodeGeneratorTests.cs
├── VerseDigestParserTests.cs
└── BlueprintGraphServiceTests.cs
```

### Example Test

```csharp
[TestClass]
public class VerseCodeGeneratorTests
{
    [TestMethod]
    public void GenerateVerseCode_WithVariables_IncludesVariableDeclarations()
    {
        // Arrange
        var graph = new BlueprintGraph { Name = "TestDevice" };
        graph.Variables.Add(new VerseVariable { Name = "Count", Type = "int" });

        var generator = new VerseCodeGenerator();

        // Act
        var code = generator.GenerateVerseCode(graph);

        // Assert
        Assert.IsTrue(code.Contains("var Count : int"));
    }
}
```

### Running Tests

```bash
dotnet test --configuration Release --verbosity detailed
```

## Deployment

### Building Release Executable

```bash
dotnet publish src/VerseVisualBlueprintEditor.UI/VerseVisualBlueprintEditor.UI.csproj \
  -c Release \
  -o ./publish \
  --self-contained false
```

### Creating GitHub Release

1. Tag the commit:
```bash
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0
```

2. GitHub Actions automatically:
   - Builds the project
   - Publishes artifacts
   - Creates GitHub Release

### Installing from Release

Users can:
1. Download the `.zip` from [Releases](../../releases)
2. Extract and run `VerseVisualBlueprintEditor.exe`

## Best Practices

### Code Style

- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use EditorConfig for consistent formatting
- Use PascalCase for public members
- Use camelCase for private fields

### Naming Conventions

```csharp
// Classes and public members
public class BlueprintGraph { }
public void SaveGraph() { }

// Private fields
private BlueprintGraphService _service;
private string _filePath;

// Constants
private const string DefaultDeviceName = "NewDevice";

// Event handlers
private void Button_Click(object sender, RoutedEventArgs e) { }
```

### Resource Management

- Always dispose of file streams
- Use `using` statements for disposable resources
- Handle exceptions gracefully

```csharp
using (var fileStream = File.OpenRead(filePath))
{
    // Use stream
}
```

### XAML Best Practices

- Use named styles from theme
- Avoid inline styles in controls
- Keep code-behind minimal
- Use data binding instead of setting properties

```xaml
<!-- Good -->
<Button Style="{StaticResource UnrealButton}" Content="Click Me"/>

<!-- Bad -->
<Button Background="#242424" Foreground="#ffffff" Content="Click Me"/>
```

## Troubleshooting

### Build Issues

```bash
# Clean and rebuild
dotnet clean
dotnet build --configuration Release

# Restore packages
dotnet restore --force
```

### WPF Rendering Issues

- Ensure Windows 10+ with latest graphics drivers
- Run as Administrator if permission issues occur

### Digest Parsing Issues

- Verify digest files follow Verse syntax
- Check file encoding (UTF-8)
- Inspect regex patterns in `VerseDigestParser.cs`

## Resources

- [.NET 8 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [MVVM Community Toolkit](https://github.com/CommunityToolkit/dotnet)
- [UEFN Documentation](https://dev.epicgames.com/documentation/en-US/uefn)
- [Verse Language Reference](https://dev.epicgames.com/documentation/en-US/uefn/verse-language-reference)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution guidelines.

## Questions?

Open an [Issue](../../issues) or [Discussion](../../discussions) with your questions!
