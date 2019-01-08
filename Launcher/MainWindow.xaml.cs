using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Drawing;
using Path = System.IO.Path;
using System.Windows.Interop;
using Image = System.Windows.Controls.Image;
using Brushes = System.Windows.Media.Brushes;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<string> exefiles = new List<string>();
        static string path;
        public MainWindow()
        {
            InitializeComponent();
            Vypis();
        }
        public void Vypis()
        {
            try
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string usrname = userName.Substring(userName.IndexOf(@"\")); // D:\username neexistuje
                path = @"D:\source\repos";//+usrname;
                int i = 0;
                string[] files = Directory.GetFiles(path, "*.sln", SearchOption.AllDirectories);
                foreach (string item in files)
                {
                    string subpath = Path.GetDirectoryName(item);
                    string[] subfiles = Directory.GetFiles(subpath, "*.exe", SearchOption.AllDirectories);
                    foreach (string subitem in subfiles)
                    {
                        if (Path.GetFileNameWithoutExtension(subitem) == Path.GetFileNameWithoutExtension(item) && subitem.Contains("bin"))
                        {
                            PridatButton(i, subitem);
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Content = ex.Message;
            }
        }
        public void PridatButton(int i, string subitem)
        {
            Grid newGrid = Generator.GenerovatGridSButtonem(i, subitem);
            exefiles.Add(subitem);
            Wrap.Children.Add(newGrid);
        }
        public static void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string nazev = button.Name.ToString();
            int execislo = int.Parse(nazev.Remove(0, 6));
            Process proc = new Process();
            proc.StartInfo.FileName = Path.GetFileName(exefiles[execislo]);
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(exefiles[execislo]);
            proc.Start();
        }

        public static void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            ContextMenu contextMenu = Btn.ContextMenu;
            contextMenu.PlacementTarget = Btn;
            contextMenu.IsOpen = true;
        }

        public static void MenuItem1Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string nazev = item.Name;
            int execislo = int.Parse(nazev.Remove(0, 9));
            Process.Start("explorer.exe", Path.GetDirectoryName(exefiles[execislo]));
        }
        public static void MenuItem2Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string nazev = item.Name;
            int execislo = int.Parse(nazev.Remove(0, 9));
            string nazevsouboru = Path.GetFileNameWithoutExtension(exefiles[execislo]);
            string[] files = Directory.GetFiles(path, nazevsouboru+".sln", SearchOption.AllDirectories);
            Process.Start("explorer.exe", Path.GetDirectoryName(files[0]));
        }
    }
}
