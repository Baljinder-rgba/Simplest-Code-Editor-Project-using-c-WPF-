using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace bEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFilePath = null;
        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme();
        }

        // Drag Window 
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
        // Color Theme
        private void ApplyTheme()
        {
            var uri = new System.Uri("Themes/ModernAesthetic.xshd", System.UriKind.Relative);

            using var stream = System.Windows.Application.GetResourceStream(uri).Stream;
            using var reader = new XmlTextReader(stream);

            var highlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            TextEditor.SyntaxHighlighting = highlighting;
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Open File Options
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*|C# Files (*.cs)|*.cs|Text Files (*.txt)|*.txt",
                Title = "Open File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string content = File.ReadAllText(openFileDialog.FileName);
                TextEditor.Text = content;
            }
        }

        // Save File Options
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            if (currentFilePath == null)
            {
                SaveAs();
                return;
            }

            File.WriteAllText(currentFilePath, TextEditor.Text, Encoding.UTF8);
        }

        // 📂 Save As dialog
        private void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|C Source File |*.c|C++ Source File |*.cpp|No Format |*.",
                Title = "Save File"
            };

            if (dialog.ShowDialog() == true)
            {
                currentFilePath = dialog.FileName;
                File.WriteAllText(currentFilePath, TextEditor.Text, new UTF8Encoding(false)
);
            }
        }

        private void OpenTerminal(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WorkingDirectory = "C:\\",
                UseShellExecute = true
            });
        }

    }
}