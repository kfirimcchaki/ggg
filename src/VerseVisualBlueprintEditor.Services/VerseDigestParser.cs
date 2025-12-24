using System.Text.RegularExpressions;
using VerseVisualBlueprintEditor.Core.Models;

namespace VerseVisualBlueprintEditor.Services
{
    /// <summary>
    /// Service for parsing Verse digest files
    /// </summary>
    public class VerseDigestParser
    {
        public VerseDiagent ParseDigestFile(string filePath)
        {
            var digest = new VerseDiagent();
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Digest file not found: {filePath}");

            var content = File.ReadAllText(filePath);
            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            digest.ModulePath = ExtractModulePath(content);
            digest.Classes = ExtractClasses(content);
            digest.Functions = ExtractFunctions(content);
            digest.Events = ExtractEvents(content);

            return digest;
        }

        private string ExtractModulePath(string content)
        {
            var match = Regex.Match(content, @"# Module import path:\s*(.+)");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        private List<VerseClass> ExtractClasses(string content)
        {
            var classes = new List<VerseClass>();
            var classPattern = @"(\w+)<[^>]*>\s*:=\s*class<[^>]*>\s*(?:\([^)]*\))?:\s*(.+?)(?=\n\s{0,4}\w+\s*<|$)";
            var matches = Regex.Matches(content, classPattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                var className = match.Groups[1].Value;
                if (!className.EndsWith("_component") && !className.EndsWith("_device"))
                    continue;

                var classBody = match.Groups[2].Value;
                var verseClass = new VerseClass
                {
                    Name = className,
                    FullPath = className,
                    IsPublic = classBody.Contains("<public>"),
                    IsNative = classBody.Contains("<native>"),
                    Description = ExtractDescription(classBody)
                };

                verseClass.Properties = ExtractProperties(classBody);
                verseClass.Methods = ExtractMethods(classBody);

                classes.Add(verseClass);
            }

            return classes;
        }

        private List<VerseProperty> ExtractProperties(string content)
        {
            var properties = new List<VerseProperty>();
            var propPattern = @"(?:@editable\s+)?var<[^>]*>\s+(\w+)<[^>]*>:([^\n=]+)";
            var matches = Regex.Matches(content, propPattern);

            foreach (Match match in matches)
            {
                var prop = new VerseProperty
                {
                    Name = match.Groups[1].Value,
                    Type = match.Groups[2].Value.Trim(),
                    IsEditable = content.Contains("@editable"),
                    IsPublic = content.Contains("<public>")
                };
                properties.Add(prop);
            }

            return properties;
        }

        private List<VerseFunction> ExtractMethods(string content)
        {
            var methods = new List<VerseFunction>();
            var methodPattern = @"(\w+)<[^>]*>\(([^)]*)\)\s*(?::<[^>]*>)?\s*:([^\n]+)";
            var matches = Regex.Matches(content, methodPattern);

            foreach (Match match in matches)
            {
                var method = new VerseFunction
                {
                    Name = match.Groups[1].Value,
                    ReturnType = match.Groups[3].Value.Trim(),
                    IsPublic = content.Contains("<public>"),
                    IsNative = content.Contains("<native>")
                };

                var paramsStr = match.Groups[2].Value;
                if (!string.IsNullOrEmpty(paramsStr))
                {
                    var paramParts = paramsStr.Split(',');
                    foreach (var param in paramParts)
                    {
                        var paramMatch = Regex.Match(param, @"(\w+):(.+)");
                        if (paramMatch.Success)
                        {
                            method.Parameters.Add(new VerseParameter
                            {
                                Name = paramMatch.Groups[1].Value.Trim(),
                                Type = paramMatch.Groups[2].Value.Trim()
                            });
                        }
                    }
                }

                methods.Add(method);
            }

            return methods;
        }

        private List<VerseEvent> ExtractEvents(string content)
        {
            var events = new List<VerseEvent>();
            var eventPattern = @"(\w+Event)<[^>]*>:listenable\(([^)]*)\)";
            var matches = Regex.Matches(content, eventPattern);

            foreach (Match match in matches)
            {
                var evt = new VerseEvent
                {
                    Name = match.Groups[1].Value,
                    EventType = match.Groups[2].Value,
                    IsPublic = true
                };
                events.Add(evt);
            }

            return events;
        }

        private string ExtractDescription(string content)
        {
            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.TrimStart().StartsWith("#"))
                {
                    return line.TrimStart().Substring(1).Trim();
                }
            }
            return string.Empty;
        }
    }
}
