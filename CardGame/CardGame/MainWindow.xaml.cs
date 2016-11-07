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

namespace CardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Label cardZoom = new Label();
        Brush defaultBrush = null;
        public GameWindow()
        {
            InitializeComponent();
            defaultBrush = this.PlaySlot.Fill;
            foreach(UIElement el in grid.Children)
            {
                if(el is Rectangle)
                {
                    var rect = (Rectangle)el;
                    rect.Fill = defaultBrush;
                    rect.MouseEnter += MouseEnter;
                    rect.MouseLeave += MouseLeave;
                    if (rect.Tag != null && rect.Tag.Equals("Opponent"))
                    {
                        var img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"file:///C:/Users/Ben/Documents/visual studio 2013/Projects/CardGame/CardGame/A Soldier's Remorse.png");
                        img.Rotation = Rotation.Rotate180;
                        img.EndInit();
                        rect.Fill = new ImageBrush
                        {
                            ImageSource = img
                        };
                    }
                    else
                    {
                        rect.Fill = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/Ben/Documents/visual studio 2013/Projects/CardGame/CardGame/A Soldier's Remorse.png", false))
                        };
                    }
                }
            }
            this.PlaySlot.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/Ben/Documents/visual studio 2013/Projects/CardGame/CardGame/A Soldier's Remorse.png",false))
            };
            this.CharacterSlot.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"file:///C:/Users/Ben/Documents/visual studio 2013/Projects/CardGame/CardGame/A Soldier's Remorse.png", false))
            };
        }


        private void MouseEnter(object sender, MouseEventArgs e)
        {
            
            var rect = (Rectangle)sender;
            if (rect.Fill == defaultBrush && ! (rect.Fill is ImageBrush))
                return;
            var imgB = (ImageBrush)rect.Fill;
            if(!(imgB.ImageSource is BitmapImage))
                return;
            var imgSrc = (BitmapImage)imgB.ImageSource;
            var newSrc = new BitmapImage();
            newSrc.BeginInit();
            newSrc.Rotation = Rotation.Rotate0;
            newSrc.UriSource = new Uri(imgSrc.UriSource.AbsoluteUri);
            newSrc.EndInit();

            cardZoom.Background = new ImageBrush
            {
                ImageSource = newSrc
            };
            var point = e.GetPosition(grid);
            if(rect.Margin.Left <596)
                cardZoom.Margin = new Thickness(328, 0, 0, 0);
            else
                cardZoom.Margin = new Thickness(-328, 0, 0, 0);
            cardZoom.Width = 205;
            cardZoom.Height = 310;
            grid.Children.Add(cardZoom);
            //Grid.SetRow(l, (int)point.X);
            //Grid.SetColumn(l, (int)point.Y);
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            var rect = (Rectangle)sender;
            if (rect.Fill == defaultBrush)
                return;
            grid.Children.Remove(cardZoom);
        }
    }
}
