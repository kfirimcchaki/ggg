# Quick Start Guide

## Installation

1. **Download** the latest release from [GitHub Releases](../../releases)
2. **Extract** the ZIP file to your desired location
3. **Run** `VerseVisualBlueprintEditor.exe`

## First Steps

### Step 1: Create a New Blueprint

1. Click **New** button in the toolbar
2. Enter a name for your blueprint (e.g., "ButtonHandler")
3. Configure the class name (default: `buttonthandler_device`)
4. Click **Create**

### Step 2: Add Variables

1. Go to the **Variables** tab on the right panel
2. Click **+ Add Variable**
3. Select the new variable and configure in **Properties**:
   - Set **Name** to something descriptive (e.g., "MyButton")
   - Set **Type** to the device/type (e.g., "button_device")
   - Enable **Is Editable** to make it configurable in UEFN
   - Enable **Is Public** to expose it

### Step 3: Load Verse Digests

1. Click **Load Workspace**
2. Select your `.code-workspace` file (from your UEFN project)
3. All Verse classes are now available in the **Verse Classes** tab
4. Search for specific classes using the search box

### Step 4: Export to Verse

1. Configure your blueprint variables
2. Click **Export to .verse**
3. Choose where to save your `.verse` file
4. The file is ready to use in UEFN!

## Example: Creating a Button Click Handler

### Setup

1. **New Blueprint** → Name: "ClickCounter"
2. **Add Variables**:
   - Name: `TargetButton`, Type: `button_device`, Editable: ✓
   - Name: `ClickCount`, Type: `int`, Default: `0`

### Export

Click **Export to .verse** → Save as `click_counter.verse`

### Result

```verse
using { /Verse.org/Simulation }
using { /Fortnite.com/Devices }
using { /UnrealEngine.com/Temporary/Diagnostics }

click_counter := class(creative_device):
    @editable
    var Public_TargetButton : button_device = button_device{}
    
    var ClickCount : int = 0

    OnBegin<override>()<suspends>:void =
        # Add nodes to the graph to generate code
```

## Tips & Tricks

### Workspace Files

- **Location**: Your UEFN project folder
- **Name**: Usually `YourProjectName.code-workspace`
- **Contains**: Paths to Verse digest files with API definitions

### Variable Types

- **Primitive**: `string`, `bool`, `int`, `float`
- **Optional**: `?int`, `?float`, `?string`
- **Devices**: `button_device`, `volume_device`, etc.
- **Custom**: Any class from loaded Verse digests

### Best Practices

✓ Use descriptive variable names  
✓ Always mark external device references as `@editable`  
✓ Load workspace files to access full API  
✓ Save your blueprints frequently  
✓ Test generated code in UEFN before deployment

### Keyboard Shortcuts (Coming Soon)

- `Ctrl+N` - New blueprint
- `Ctrl+O` - Open blueprint
- `Ctrl+S` - Save blueprint
- `Ctrl+E` - Export to .verse
- `Ctrl+L` - Load workspace

## File Locations

| File Type | Extension | Use |
|-----------|-----------|-----|
| Blueprint | `.blueprint` | Save/load visual graphs |
| Verse Code | `.verse` | Use in UEFN projects |
| Workspace | `.code-workspace` | Load UEFN project structure |

## Troubleshooting

### "File not found" when loading workspace

- Ensure the `.code-workspace` file exists
- Check the file path is correct
- Verify Verse digest files are in the workspace folder

### No classes appear in Verse Classes tab

- Click **Load Workspace** first
- Verify workspace file contains Verse paths
- Check if digest files exist in the workspace

### Export produces empty .verse file

- Add at least one variable to your blueprint
- Ensure the blueprint name is valid
- Check the export path is writable

### Application won't start

- Install .NET 8.0 Runtime or higher
- Run as Administrator if permission issues occur
- Check Windows 10+ is installed

## Getting Help

- Check [README.md](../../README.md) for detailed documentation
- Open an [Issue](../../issues) for bugs
- Use [Discussions](../../discussions) for questions

## Next Steps

1. ✅ Create your first blueprint
2. ✅ Export a `.verse` file
3. ✅ Import into your UEFN project
4. ✅ Playtest your device in-game
5. 🚀 Share your creations with the community!

---

**Happy Verse coding! 🎮**
