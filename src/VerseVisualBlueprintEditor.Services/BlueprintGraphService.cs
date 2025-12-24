using VerseVisualBlueprintEditor.Core.Models;
using System.Text.Json;

namespace VerseVisualBlueprintEditor.Services
{
    /// <summary>
    /// Service for persisting and loading blueprint graphs
    /// </summary>
    public class BlueprintGraphService
    {
        public void SaveGraph(BlueprintGraph graph, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(graph, options);
            File.WriteAllText(filePath, json);
        }

        public BlueprintGraph LoadGraph(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Blueprint file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            var graph = JsonSerializer.Deserialize<BlueprintGraph>(json);

            return graph ?? new BlueprintGraph();
        }

        public BlueprintGraph CreateNewGraph(string name)
        {
            return new BlueprintGraph
            {
                Name = name,
                DeviceClassName = name.Replace(" ", "_") + "_device",
                Imports = new List<string>
                {
                    "using { /Verse.org/Simulation }",
                    "using { /Fortnite.com/Devices }",
                    "using { /UnrealEngine.com/Temporary/Diagnostics }"
                }
            };
        }

        public GraphNode AddNode(BlueprintGraph graph, string nodeType, string name, double x, double y)
        {
            var node = new GraphNode
            {
                NodeType = nodeType,
                Name = name,
                X = x,
                Y = y
            };

            // Add default pins based on node type
            switch (nodeType)
            {
                case "event":
                    node.OutputPins.Add(new NodePin { Name = "Exec", PinType = "exec", IsInput = false });
                    break;

                case "function":
                    node.InputPins.Add(new NodePin { Name = "Exec", PinType = "exec", IsInput = true });
                    node.OutputPins.Add(new NodePin { Name = "Exec", PinType = "exec", IsInput = false });
                    break;

                case "variable":
                    node.OutputPins.Add(new NodePin { Name = "Get", PinType = "object", IsInput = false });
                    break;
            }

            graph.Nodes.Add(node);
            return node;
        }

        public void ConnectNodes(BlueprintGraph graph, Guid fromPinId, Guid toPinId)
        {
            var connection = new GraphConnection
            {
                FromPinId = fromPinId,
                ToPinId = toPinId
            };

            graph.Connections.Add(connection);
        }

        public void RemoveNode(BlueprintGraph graph, Guid nodeId)
        {
            var node = graph.Nodes.FirstOrDefault(n => n.Id == nodeId);
            if (node != null)
            {
                graph.Nodes.Remove(node);

                // Remove all connections involving this node
                var pinsToRemove = node.InputPins.Concat(node.OutputPins).Select(p => p.Id).ToList();
                graph.Connections.RemoveAll(c => pinsToRemove.Contains(c.FromPinId) || pinsToRemove.Contains(c.ToPinId));
            }
        }

        public void RemoveConnection(BlueprintGraph graph, Guid fromPinId, Guid toPinId)
        {
            graph.Connections.RemoveAll(c => c.FromPinId == fromPinId && c.ToPinId == toPinId);
        }
    }
}
