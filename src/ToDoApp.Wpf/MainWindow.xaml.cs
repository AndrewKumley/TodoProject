using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Wpf.Models;
using ToDoApp.Wpf.ViewModels;

namespace ToDoApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FilterTxt = "Text file (*.txt)|*.txt";
        private const string FilterJson = "JSON file (*.json)|*.json";
        private const string FilterXml = "XML file (*.xml)|*.xml";
        private const string FilterSoap = "SOAP file (*.soap)|*.soap";
        private const string FilterBinary = "Binary file (*.bin)|*.bin";
        private const string FilterAny = "Any files (*.*)|*.*";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainWindowViewModel();
        }

        //private void OnAddTodoTaskButtonClick(object sender, RoutedEventArgs e)
        //{
        //    TodoTask item = new TodoTask();
        //    item.Description = TodoTaskNameText.Text;
        //    TodoTaskListView.Items.Add(item);
        //}

        private void OnRemoveTodoTaskButtonClick(object sender, RoutedEventArgs e)
        {
            int index = TodoTaskListView.SelectedIndex;
            if (CanRemoveTodoTask(index))
                TodoTaskListView.Items.RemoveAt(index);
        }

        private void OnTodoTaskListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = TodoTaskListView.SelectedIndex;
            RemoveTodoTaskButton.IsEnabled = CanRemoveTodoTask(index);
        }

        private bool CanRemoveTodoTask(int selectedIndex)
        {
            // selected index to remove cannot be less than 0 or greater than item count
            return (selectedIndex >= 0 && selectedIndex < TodoTaskListView.Items.Count);
        }

        private void OnMainHelpAboutMenuClicked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("TodoApp_AndrewKumley",
                "About",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void OnMainFileSaveAsMenuClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();

            dialog.Filter = FilterTxt + "|" +
                            FilterJson + "|" +
                            FilterXml + "|" +
                            FilterSoap + "|" +
                            FilterBinary;

            dialog.FilterIndex = 1;

            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                string filePath = dialog.FileName;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                string content = "";

                List<TodoTask> items = new List<TodoTask>();
                ViewModels.MainWindowViewModel viewModel = (this.DataContext as ViewModels.MainWindowViewModel);
                foreach (var todoTaskViewModel in viewModel.TodoTaskItems)
                {
                    Models.TodoTask item = new Models.TodoTask();

                    item.Description = todoTaskViewModel.Description;
                    // todo: all properties
                    item.DueBy = todoTaskViewModel.DueBy;
                    item.IsCompleted = todoTaskViewModel.IsCompleted;
                    item.CompletedOn = todoTaskViewModel.CompletedOn;
                    items.Add(item);
                }

                if (dialog.FilterIndex == 1)
                {
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    
                    content = writer.ToString();

                    writer.Dispose();
                }
                else if (dialog.FilterIndex == 2)
                {
                    
                    string json = Newtonsoft
                        .Json
                        .JsonConvert
                        .SerializeObject(items);
                    content = json;
                }
                else if (dialog.FilterIndex == 3)
                {
                    System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(List<TodoTask>));
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    serializer.Serialize(stream, items);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);

                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    content = reader.ReadToEnd();

                    reader.Dispose();
                    stream.Dispose();
                }
                else if (dialog.FilterIndex == 4)
                {
                    //System.Runtime.Serialization.Formatters.Soap.SoapFormatter serializer =
                    //    new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
                    //System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    //serializer.Serialize(stream, document);
                    //stream.Seek(0, System.IO.SeekOrigin.Begin);

                    //System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    //content = reader.ReadToEnd();

                    //reader.Dispose();
                    //stream.Dispose();

                }
                else
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer =
                        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    serializer.Serialize(stream, items);

                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    content = Convert.ToBase64String(stream.ToArray());

                    stream.Dispose();
                }

                System.IO.File.WriteAllText(filePath, content);
            }
        }

        private void OnMainFileOpenMenuClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = FilterTxt + "|" +
                            FilterJson + "|" +
                            FilterXml + "|" +
                            FilterSoap + "|" +
                            FilterBinary + "|" +
                            FilterAny;

            dialog.FilterIndex = 1;

            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                string filePath = dialog.FileName;
                if (System.IO.File.Exists(filePath))
                {


                    //if (dialog.FilterIndex == 1)
                    //{
                    //    System.IO.StringReader reader = new System.IO.StringReader(content);
                    //    string line = reader.ReadLine();
                    //    System.DateTime lastSaved;
                    //    if (System.DateTime.TryParse(line, out lastSaved))
                    //    {
                    //        line = string.Empty;
                    //    }
                    //    else
                    //    {
                    //        lastSaved = System.DateTime.Now;
                    //    }

                    //    content = line + reader.ReadToEnd();
                    //    Models.Document document = new Models.Document(content);
                    //    document.LastSaved = lastSaved;
                    //    content = document.Content;

                    //    reader.Dispose();
                    //}
                    //else if (dialog.FilterIndex == 2)
                    //{
                    //    string json = content;
                    //    object jsonObject = Newtonsoft.Json
                    //        .JsonConvert
                    //        .DeserializeObject(json, typeof(Models.Document));
                    //    Models.Document document = (Models.Document)jsonObject;
                    //    content = document.Content;
                    //}
                    //else if (dialog.FilterIndex == 3)
                    //{
                    //    System.Xml.Serialization.XmlSerializer serializer =
                    //        new System.Xml.Serialization.XmlSerializer(typeof(Models.Document));

                    //    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(content);
                    //    System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                    //    object xmlObject = serializer.Deserialize(stream);
                    //    Models.Document document = (Models.Document)xmlObject;

                    //    content = document.Content;
                    //    stream.Dispose();
                    //}
                    //else if (dialog.FilterIndex == 4)
                    //{
                    //    //System.Runtime.Serialization.Formatters.Soap.SoapFormatter serializer =
                    //    //    new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();

                    //    //byte[] buffer = System.Text.Encoding.ASCII.GetBytes(content);
                    //    //System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                    //    //object soapObject = serializer.Deserialize(stream);
                    //    //Models.Document document = (Models.Document)soapObject;
                    //    //content = document.Content;

                    //    //stream.Dispose();
                    //}

                    //else if (dialog.FilterIndex == 5)
                    //{
                    //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer =
                    //        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    //    byte[] buffer = Convert.FromBase64String(content);
                    //    System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                    //    object binaryObject = serializer.Deserialize(stream);

                    //    Models.Document document = (Models.Document)binaryObject;
                    //    content = document.Content;
                    //    stream.Dispose();
                    //}
                    //else
                    //{
                        
                    //}

                    
                }
            }
        }
    }
}
