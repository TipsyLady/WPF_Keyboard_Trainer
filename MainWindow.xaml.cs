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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Keyboard_Trainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Key,Button> All_buttons;
        DispatcherTimer timer = new DispatcherTimer();
        int tmp_timer = 0;
        int fail;
        string Upper_str = "QWERTYUIOPASDFGHJKLZXCVBNM ";
        string Lower_str = "qwertyuiopasdfghjklzxcvbnm ";
        string Diggit_str = "1234567890 ";
        string Char_str = "!@#$%^&*()~`{[}]:;\"'<,>.?/|\\ ";
        Random rand;

        public MainWindow()
        {
            InitializeComponent();
            All_buttons = new Dictionary<Key, Button>();
            CollectAllButtons();
            TextBox1.IsEnabled = false;
            TextBox2.IsEnabled = false;
            Button_Stop.IsEnabled = false;
            Button_Start.IsEnabled = true;
            Button_Capital.IsEnabled = true;
            Slider.IsEnabled = true;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tmp_timer++;
            _speed.Content = Math.Round(((double)TextBox2.Text.Length / tmp_timer) * 60).ToString();
        }

        private void CollectAllButtons()
        {
            All_buttons[Key.Oem3] = Oem3;
            All_buttons[Key.D1] = D1;
            All_buttons[Key.D2] = D2;
            All_buttons[Key.D3] = D3;
            All_buttons[Key.D4] = D4;
            All_buttons[Key.D5] = D5;
            All_buttons[Key.D6] = D6;
            All_buttons[Key.D7] = D7;
            All_buttons[Key.D8] = D8;
            All_buttons[Key.D9] = D9;
            All_buttons[Key.D0] = D0;
            All_buttons[Key.OemMinus] = OemMinus;
            All_buttons[Key.OemPlus] = OemPlus;
            All_buttons[Key.Back] = Backspace;
            All_buttons [Key.Tab] = Tab;
            All_buttons [Key.Q] = Q;
            All_buttons [Key.W] = W;
            All_buttons [Key.E] = E;
            All_buttons [Key.R] = R;
            All_buttons [Key.T] = T;
            All_buttons [Key.Y] = Y;
            All_buttons [Key.U] = U;
            All_buttons [Key.I] = I;
            All_buttons [Key.O] = O;
            All_buttons [Key.P] = P;
            All_buttons [Key.OemOpenBrackets] = OemOpenBrackets;
            All_buttons [Key.OemCloseBrackets] = OemCloseBrackets;
            All_buttons [Key.Oem5] = Oem5;
            All_buttons [Key.Capital] = Capital;
            All_buttons[Key.A] = A;
            All_buttons[Key.S] = S;
            All_buttons[Key.D] = D;
            All_buttons[Key.F] = F;
            All_buttons[Key.G] = G;
            All_buttons[Key.H] = H;
            All_buttons[Key.J] = J;
            All_buttons[Key.K] = K;
            All_buttons[Key.L] = L;
            All_buttons [Key.Oem1] = Oem1;
            All_buttons [Key.OemQuotes] = OemQuotes;
            All_buttons [Key.Return] = Return;
            All_buttons [Key.LeftShift] = LeftShift;
            All_buttons[Key.Z] = Z;
            All_buttons[Key.X] = X;
            All_buttons[Key.C] = C;
            All_buttons[Key.V] = V;
            All_buttons[Key.B] = B;
            All_buttons[Key.N] = N;
            All_buttons[Key.M] = M;
            All_buttons[Key.OemComma] = OemComma;
            All_buttons[Key.OemPeriod] = OemPeriod;
            All_buttons[Key.OemQuestion] = OemQuestion;
            All_buttons[Key.RightShift] = RightShift;
            All_buttons[Key.LeftCtrl] = LeftCtrl;
            All_buttons[Key.LWin] = LWin;
            All_buttons[Key.LeftAlt] = System1;
            All_buttons[Key.Space] = Space;
            All_buttons[Key.RightAlt] = System2;
            All_buttons[Key.RWin] = RWin;
            All_buttons[Key.RightCtrl] = RightCtrl;
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void Start()
        {
            Button_Start.IsEnabled = false;
            Button_Capital.IsEnabled = false;
            Button_Stop.IsEnabled = true;
            Slider.IsEnabled = false;
            TextBox1.IsEnabled = true;
            TextBox2.IsEnabled = true;
            TextBox2.Focus();
            RandomText(Convert.ToInt32(Difficulty.Content));
            tmp_timer = 0;
            timer.Start();
        }
        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            MessageBox.Show($"You have written the all text. " +
                    $"Your spees is = {_speed.Content}" +
                    $"Your fails are = {fails.Content}", "Finished", MessageBoxButton.OK);
            TextBox2.Clear();
            fails.Content = 0;
            _speed.Content = 0;
            Button_Start.IsEnabled = true;
            Button_Capital.IsEnabled = true;
            Button_Stop.IsEnabled = false;
            Slider.IsEnabled = true;
            TextBox2.IsEnabled = false;
            timer.Stop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            All_buttons[e.Key].Effect = new DropShadowEffect();
            bool capital = Keyboard.IsKeyToggled(Key.Capital);

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                foreach (Button item in All_buttons.Values)
                {
                    if (item.Content.ToString().Length == 1)
                        item.Content = item.Content.ToString().ToUpper();
                    Upper_Chars();
                }
            }
            if (!capital)
            {
                foreach (Button item in All_buttons.Values)
                {
                    if (item.Content.ToString().Length == 1)
                        item.Content = item.Content.ToString().ToUpper();
                }
            }
           
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            All_buttons[e.Key].Effect = null;
            bool capital = Keyboard.IsKeyToggled(Key.Capital);

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                    foreach (Button item in All_buttons.Values)
                    {
                    if(item.Content.ToString().Length==1)
                        item.Content = item.Content.ToString().ToLower();
                    Lower_Chars();
                    }
            }
            if (capital)
            {
                foreach (Button item in All_buttons.Values)
                {
                    if (item.Content.ToString().Length == 1)
                        item.Content = item.Content.ToString().ToLower();
                    Lower_Chars();
                }
            }
            
        }

        private void RandomText(int Difficulty)
        {
            TextBox1.Clear();
            rand = new Random();
            string str = "";
            switch (Difficulty)
            {
                case 1:
                    
                    for (int i = 0; i < 15; i++)
                    {
                        if (Button_Capital.IsChecked == true)
                            str += Upper_str[rand.Next(0, Upper_str.Length)];
                        str += Lower_str[rand.Next(0, Lower_str.Length)];
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        TextBox1.Text += str[rand.Next(0, str.Length)];
                        if (i % 3 == 0)
                            TextBox1.Text += " ";
                    }
                    break;
                case 2:
                    for (int i = 0; i < 20; i++)
                    {
                        if (Button_Capital.IsChecked == true)
                            str += Upper_str[rand.Next(0, Upper_str.Length)];
                        str += Lower_str[rand.Next(0, Lower_str.Length)];
                        str += Diggit_str[rand.Next(0,Diggit_str.Length)];
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        TextBox1.Text += str[rand.Next(0, str.Length)];
                        if (i % 5 == 0)
                            TextBox1.Text += " ";
                    }
                    break;
                case 3:
                    for (int i = 0; i < 25; i++)
                    {
                        if(Button_Capital.IsChecked==true)
                            str += Upper_str[rand.Next(0,Upper_str.Length)];
                        str += Lower_str[rand.Next(0, Lower_str.Length)];
                        str += Diggit_str[rand.Next(0, Diggit_str.Length)];
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        TextBox1.Text += str[rand.Next(0, str.Length)];
                        if (i % 7 == 0)
                            TextBox1.Text += " ";
                    }
                    break;
                case 4:
                    for (int i = 0; i < 30; i++)
                    {
                        if (Button_Capital.IsChecked == true)
                            str += Upper_str[rand.Next(0, Upper_str.Length)];
                        str += Lower_str[rand.Next(0, Lower_str.Length)];
                        str += Diggit_str[rand.Next(0, Diggit_str.Length)];
                        str += Char_str[rand.Next(0, Char_str.Length)];
                    }
                    for (int i = 0; i < 35; i++)
                    {
                        TextBox1.Text += str[rand.Next(0, str.Length)];
                        if (i % 9 == 0)
                            TextBox1.Text += " ";
                    }
                    break;
                case 5:
                    for (int i = 0; i < 35; i++)
                    {
                        if(Button_Capital.IsChecked == true)
                            str += Upper_str[rand.Next(0, Upper_str.Length)];
                        str += Lower_str[rand.Next(0, Lower_str.Length)];
                        str += Diggit_str[rand.Next(0, Diggit_str.Length)];
                        str += Char_str[rand.Next(0, Char_str.Length)];
                        
                    }
                    for (int i = 0; i < 40; i++)
                    {
                        TextBox1.Text += str[rand.Next(0, str.Length)];
                        if (i % 11 == 0)
                            TextBox1.Text += " ";
                    }
                    break;
            }


        }

        private void Upper_Chars()
        {
            Oem3.Content = "~";
            D1.Content = "!";
            D2.Content = "@";
            D3.Content = "#";
            D4.Content = "$";
            D5.Content = "%";
            D6.Content = "^";
            D7.Content = "&";
            D8.Content = "*";
            D9.Content = "(";
            D0.Content = ")";
            OemMinus.Content = "_";
            OemPlus.Content = "+";
            OemOpenBrackets.Content = "{";
            OemCloseBrackets.Content = "}";
            Oem5.Content = "|";
            Oem1.Content = ":";
            OemQuotes.Content = "\"";
            OemComma. Content = "<";
            OemPeriod. Content = ">";
            OemQuestion. Content = "?";
        }
        private void Lower_Chars()
        {
            Oem3.Content = "`";
            D1.Content = "1";
            D2.Content = "2";
            D3.Content = "3";
            D4.Content = "4";
            D5.Content = "5";
            D6.Content = "6";
            D7.Content = "7";
            D8.Content = "8";
            D9.Content = "9";
            D0.Content = "0";
            OemMinus.Content = "-";
            OemPlus.Content = "=";
            OemOpenBrackets.Content = "[";
            OemCloseBrackets.Content = "]";
            Oem5.Content = "\\";
            Oem1.Content = ";";
            OemQuotes.Content = "'";
            OemComma.Content = ",";
            OemPeriod.Content = ".";
            OemQuestion.Content = "/";
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < TextBox2.Text.Length; i++)
            {
                if(TextBox1.Text.Substring(i,1)==TextBox2.Text.Substring(i,1))
                {
                    TextBox2.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    TextBox2.Foreground = new SolidColorBrush(Colors.Red);
                    fail++;
                }
            }
            fails.Content = fail;

            if(TextBox1.Text.Length==TextBox2.Text.Length)
            {
                Stop();
            }

        }
    }
}
