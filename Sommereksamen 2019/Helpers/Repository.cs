using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.Win32;
using Sommereksamen_2019.Model;

namespace Sommereksamen_2019.Helpers
{
    public class Repository
    {
        public string CurrentFileName { get; set; }

        public Repository()
        {
            CurrentFileName = "";
        }

        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(ObservableCollection<TreeData>));

        public async Task<ObservableCollection<TreeData>> GetData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory + "\\data"
            };
            var tempData = new ObservableCollection<TreeData>();
            if (openFileDialog.ShowDialog() != false)
            {
                CurrentFileName = openFileDialog.FileName;
                TextReader reader = new StreamReader(CurrentFileName);
                tempData = (ObservableCollection<TreeData>)_serializer.Deserialize(reader);
                reader.Close();
                MessageBox.Show("File loaded successfully",
                    "File Loaded",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("File not found",
                    "File not found",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

            return tempData;
        }

        public void SaveData(ObservableCollection<TreeData> data)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\data";

            if (saveFileDialog.ShowDialog() == false) return;
            CurrentFileName = saveFileDialog.FileName;
            TextWriter writer = new StreamWriter(CurrentFileName);
            _serializer.Serialize(writer, TreeData.Trees);
            writer.Close();
            MessageBox.Show("File Saved", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SaveData(ObservableCollection<TreeData> data, string fileName)
        {
            CurrentFileName = fileName;
            TextWriter writer = new StreamWriter(CurrentFileName);
            _serializer.Serialize(writer, TreeData.Trees);
            writer.Close();
            MessageBox.Show("File Saved", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async Task<ObservableCollection<TreeData>> UpdateData()
        {
            TextReader reader = new StreamReader(CurrentFileName);
            var tempData = (ObservableCollection<TreeData>)_serializer.Deserialize(reader);
            reader.Close();

            return tempData;
        }
    }
}