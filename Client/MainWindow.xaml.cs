using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Client.Dtos;
using Microsoft.Win32;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Web.InformationCards_Client client;
        OpenFileDialog _openFileDialog;
        public MainWindow()
        {
            client = new Web.InformationCards_Client("http://localhost:5000/");
            _openFileDialog = new OpenFileDialog() { Filter = "Image files(*.png;*.jpg;)|*.png;*.jpg|All files (*.*)|*.*" };
            InitializeComponent();
        }
        private async void ClickRead(object sender, RoutedEventArgs e)
        {
            ReadPanel.Children.Clear();
            if (ReadName.Text.Length > 0 && !ReadName.Text.All(x => x == ' '))
            {
                try
                {
                    var cardsEnumerable = await client.ReadAsync(ReadName.Text);
                    var cards = cardsEnumerable.ToList();
                    for (int step = 0; step < cards.Count(); step++)
                    {
                        StackPanel panel = new StackPanel();
                        panel.Margin = new Thickness(0, 10, 0, 10);
                        panel.Background = new SolidColorBrush(Color.FromRgb(212, 105, 105));
                        TextBlock name = new TextBlock();
                        name.Text = cards[step].Name;
                        name.Margin = new Thickness(0, 10, 0, 10);
                        UIElement img;
                        try
                        {
                            img = Convert.FromBase64String(cards[step].Img).ToImage();
                        }
                        catch(NotSupportedException ex)
                        {
                            img = new TextBlock() { Text = $"Unsupported format - {ex.Message}" };
                        }
                        panel.Children.Add(name);
                        panel.Children.Add(img);
                        ReadPanel.Children.Add(panel);
                    }
                }
                catch (WebException ex)
                {
                    ReadPanel.Children.Add(new TextBlock() { Text = ex.Message });
                }
            }
            else
            {
                ReadPanel.Children.Add(new TextBlock() { Text = "The field is empty" });
            }
        }
        private async void ClickReadAll(object sender, RoutedEventArgs e)
        {
            ReadAllPanel.Children.Clear();
            try
            {
                var cardsEnumerable = await client.ReadAllAsync();
                var cards = cardsEnumerable.ToList();
                for (int step = 0; step < cards.Count(); step++)
                {
                    StackPanel panel = new StackPanel();
                    panel.Margin = new Thickness(0, 10, 0, 10);
                    panel.Background = new SolidColorBrush(Color.FromRgb(212, 105, 105));
                    TextBlock name = new TextBlock();
                    name.Text = cards[step].Name;
                    name.Margin = new Thickness(0, 10, 0, 10);
                    UIElement img;
                    try
                    {
                        img = Convert.FromBase64String(cards[step].Img).ToImage();
                    }
                    catch (NotSupportedException ex)
                    {
                        img = new TextBlock() { Text = $"Unsupported format - {ex.Message}" };
                    }
                    panel.Children.Add(name);
                    panel.Children.Add(img);
                    ReadAllPanel.Children.Add(panel);
                }
            }
            catch (WebException ex)
            {
                ReadAllPanel.Children.Add(new TextBlock() { Text = ex.Message });
            }
        }
        private string _pickingImgCreate = "";
        private void PickImgCreate(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(_openFileDialog.FileName, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    _pickingImgCreate = Convert.ToBase64String(bytes);
                    CreateIndicator.Fill = new SolidColorBrush() { Color = Color.FromRgb(16, 191, 45) };
                }
            }
        }
        private async void ClickCreate(object sender, RoutedEventArgs e)
        {
            CreatePanel.Children.Clear();
            try
            {
                if (_pickingImgCreate != "")
                {
                    await client.CreateAsync(new Dtos.InformationCardDto() { Name = CreateName.Text, Img = _pickingImgCreate });
                    CreatePanel.Children.Add(new TextBlock() { Text = "Created" });
                    _pickingImgCreate = "";
                    CreateIndicator.Fill = new SolidColorBrush() { Color = Color.FromRgb(139, 0, 0) };
                }
                else CreatePanel.Children.Add(new TextBlock() { Text = "No image picked" });
            }
            catch (WebException ex)
            {
                CreatePanel.Children.Add(new TextBlock() { Text = ex.Message });
            }
        }
        private string _pickingImgUpdate = "";
        private void PickImgUpdate(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(_openFileDialog.FileName, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    _pickingImgUpdate = Convert.ToBase64String(bytes);
                }
                UpdateIndicator.Fill = new SolidColorBrush() { Color = Color.FromRgb(16, 191, 45) };
            }
        }
        private async void ClickUpdate(object sender, RoutedEventArgs e)
        {
            UpdatePanel.Children.Clear();
            try
            {
                if (_pickingImgUpdate != "")
                {
                    await client.UpdateAllAsync(SearchName.Text, new Dtos.InformationCardDto() { Name = UpdateName.Text, Img = _pickingImgUpdate });
                    UpdatePanel.Children.Add(new TextBlock() { Text = "All Updated" });
                    _pickingImgUpdate = "";
                    UpdateIndicator.Fill = new SolidColorBrush() { Color = Color.FromRgb(139, 0, 0) };
                }
                else UpdatePanel.Children.Add(new TextBlock() { Text = "No image picked" });
            }
            catch (WebException ex)
            {
                UpdatePanel.Children.Add(new TextBlock() { Text = ex.Message });
            }
        }
        private async void ClickDelete(object sender, RoutedEventArgs e)
        {
            DeletePanel.Children.Clear();
            try
            {
                await client.RemoveAllAsync(DeleteName.Text);
                DeletePanel.Children.Add(new TextBlock() { Text = "All Deleted" });
            }
            catch (WebException ex)
            {
                DeletePanel.Children.Add(new TextBlock() { Text = ex.Message });
            }
        }
        private void ClearRead(object sender, RoutedEventArgs e)
        {
            ReadPanel.Children.Clear();
        }
        private void ClearReadAll(object sender, RoutedEventArgs e)
        {
            ReadAllPanel.Children.Clear();
        }
        private void SortCards(object sender, RoutedEventArgs e)
        {
            var list = ReadAllPanel.Children.Cast<StackPanel>().OrderBy(panel => ((TextBlock)panel.Children[0]).Text).ToArray();
            ReadAllPanel.Children.Clear();
            foreach (var panel in list) ReadAllPanel.Children.Add(panel);
        }
    }
}
