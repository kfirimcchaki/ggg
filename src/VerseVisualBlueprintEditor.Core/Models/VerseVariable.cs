namespace VerseVisualBlueprintEditor.Core.Models
{
    /// <summary>
    /// Represents a variable in the Verse blueprint
    /// </summary>
    public class VerseVariable
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "string";
        public string DefaultValue { get; set; } = string.Empty;
        public bool IsEditable { get; set; } = true;
        public bool IsPublic { get; set; } = true;
        public string Description { get; set; } = string.Empty;
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
