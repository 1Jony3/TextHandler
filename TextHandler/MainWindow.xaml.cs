using Microsoft.Win32;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain;

namespace TextHandler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int FileCount = 0;

        Invoker invoker = new Invoker();
        Receiver receiver = new Receiver();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            string filename = OpenFile();
            TextBoxOpen.Text = filename;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string filename = OpenFile();
            TextBoxSave.Text = filename; 
        }

        private string OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                FileName = "Document",
                Multiselect = true,
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dialog.ShowDialog();

            string fileName = "";
            int fileCount = 0;

            if (result == true)
            {
                              
                foreach (string filePath in dialog.FileNames)
                {
                    fileName += filePath + " ";
                    fileCount++;
                }
                if (FileCount == 0) FileCount = fileCount;
                if (FileCount != 0 && FileCount == fileCount)
                    return fileName.Trim();
                else 
                {
                    MessageBox.Show("Количество файлов для сохранения должно быть равно количеству файлов для открытия");
                    FileCount = 0;
                    return "...";
                }
            }
            else
            {
                MessageBox.Show("Произошла ошибка");
                return "...";
            }
        }

        private void convertFile_Click(object sender, RoutedEventArgs e)
        {
            string[] filePathOpen = TextBoxOpen.Text.Split(' ');
            string[] filePathSave = TextBoxSave.Text.Split(' ');
            int minLength = int.Parse(TextBoxMinLetter.Text);
            bool deletePunctuationMarks = CheckBox.IsChecked ?? true;

            invoker.SetOnStart(new Command(receiver, filePathOpen, filePathSave, minLength, deletePunctuationMarks));
            invoker.DoSomething();
        }

    }
}
