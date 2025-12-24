using System.Windows;

namespace VerseVisualBlueprintEditor.UI.Windows
{
    public partial class NewGraphWindow : Window
    {
        public string GraphName { get; private set; } = string.Empty;
        public string ClassName { get; private set; } = string.Empty;

        public NewGraphWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter a blueprint name");
                return;
            }

            GraphName = NameTextBox.Text;
            ClassName = ClassNameTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
