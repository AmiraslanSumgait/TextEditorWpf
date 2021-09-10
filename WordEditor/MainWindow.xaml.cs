using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
namespace WordEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string FileName { get; set; }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files|*.*|Text files|*.txt";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    TextBox.Text = sr.ReadToEnd();
                }
            }
        }
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputName.txt";
            save.Filter = "Text File | *.txt";
            if (string.IsNullOrWhiteSpace(FilePath.Text))
            {
                if (save.ShowDialog() == true)
                {
                    StreamWriter writer = new StreamWriter(save.OpenFile());
                    writer.WriteLine(TextBox.Text);
                    FilePath.Text = save.FileName;
                    writer.Dispose();
                    writer.Close();
                    MessageBox.Show("File succesfully saved", "Done");
                }
            }
            else File.WriteAllText(FilePath.Text, TextBox.Text); 
        }
        private void btnTextboxProcess_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender) as Button;
            if (button.Content.ToString() == "Cut") TextBox.Cut();
            else if (button.Content.ToString() == "Copy") TextBox.Copy();
            else if (button.Content.ToString() == "Paste") TextBox.Paste();
            else if (button.Content.ToString() == "Select All") TextBox.SelectAll();
            TextBox.Focus();
        }
        private bool AutoSave;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AutoSave == true) btnSaveFile_Click(null,null);
        }

        private void ToggleButtonAutoSave_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonAutoSave.IsChecked == true)
            {
                if (!string.IsNullOrWhiteSpace(FilePath.Text)) AutoSave = true;
                else
                {
                    MessageBox.Show("Your File is empty.Plese determine your file path","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                    AutoSave = false;
                    ToggleButtonAutoSave.IsChecked = false;
                }
            }
        }
    }
}
