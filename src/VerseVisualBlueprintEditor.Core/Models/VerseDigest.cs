namespace VerseVisualBlueprintEditor.Core.Models
{
    /// <summary>
    /// Represents a loaded Verse digest file
    /// </summary>
    public class VerseDiagent
    {
        public string ModulePath { get; set; } = string.Empty;
        public List<VerseClass> Classes { get; set; } = new();
        public List<VerseFunction> Functions { get; set; } = new();
        public List<VerseEvent> Events { get; set; } = new();
    }

    /// <summary>
    /// Represents a Verse class from the digest
    /// </summary>
    public class VerseClass
    {
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public List<VerseProperty> Properties { get; set; } = new();
        public List<VerseFunction> Methods { get; set; } = new();
        public bool IsNative { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a property on a Verse class
    /// </summary>
    public class VerseProperty
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public bool IsEditable { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a Verse function or method
    /// </summary>
    public class VerseFunction
    {
        public string Name { get; set; } = string.Empty;
        public string ReturnType { get; set; } = "void";
        public List<VerseParameter> Parameters { get; set; } = new();
        public bool IsNative { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Modifiers { get; set; } = new(); // override, async, etc.
    }

    /// <summary>
    /// Represents a parameter for a Verse function
    /// </summary>
    public class VerseParameter
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a Verse event
    /// </summary>
    public class VerseEvent
    {
        public string Name { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
