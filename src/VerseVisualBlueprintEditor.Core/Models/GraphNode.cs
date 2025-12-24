namespace VerseVisualBlueprintEditor.Core.Models
{
    /// <summary>
    /// Represents a node in the visual graph
    /// </summary>
    public class GraphNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string NodeType { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public List<NodePin> InputPins { get; set; } = new();
        public List<NodePin> OutputPins { get; set; } = new();
        public Dictionary<string, string> Properties { get; set; } = new();
    }

    /// <summary>
    /// Represents a pin/connection point on a node
    /// </summary>
    public class NodePin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string PinType { get; set; } = "exec"; // exec, object, bool, int, float, string
        public bool IsInput { get; set; }
        public Guid? ConnectedPinId { get; set; }
    }

    /// <summary>
    /// Represents a connection between two pins
    /// </summary>
    public class GraphConnection
    {
        public Guid FromPinId { get; set; }
        public Guid ToPinId { get; set; }
    }
}
