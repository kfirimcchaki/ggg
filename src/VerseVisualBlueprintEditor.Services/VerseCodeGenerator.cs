using VerseVisualBlueprintEditor.Core.Models;
using System.Text;

namespace VerseVisualBlueprintEditor.Services
{
    /// <summary>
    /// Service for generating Verse code from visual blueprint graphs
    /// </summary>
    public class VerseCodeGenerator
    {
        public string GenerateVerseCode(BlueprintGraph graph)
        {
            var sb = new StringBuilder();

            // Add imports
            AddImports(sb, graph);

            // Add device class declaration
            AddClassDeclaration(sb, graph);

            // Add variables
            AddVariables(sb, graph);

            // Add OnBegin method
            AddOnBeginMethod(sb, graph);

            // Close class
            sb.AppendLine();

            return sb.ToString();
        }

        private void AddImports(StringBuilder sb, BlueprintGraph graph)
        {
            sb.AppendLine("# Auto-generated Verse Blueprint - Do not modify manually unless necessary");
            sb.AppendLine();

            var imports = new HashSet<string>(graph.Imports)
            {
                "using { /Verse.org/Simulation }",
                "using { /Fortnite.com/Devices }",
                "using { /UnrealEngine.com/Temporary/Diagnostics }"
            };

            foreach (var import in imports.OrderBy(x => x))
            {
                sb.AppendLine(import);
            }

            sb.AppendLine();
        }

        private void AddClassDeclaration(StringBuilder sb, BlueprintGraph graph)
        {
            var className = graph.DeviceClassName;
            if (string.IsNullOrEmpty(className))
                className = graph.Name.Replace(" ", "_") + "_device";

            sb.AppendLine($"{className} := class(creative_device):");
        }

        private void AddVariables(StringBuilder sb, BlueprintGraph graph)
        {
            if (graph.Variables.Count == 0)
            {
                sb.AppendLine("    # Variables");
                return;
            }

            sb.AppendLine();
            foreach (var variable in graph.Variables)
            {
                AddVariable(sb, variable);
            }

            sb.AppendLine();
        }

        private void AddVariable(StringBuilder sb, VerseVariable variable)
        {
            var sb_var = new StringBuilder("    ");

            if (variable.IsEditable)
                sb_var.Append("@editable\n    ");

            sb_var.Append("var ");

            if (variable.IsPublic)
                sb_var.Append("Public_");

            sb_var.Append($"{variable.Name} : {variable.Type}");

            if (!string.IsNullOrEmpty(variable.DefaultValue))
            {
                sb_var.Append($" = {variable.DefaultValue}");
            }

            sb.AppendLine(sb_var.ToString());
        }

        private void AddOnBeginMethod(StringBuilder sb, BlueprintGraph graph)
        {
            sb.AppendLine();
            sb.AppendLine("    OnBegin<override>()<suspends>:void =");

            if (graph.Nodes.Count == 0)
            {
                sb.AppendLine("        # Add nodes to the graph to generate code");
            }
            else
            {
                var eventNodes = graph.Nodes.Where(n => n.NodeType == "event").ToList();

                foreach (var node in eventNodes)
                {
                    AddNodeCode(sb, node, graph);
                }

                if (eventNodes.Count == 0)
                {
                    sb.AppendLine("        # No event nodes found");
                }
            }

            sb.AppendLine();
        }

        private void AddNodeCode(StringBuilder sb, GraphNode node, BlueprintGraph graph)
        {
            if (node.Properties.TryGetValue("ClassName", out var className) &&
                node.Properties.TryGetValue("EventName", out var eventName))
            {
                var varName = node.Properties.ContainsKey("VariableName") ? 
                    node.Properties["VariableName"] : className.ToLower();

                sb.AppendLine($"        if (Device := Get_{className}()):");
                sb.AppendLine($"            Device.{eventName}.Subscribe(OnEvent_{eventName})");
            }
        }

        public void ExportToFile(BlueprintGraph graph, string outputPath)
        {
            var code = GenerateVerseCode(graph);
            File.WriteAllText(outputPath, code, Encoding.UTF8);
        }

        public string GetFileExtension() => ".verse";
    }
}
