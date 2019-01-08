using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;

namespace Launcher
{
    class Generator
    {
        public static Grid GenerovatGridSButtonem(int i, string subitem)
        {
            Button newBtn = new Button
            {
                Name = "Button" + i.ToString(),
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent
            };
            newBtn.Click += new RoutedEventHandler(MainWindow.ButtonClick);
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(subitem);
            var bitmap = icon.ToBitmap();
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            StackPanel newStackPanel = new StackPanel
            {
                Width = 90,
                Height = 60
            };
            Image newImage = new Image
            {
                Source = bitmapSource,
                Width = 40,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Label newLabel = new Label
            {
                Content = Path.GetFileNameWithoutExtension(subitem),
                Width = 90,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            Grid newGrid = new Grid();
            Button newBtn2 = new Button
            {
                Name = "Button2" + i.ToString(),
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 20,
                Width = 20
            };
            newBtn2.Click += new RoutedEventHandler(MainWindow.TestBtn_Click);
            ContextMenu newContextMenu = new ContextMenu
            {
                Background = Brushes.White,
                HasDropShadow = false,
                BorderThickness = new Thickness(0)
            };
            MenuItem newMenuItem = new MenuItem
            {
                Name = "MenuItem1" + i.ToString(),
                Header = "Otevřít umístění"
            };
            newMenuItem.Click += new RoutedEventHandler(MainWindow.MenuItem1Click);
            MenuItem newMenuItem2 = new MenuItem
            {
                Name = "MenuItem2" + i.ToString(),
                Header = "Otevřít umístění projektu"
            };
            Image newImage2 = new Image
            {
                Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+@"\imgs\menu_lines.png"))
            };
            newMenuItem2.Click += new RoutedEventHandler(MainWindow.MenuItem2Click);
            newContextMenu.Items.Add(newMenuItem);
            newContextMenu.Items.Add(newMenuItem2);
            newBtn2.ContextMenu = newContextMenu;
            newBtn2.Content = newImage2;
            newStackPanel.Children.Add(newImage);
            newStackPanel.Children.Add(newLabel);
            newBtn.Content = newStackPanel;
            newGrid.Children.Add(newBtn);
            newGrid.Children.Add(newBtn2);
            return newGrid;
        }
    }
}
