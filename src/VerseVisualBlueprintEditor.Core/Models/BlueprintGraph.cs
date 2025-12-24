namespace VerseVisualBlueprintEditor.Core.Models
{
    /// <summary>
    /// Represents a complete Verse blueprint graph
    /// </summary>
    public class BlueprintGraph
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "NewDevice";
        public string FilePath { get; set; } = string.Empty;
        public List<GraphNode> Nodes { get; set; } = new();
        public List<GraphConnection> Connections { get; set; } = new();
        public List<VerseVariable> Variables { get; set; } = new();
        public string DeviceClassName { get; set; } = string.Empty;
        public List<string> Imports { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
