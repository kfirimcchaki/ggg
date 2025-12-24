# Verse Visual Blueprint Editor

A professional visual blueprint editor for UEFN (Unreal Editor for Fortnite) Verse code, inspired by Unreal Engine's Blueprint Editor. Create, visualize, and export Verse code with a node-based graph interface.

## Features

- **Visual Blueprint Graph Editor** - Drag-and-drop node graph similar to UE5 Blueprint Editor
- **Variable Management** - Create and configure Verse variables with full property editing
- **Verse Digest Parsing** - Load and browse Verse classes from digest files
- **Workspace Integration** - Load `.code-workspace` files to access project structure
- **Code Generation** - Export visual blueprints directly to valid `.verse` files
- **Blueprint Export/Import** - Save and load blueprint graphs as `.blueprint` files
- **Modern Dark Theme** - Unreal Engine-inspired dark UI with intuitive layout

## Architecture

The project is organized into three main components:

### Core (`VerseVisualBlueprintEditor.Core`)
- Data models for blueprints, nodes, variables, and Verse digest structures
- Type definitions and object graphs

### Services (`VerseVisualBlueprintEditor.Services`)
- `BlueprintGraphService` - Graph manipulation and persistence
- `VerseCodeGenerator` - Converts visual graphs to valid Verse code
- `VerseDigestParser` - Parses Verse digest files to extract classes and metadata
- `WorkspaceService` - Loads and manages workspace configurations

### UI (`VerseVisualBlueprintEditor.UI`)
- WPF-based Windows desktop application
- Unreal Engine-themed dark interface
- Blueprint graph canvas with node editing
- Details panel for variable configuration
- Verse class browser

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Windows 10+ (WPF application)
- Visual Studio 2022+ (recommended) or any .NET-compatible IDE

### Building from Source

```bash
git clone https://github.com/yourusername/VerseVisualBlueprintEditor.git
cd VerseVisualBlueprintEditor

# Restore dependencies
dotnet restore

# Build
dotnet build --configuration Release

# Run
dotnet run --project src/VerseVisualBlueprintEditor.UI/VerseVisualBlueprintEditor.UI.csproj
```

### Installation

Download the latest release executable from the [Releases](../../releases) page and run it directly.

## Usage

### Creating a New Blueprint

1. Click **New** in the menu bar
2. Enter a blueprint name (e.g., "Button_Handler")
3. Configure the class name
4. Click **Create**

### Loading a Workspace

1. Click **Load Workspace**
2. Select your `.code-workspace` file
3. The editor will automatically load all Verse digest files
4. Browse loaded classes in the **Verse Classes** tab

### Adding Variables

1. Go to the **Variables** tab in the Details panel
2. Click **+ Add Variable**
3. Select the variable and configure it in the **Properties** tab:
   - **Name** - Variable identifier
   - **Type** - Variable type (string, bool, int, float, or custom Verse class)
   - **Default Value** - Initial value (optional)
   - **Is Editable** - Mark as @editable in UEFN
   - **Is Public** - Mark as public variable

### Exporting to Verse

1. Configure your blueprint with variables
2. Click **Export to .verse**
3. Choose the output location
4. The `.verse` file is ready for use in UEFN

## Project Structure

```
VerseVisualBlueprintEditor/
├── src/
│   ├── VerseVisualBlueprintEditor.Core/
│   │   └── Models/
│   ├── VerseVisualBlueprintEditor.Services/
│   │   ├── BlueprintGraphService.cs
│   │   ├── VerseCodeGenerator.cs
│   │   ├── VerseDigestParser.cs
│   │   └── WorkspaceService.cs
│   └── VerseVisualBlueprintEditor.UI/
│       ├── Windows/
│       │   ├── MainWindow.xaml
│       │   └── NewGraphWindow.xaml
│       ├── Views/
│       ├── ViewModels/
│       ├── Themes/
│       └── App.xaml
├── .github/workflows/
│   └── dotnet.yml (CI/CD Pipeline)
├── VerseVisualBlueprintEditor.sln
├── README.md
└── LICENSE
```

## Verse Code Generation

The editor generates production-ready Verse code following UEFN best practices:

```verse
using { /Verse.org/Simulation }
using { /Fortnite.com/Devices }
using { /UnrealEngine.com/Temporary/Diagnostics }

my_device := class(creative_device):
    @editable
    var Public_ButtonDevice : button_device = button_device{}
    
    var ClickCount : int = 0

    OnBegin<override>()<suspends>:void =
        if (Device := Get_button_device()):
            Device.InteractedWithEvent.Subscribe(OnButtonPressed)

    OnButtonPressed(Agent : agent) : void =
        ClickCount += 1
        Print("Button pressed {ClickCount} times")
```

## Supported Variable Types

### Primitive Types
- `string`
- `bool`
- `int`
- `float`
- `?int` (optional int)
- `?float` (optional float)

### Custom Types
- Any Verse class loaded from digest files
- Device types (button_device, volume_device, etc.)
- Custom classes from your project

## CI/CD Pipeline

The repository includes GitHub Actions workflow for automated builds:

- **Build** - Compiles the project on every push
- **Test** - Runs unit tests (when added)
- **Publish** - Creates release artifacts
- **Release** - Publishes tagged releases automatically

Artifacts are available in the [Releases](../../releases) section.

## Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Roadmap

- [ ] Advanced node graph visual editor with canvas pan/zoom
- [ ] Event node system for connecting multiple devices
- [ ] Function call nodes with parameter binding
- [ ] Conditional node system (if/else logic)
- [ ] Array and collection support
- [ ] Blueprint copy/paste and templates
- [ ] Verse code syntax highlighting in export preview
- [ ] Direct UEFN integration
- [ ] Blueprint validation and error checking

## Known Limitations

- Blueprint graph visualization is currently text-based; full node graph UI coming soon
- Limited node types in current version
- Verse digest parsing covers core classes only

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## References

- [UEFN Documentation](https://dev.epicgames.com/documentation/en-US/uefn)
- [Verse Language Guide](https://dev.epicgames.com/documentation/en-US/uefn/verse-language-guide)
- [Fortnite Creative Devices](https://dev.epicgames.com/documentation/en-US/uefn/devices-in-fortnite-creative)
- [Unreal Engine Blueprint Editor](https://docs.unrealengine.com/en-US/Engine/Blueprints/)

## Support

For issues, questions, or suggestions:
- Open an [Issue](../../issues)
- Check [Discussions](../../discussions) for Q&A

## Credits

Inspired by the Unreal Engine Blueprint Editor and built for the UEFN community.

---

**Made with ❤️ for Fortnite Creative Developers**
