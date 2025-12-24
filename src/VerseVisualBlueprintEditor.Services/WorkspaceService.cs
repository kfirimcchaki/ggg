using System.Text.Json;
using VerseVisualBlueprintEditor.Core.Models;

namespace VerseVisualBlueprintEditor.Services
{
    /// <summary>
    /// Service for managing code workspace files
    /// </summary>
    public class WorkspaceService
    {
        public WorkspaceMetadata LoadWorkspace(string workspacePath)
        {
            if (!File.Exists(workspacePath))
                throw new FileNotFoundException($"Workspace file not found: {workspacePath}");

            var content = File.ReadAllText(workspacePath);
            var metadata = new WorkspaceMetadata();

            try
            {
                var jsonDoc = JsonDocument.Parse(content);
                var root = jsonDoc.RootElement;

                if (root.TryGetProperty("folders", out var folders))
                {
                    foreach (var folder in folders.EnumerateArray())
                    {
                        if (folder.TryGetProperty("path", out var pathProp))
                        {
                            metadata.FolderPaths.Add(pathProp.GetString() ?? string.Empty);
                        }

                        if (folder.TryGetProperty("name", out var nameProp))
                        {
                            var folderName = nameProp.GetString() ?? string.Empty;
                            if (folderName.Contains("Verse"))
                            {
                                metadata.VerseFolderPath = pathProp.GetString() ?? string.Empty;
                            }
                        }
                    }
                }

                if (root.TryGetProperty("settings", out var settings))
                {
                    metadata.Settings = ExtractSettings(settings);
                }
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse workspace JSON", ex);
            }

            return metadata;
        }

        public List<string> FindVerseDdigestFiles(string basePath)
        {
            var digestFiles = new List<string>();

            try
            {
                var verseFiles = Directory.GetFiles(basePath, "*.digest.verse", SearchOption.AllDirectories);
                digestFiles.AddRange(verseFiles);
            }
            catch (UnauthorizedAccessException)
            {
                // Silently ignore folders we don't have access to
            }

            return digestFiles;
        }

        private Dictionary<string, string> ExtractSettings(JsonElement settings)
        {
            var result = new Dictionary<string, string>();
            foreach (var property in settings.EnumerateObject())
            {
                result[property.Name] = property.Value.GetString() ?? string.Empty;
            }
            return result;
        }
    }

    /// <summary>
    /// Workspace metadata container
    /// </summary>
    public class WorkspaceMetadata
    {
        public List<string> FolderPaths { get; set; } = new();
        public string VerseFolderPath { get; set; } = string.Empty;
        public Dictionary<string, string> Settings { get; set; } = new();
    }
}
