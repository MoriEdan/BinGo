using System;
using System.Collections.Generic;
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
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace binGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static List<KeyValuePair<string, string>> result;
        private static List<string> cache;
        public static string logFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +"\\temp\\";

        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;

            result = new List<KeyValuePair<string, string>>();
            cache = new List<string>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        // Code to allow drag and drop of the form
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    DragMove();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (key.Text == "Search")
            {
                key.Text = "";
            }
        }

        private void key_LostFocus(object sender, RoutedEventArgs e)
        {
            if (key.Text == "")
            {
                key.Text = "Search";
            }
        }

        private void key_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Topmost = true;

                this.Height = 47;
                if (key.Text.Trim().Length == 0) return;

                animateToTopIfNeeded(150);

                result_key.Items.Clear();
                result_value.Children.Clear();
                
                result = bingSearch.Search(key.Text.Trim());
                foreach (KeyValuePair<string, string> kvp in result)
                {
                    result_key.Items.Add(kvp.Key);
                }
                this.Height = 382;                
            }
        }

        private Image CreateImage(string src)
        {
            Image Mole = new Image();
            Mole.Width = 300;
            Mole.Height = 300;
            ImageSource MoleImage = new BitmapImage(new Uri(src));
            Mole.Source = MoleImage;
            return Mole;
        }

        private void animateToTopIfNeeded(float milliseconds) 
        {
            float dx = (float)(((int)this.Top - 100) / milliseconds);
            float pos = (float) this.Top;

            while (pos > 100)
            {
                pos -= dx;
                this.Top = pos;
                Thread.Sleep(1);
            }
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        private void result_key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (result_key.SelectedIndex >= 0)
            {
                var img = CreateImage(result.ElementAt(result_key.SelectedIndex).Value);
                result_value.Children.Clear();
                result_value.Children.Add(img);
            }
        }
    }

    class cacheWorker
    {
        public void performCache(List<KeyValuePair<string, string>> result)
        {
            if (!Directory.Exists(MainWindow.logFilesDir))
            {
                Directory.CreateDirectory(MainWindow.logFilesDir);
            }
        }
    }
}
