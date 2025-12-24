using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerseVisualBlueprintEditor.Core.Models;
using VerseVisualBlueprintEditor.Services;

namespace VerseVisualBlueprintEditor.Tests
{
    [TestClass]
    public class VerseCodeGeneratorTests
    {
        private VerseCodeGenerator _generator = new();

        [TestMethod]
        [TestCategory("CodeGeneration")]
        public void GenerateVerseCode_WithVariables_IncludesVariableDeclarations()
        {
            // Arrange
            var graph = new BlueprintGraph
            {
                Name = "TestDevice",
                DeviceClassName = "test_device",
                Imports = new List<string>
                {
                    "using { /Verse.org/Simulation }",
                    "using { /Fortnite.com/Devices }"
                }
            };

            graph.Variables.Add(new VerseVariable
            {
                Name = "Count",
                Type = "int",
                DefaultValue = "0",
                IsPublic = true,
                IsEditable = true
            });

            // Act
            var code = _generator.GenerateVerseCode(graph);

            // Assert
            Assert.IsTrue(code.Contains("using { /Verse.org/Simulation }"));
            Assert.IsTrue(code.Contains("test_device := class(creative_device):"));
            Assert.IsTrue(code.Contains("var Public_Count : int = 0"));
            Assert.IsTrue(code.Contains("OnBegin<override>()<suspends>:void ="));
        }

        [TestMethod]
        [TestCategory("CodeGeneration")]
        public void GenerateVerseCode_WithEditableVariable_IncludesEditableAttribute()
        {
            // Arrange
            var graph = new BlueprintGraph
            {
                Name = "TestDevice",
                DeviceClassName = "test_device"
            };

            var editableVar = new VerseVariable
            {
                Name = "Button",
                Type = "button_device",
                IsEditable = true,
                IsPublic = true
            };

            graph.Variables.Add(editableVar);

            // Act
            var code = _generator.GenerateVerseCode(graph);

            // Assert
            Assert.IsTrue(code.Contains("@editable"));
        }

        [TestMethod]
        [TestCategory("CodeGeneration")]
        public void ExportToFile_CreatesValidFile()
        {
            // Arrange
            var graph = new BlueprintGraph
            {
                Name = "ExportTest",
                DeviceClassName = "export_test_device"
            };

            string tempPath = Path.Combine(Path.GetTempPath(), "test_export.verse");

            try
            {
                // Act
                _generator.ExportToFile(graph, tempPath);

                // Assert
                Assert.IsTrue(File.Exists(tempPath));
                var content = File.ReadAllText(tempPath);
                Assert.IsTrue(content.Contains("export_test_device"));
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }

    [TestClass]
    public class BlueprintGraphServiceTests
    {
        private BlueprintGraphService _service = new();

        [TestMethod]
        [TestCategory("GraphManagement")]
        public void CreateNewGraph_ReturnsValidGraph()
        {
            // Act
            var graph = _service.CreateNewGraph("MyBlueprint");

            // Assert
            Assert.IsNotNull(graph);
            Assert.AreEqual("MyBlueprint", graph.Name);
            Assert.IsTrue(graph.Imports.Count > 0);
            Assert.IsNotNull(graph.Nodes);
            Assert.IsNotNull(graph.Variables);
        }

        [TestMethod]
        [TestCategory("GraphManagement")]
        public void AddNode_AddsNodeToGraph()
        {
            // Arrange
            var graph = _service.CreateNewGraph("TestGraph");

            // Act
            var node = _service.AddNode(graph, "event", "OnBegin", 100, 100);

            // Assert
            Assert.AreEqual(1, graph.Nodes.Count);
            Assert.AreEqual("event", node.NodeType);
            Assert.AreEqual("OnBegin", node.Name);
        }

        [TestMethod]
        [TestCategory("GraphManagement")]
        public void RemoveNode_RemovesNodeFromGraph()
        {
            // Arrange
            var graph = _service.CreateNewGraph("TestGraph");
            var node = _service.AddNode(graph, "event", "OnBegin", 100, 100);

            // Act
            _service.RemoveNode(graph, node.Id);

            // Assert
            Assert.AreEqual(0, graph.Nodes.Count);
        }

        [TestMethod]
        [TestCategory("GraphManagement")]
        public void SaveAndLoadGraph_PreservesData()
        {
            // Arrange
            var originalGraph = _service.CreateNewGraph("SaveTest");
            originalGraph.Variables.Add(new VerseVariable { Name = "TestVar", Type = "int" });

            string tempPath = Path.Combine(Path.GetTempPath(), "test_blueprint.blueprint");

            try
            {
                // Act
                _service.SaveGraph(originalGraph, tempPath);
                var loadedGraph = _service.LoadGraph(tempPath);

                // Assert
                Assert.AreEqual(originalGraph.Name, loadedGraph.Name);
                Assert.AreEqual(1, loadedGraph.Variables.Count);
                Assert.AreEqual("TestVar", loadedGraph.Variables[0].Name);
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }

    [TestClass]
    public class VerseVariableTests
    {
        [TestMethod]
        [TestCategory("Variables")]
        public void VerseVariable_DefaultValues_AreSet()
        {
            // Act
            var variable = new VerseVariable
            {
                Name = "TestVariable",
                Type = "string"
            };

            // Assert
            Assert.AreEqual("TestVariable", variable.Name);
            Assert.AreEqual("string", variable.Type);
            Assert.IsTrue(variable.IsEditable);
            Assert.IsTrue(variable.IsPublic);
            Assert.IsNotNull(variable.Id);
        }

        [TestMethod]
        [TestCategory("Variables")]
        public void VerseVariable_WithDefaultValue_StoresValue()
        {
            // Act
            var variable = new VerseVariable
            {
                Name = "Counter",
                Type = "int",
                DefaultValue = "42"
            };

            // Assert
            Assert.AreEqual("42", variable.DefaultValue);
        }
    }
}
