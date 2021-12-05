using System;
using System.Collections.Generic;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Web.InformationCards_Client client;
        public MainWindow()
        {
            client = new Web.InformationCards_Client("http://localhost:5000/");
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
                        TextBlock name = new TextBlock();
                        name.Text = cards[step].Name;
                        name.Margin = new Thickness(0, 10, 0, 10);
                        TextBlock img = new TextBlock();
                        img.Text = cards[step].Img;
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
                    TextBlock name = new TextBlock();
                    name.Text = cards[step].Name;
                    name.Margin = new Thickness(0, 10, 0, 10);
                    TextBlock img = new TextBlock();
                    img.Text = cards[step].Img;
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
        private async void ClickCreate(object sender, RoutedEventArgs e)
        {
            CreatePanel.Children.Clear();
            try
            {
                await client.CreateAsync(new Models.InformationCard() { Name = CreateName.Text, Img = CreateImg.Text });
                CreatePanel.Children.Add(new TextBlock() { Text = "Created" });
            }
            catch (WebException ex)
            {
                CreatePanel.Children.Add(new TextBlock() { Text = ex.Message });
            }
        }
        private async void ClickUpdate(object sender, RoutedEventArgs e)
        {
            UpdatePanel.Children.Clear();
            try
            {
                await client.UpdateAllAsync(SearchName.Text, new Models.InformationCard() { Name = UpdateName.Text, Img = UpdateImg.Text });
                UpdatePanel.Children.Add(new TextBlock() { Text = "All Updated" });
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
    }
}
