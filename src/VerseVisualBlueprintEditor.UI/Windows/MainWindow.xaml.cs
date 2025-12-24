using System.Windows;
using Microsoft.Win32;
using VerseVisualBlueprintEditor.Core.Models;
using VerseVisualBlueprintEditor.Services;

namespace VerseVisualBlueprintEditor.UI.Windows
{
    public partial class MainWindow : Window
    {
        private BlueprintGraphService _blueprintService = new();
        private VerseCodeGenerator _codeGenerator = new();
        private VerseDigestParser _digestParser = new();
        private WorkspaceService _workspaceService = new();
        private BlueprintGraph _currentGraph = new();
        private List<VerseClass> _loadedClasses = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _currentGraph = _blueprintService.CreateNewGraph("MyDevice");
            RefreshUI();
        }

        private void RefreshUI()
        {
            VariablesList.ItemsSource = null;
            VariablesList.ItemsSource = _currentGraph.Variables;

            ClassesList.ItemsSource = null;
            ClassesList.ItemsSource = _loadedClasses;
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewGraphWindow();
            if (window.ShowDialog() == true)
            {
                _currentGraph = _blueprintService.CreateNewGraph(window.GraphName);
                RefreshUI();
                MessageBox.Show("New blueprint created!");
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Blueprint Files (*.blueprint)|*.blueprint|All Files (*.*)|*.*",
                DefaultExt = ".blueprint"
            };

            if (dialog.ShowDialog() == true)
            {
                _currentGraph = _blueprintService.LoadGraph(dialog.FileName);
                RefreshUI();
                MessageBox.Show("Blueprint loaded!");
            }
        }

        private void LoadWorkspace_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Workspace Files (*.code-workspace)|*.code-workspace",
                DefaultExt = ".code-workspace"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var workspace = _workspaceService.LoadWorkspace(dialog.FileName);
                    LoadVerseDgests(workspace.VerseFolderPath);
                    MessageBox.Show("Workspace loaded successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading workspace: {ex.Message}");
                }
            }
        }

        private void LoadVerseDgests(string basePath)
        {
            try
            {
                var digestFiles = _workspaceService.FindVerseDdigestFiles(basePath);
                _loadedClasses.Clear();

                foreach (var digestFile in digestFiles)
                {
                    var digest = _digestParser.ParseDigestFile(digestFile);
                    _loadedClasses.AddRange(digest.Classes);
                }

                RefreshUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading digests: {ex.Message}");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Blueprint Files (*.blueprint)|*.blueprint",
                DefaultExt = ".blueprint"
            };

            if (dialog.ShowDialog() == true)
            {
                _blueprintService.SaveGraph(_currentGraph, dialog.FileName);
                MessageBox.Show("Blueprint saved!");
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Verse Files (*.verse)|*.verse",
                DefaultExt = ".verse"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _codeGenerator.ExportToFile(_currentGraph, dialog.FileName);
                    MessageBox.Show("Verse code exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting code: {ex.Message}");
                }
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings dialog would go here");
        }

        private void AddVariable_Click(object sender, RoutedEventArgs e)
        {
            var variable = new VerseVariable
            {
                Name = "NewVariable",
                Type = "string",
                IsEditable = true,
                IsPublic = true
            };

            _currentGraph.Variables.Add(variable);
            RefreshUI();
        }

        private void RemoveVariable_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is VerseVariable variable)
            {
                _currentGraph.Variables.Remove(variable);
                RefreshUI();
            }
        }

        private void VariablesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (VariablesList.SelectedItem is VerseVariable variable)
            {
                PopulatePropertyPanel(variable);
            }
        }

        private void PopulatePropertyPanel(VerseVariable variable)
        {
            PropertiesPanel.Children.Clear();

            AddPropertyControl(PropertiesPanel, "Name:", variable.Name, (v) => variable.Name = v);
            AddPropertyControl(PropertiesPanel, "Type:", variable.Type, (v) => variable.Type = v);
            AddPropertyControl(PropertiesPanel, "Default Value:", variable.DefaultValue, (v) => variable.DefaultValue = v);

            var editableCheckBox = new CheckBox
            {
                Content = "Is Editable",
                IsChecked = variable.IsEditable,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
            };
            editableCheckBox.Checked += (s, e) => variable.IsEditable = true;
            editableCheckBox.Unchecked += (s, e) => variable.IsEditable = false;
            PropertiesPanel.Children.Add(editableCheckBox);

            var publicCheckBox = new CheckBox
            {
                Content = "Is Public",
                IsChecked = variable.IsPublic,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)),
                Margin = new Thickness(0, 4, 0, 0)
            };
            publicCheckBox.Checked += (s, e) => variable.IsPublic = true;
            publicCheckBox.Unchecked += (s, e) => variable.IsPublic = false;
            PropertiesPanel.Children.Add(publicCheckBox);
        }

        private void AddPropertyControl(StackPanel panel, string label, string value, Action<string> setter)
        {
            var container = new StackPanel { Margin = new Thickness(0, 4, 0, 0) };

            var labelControl = new TextBlock
            {
                Text = label,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 176, 176)),
                FontSize = 11,
                Margin = new Thickness(0, 0, 0, 2)
            };
            container.Children.Add(labelControl);

            var textBox = new TextBox
            {
                Text = value,
                Style = (System.Windows.Style)FindResource("UnrealTextBox")
            };
            textBox.TextChanged += (s, e) => setter(textBox.Text);
            container.Children.Add(textBox);

            panel.Children.Add(container);
        }

        private void ClassSearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var searchText = ClassSearchBox.Text.ToLower();
            var filtered = _loadedClasses
                .Where(c => c.Name.ToLower().Contains(searchText))
                .ToList();

            ClassesList.ItemsSource = null;
            ClassesList.ItemsSource = filtered;
        }
    }
}
