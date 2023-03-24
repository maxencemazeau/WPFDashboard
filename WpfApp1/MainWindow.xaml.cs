using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static WpfApp1.MainWindow;


namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
            ;

            var converter = new BrushConverter();
            ObservableCollection<Members> members = new ObservableCollection<Members>();

            //Create DataGrid Info
            members.Add(new Members { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "John Doe", Position = "Coach", Email = "", Phone = "None" });
            members.Add(new Members { Number = "2", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "Reza Avali", Position = "Administrator", Email = "", Phone = "None" });
            members.Add(new Members { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "Dennis Castillo", Position = "Coach", Email = "", Phone = "None" });
            members.Add(new Members { Number = "4", Character = "G", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "Gabriel Cox", Position = "Coach", Email = "", Phone = "None" });
            members.Add(new Members { Number = "5", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "Lena Jones", Position = "Director", Email = "", Phone = "None" });
            members.Add(new Members { Number = "6", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "Benjamin Ben", Position = "Manager", Email = "", Phone = "None" });

            membersDataGrid.ItemsSource= members;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;

                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        public class Members
        {
            public string Character { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Brush BgColor { get; set; }
        }

        private async void btnGetMembers_Click(object sender, RoutedEventArgs e)
        {
            // Create an HTTP client to make requests to the API
            var httpClient = new HttpClient();

            // Set the base URL for the API
            httpClient.BaseAddress = new Uri("http://localhost:8080");

            // Make a GET request to the API to retrieve the list of members
            HttpResponseMessage response = await httpClient.GetAsync("/membre");

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a list of member objects
                List<Membre> members = JsonConvert.DeserializeObject<List<Membre>>(responseContent);

                // Display the list of members in a ListBox
                lbMembers.ItemsSource = members;
            }
            else
            {
                // Display an error message if the API request was not successful
                MessageBox.Show("Error retrieving members from API: " + response.ReasonPhrase);
            }
        }
    }

    // Define a Member class to represent the members returned by the API
    public class Membre
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
    }
}



}

