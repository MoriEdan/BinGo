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
using System.Windows.Input;
using System.Threading;

namespace binGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
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
                result r = new result();
                r.Show();
                this.Topmost = true;

                this.Height = 47;
                if (key.Text.Trim().Length == 0) return;

                animateToTopIfNeeded(150);

                result_key.Items.Clear();
                

                foreach (KeyValuePair<string, string> kvp in bingSearch.Search(key.Text.Trim()))
                {
                    var img = CreateImage(kvp.Value);
                    result_key.Items.Add(kvp.Key);
                    workspace.Children.Add(img);
                }
                this.Height = 382;                
            }
        }

        private Image CreateImage(string src)
        {
            Image Mole = new Image();
            Mole.Width = 100;
            Mole.Height = 100;
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
    }
}
