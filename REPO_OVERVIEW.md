# Verse Visual Blueprint Editor - Repository

This is the official repository for the Verse Visual Blueprint Editor, a professional visual blueprint editor for UEFN Verse code.

## Quick Links

- 📚 [Documentation](README.md)
- 🚀 [Quick Start Guide](QUICKSTART.md)
- 👨‍💻 [Developer Guide](DEVELOPMENT.md)
- 🤝 [Contributing](CONTRIBUTING.md)
- 🔒 [Security Policy](SECURITY.md)
- 📋 [Changelog](CHANGELOG.md)
- 🗺️ [Roadmap](TODO.md)

## Features at a Glance

✨ **Visual Blueprint Editor** - Create Verse devices with a node-based graph  
📁 **Workspace Support** - Load and manage UEFN projects  
💾 **Code Generation** - Export blueprints to valid `.verse` files  
🎨 **Modern UI** - Unreal Engine-inspired dark theme  
⚡ **Fast & Reliable** - Built on .NET 8.0  

## System Requirements

- **OS**: Windows 10 or later
- **Runtime**: .NET 8.0 or higher
- **Memory**: 512 MB RAM minimum
- **Disk**: 100 MB free space

## Installation

[Download the latest release](../../releases) and run the executable.

Or build from source:
```bash
git clone https://github.com/yourusername/VerseVisualBlueprintEditor.git
cd VerseVisualBlueprintEditor
dotnet build --configuration Release
dotnet run --project src/VerseVisualBlueprintEditor.UI
```

## Example Usage

Create a button click handler:

```verse
using { /Verse.org/Simulation }
using { /Fortnite.com/Devices }

click_counter_device := class(creative_device):
    @editable
    var Public_Button : button_device = button_device{}
    
    var ClickCount : int = 0

    OnBegin<override>()<suspends>:void =
        Button.InteractedWithEvent.Subscribe(OnButtonPressed)

    OnButtonPressed(Agent : agent) : void =
        ClickCount += 1
        Print("Clicked {ClickCount} times!")
```

**Create this in seconds with the Visual Blueprint Editor!**

## Project Structure

```
VerseVisualBlueprintEditor/
├── src/
│   ├── VerseVisualBlueprintEditor.Core/      # Data models
│   ├── VerseVisualBlueprintEditor.Services/  # Business logic
│   └── VerseVisualBlueprintEditor.UI/        # WPF application
├── tests/
│   └── VerseVisualBlueprintEditor.Tests/     # Unit tests
├── .github/workflows/                         # CI/CD pipeline
├── README.md                                  # Full documentation
├── DEVELOPMENT.md                             # Developer guide
└── QUICKSTART.md                              # Getting started
```

## Building from Source

### Prerequisites
- Visual Studio 2022 or VS Code
- .NET 8.0 SDK

### Build Steps
```bash
# Clone and navigate
git clone https://github.com/yourusername/VerseVisualBlueprintEditor.git
cd VerseVisualBlueprintEditor

# Restore and build
dotnet restore
dotnet build --configuration Release

# Run tests
dotnet test

# Publish
dotnet publish src/VerseVisualBlueprintEditor.UI -c Release
```

## Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

**Areas where we need help:**
- Visual node graph UI implementation
- Blueprint templates
- Documentation improvements
- Bug reports and testing

## CI/CD Pipeline

Every push to `main` automatically:
✅ Builds the project  
✅ Runs unit tests  
✅ Creates release artifacts  
📦 Publishes to GitHub Releases (on tag)

## License

MIT License - See [LICENSE](LICENSE) file for details.

## Community

- 💬 [Discussions](../../discussions) - Ask questions
- 🐛 [Issues](../../issues) - Report bugs
- 🌟 Star this repo if you find it helpful!

## Support

For help:
1. Check [QUICKSTART.md](QUICKSTART.md)
2. Read [README.md](README.md)
3. Review [DEVELOPMENT.md](DEVELOPMENT.md)
4. Open a [Discussion](../../discussions)

## Roadmap

**v1.0** (Current)
- Core blueprint editor
- Variable management
- Code generation

**v1.1** (Planned)
- Visual node graph canvas
- Advanced node types
- Blueprint templates

**v2.0** (Future)
- Direct UEFN integration
- Blueprint collaboration
- Community marketplace

See [TODO.md](TODO.md) for detailed roadmap.

## Credits

Created with ❤️ for the UEFN community.

Inspired by:
- Unreal Engine Blueprint Editor
- Epic Games UEFN team
- Verse community

---

**Ready to create amazing UEFN experiences?** Get started now! 🚀

[Download](../../releases) | [Docs](README.md) | [Guide](QUICKSTART.md) | [Contribute](CONTRIBUTING.md)
