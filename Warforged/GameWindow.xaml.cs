using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Warforged
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Label cardZoom = new Label();
        Brush defaultBrush = null;
        Brush BackofCard = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));


        int HandIndex = 0;
        int OHandIndex = 0;
        List<Rectangle> Hand = new List<Rectangle>();
        List<Rectangle> OHand = new List<Rectangle>();
        public GameWindow()
        {
            InitializeComponent();
            defaultBrush = this.PlaySlot.Fill;

            Choice1.Visibility = Visibility.Hidden;
            Choice2.Visibility = Visibility.Hidden;
            Choice3.Visibility = Visibility.Hidden;
            Choice4.Visibility = Visibility.Hidden;
            Choice5.Visibility = Visibility.Hidden;
            Choice6.Visibility = Visibility.Hidden;
            Choice7.Visibility = Visibility.Hidden;


            Hand.Add(Hand1);
            Hand.Add(Hand2);
            Hand.Add(Hand3);
            Hand.Add(Hand4);
            Hand.Add(Hand5);
            Hand.Add(Hand6);
            Hand.Add(Hand7);
            Hand.Add(Hand8);
            Hand.Add(Hand9);
            Hand.Add(Hand10);


            OHand.Add(OHand1);
            OHand.Add(OHand2);
            OHand.Add(OHand3);
            OHand.Add(OHand4);
            OHand.Add(OHand5);
            OHand.Add(OHand6);
            OHand.Add(OHand7);
            OHand.Add(OHand8);
            OHand.Add(OHand9);
            OHand.Add(OHand10);
            
            foreach(Rectangle h in Hand)
            {
                h.Visibility = Visibility.Hidden;
            }
            foreach (Rectangle h in OHand)
            {
                h.Visibility = Visibility.Hidden;
            }

            Edros e = new Edros();

            foreach(Character.Card c in e.hand)
            {
                AddToHand(c);
            }
            AddToStandby(e.standby[0], 0);
            AddToStandby(e.standby[1], 1);
            AddToStandby(e.standby[2], 2);
            AddToStandby(e.standby[3], 3);

            AddToLineup(e.invocation[0], 0);
            AddToLineup(e.invocation[1], 1);
            AddToLineup(e.invocation[2], 2);
            AddToLineup(e.invocation[3], 3);

            CharacterSlot.Fill = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(Character.Card.ImageDir+"Edros"+System.IO.Path.DirectorySeparatorChar+"Edros.png"))
            };
        }

        //Adds the card to the standby position.
        //0 is the rightmost position
        //3 is the leftmost position
        public void AddToStandby(Character.Card c,int position)
        {
            if(position == 0)
            {
                RightStandby.Fill = c.CardImage;
                RightStandby.DataContext = c;
            }
            if(position == 1)
            {
                MiddleRightStandby.Fill = c.CardImage;
                MiddleRightStandby.DataContext = c;
            }
            if (position == 2)
            {
                MiddleLeftStandby.Fill = c.CardImage;
                MiddleLeftStandby.DataContext = c;
            }
            if (position == 3)
            {
                LeftStandby.Fill = c.CardImage;
                LeftStandby.DataContext = c;
            }
        }

        //Adds the card to the lineup position.
        //0 is the toptmost position
        //3 is the bottommost position
        public void AddToLineup(Character.Card c, int position)
        {
            if (position == 0)
            {
                Lineup1.Fill = BackofCard;
                Lineup1.DataContext = c;
            }
            if (position == 1)
            {
                Lineup2.Fill = BackofCard;
                Lineup2.DataContext = c;
            }
            if (position == 2)
            {
                Lineup3.Fill = BackofCard;
                Lineup3.DataContext = c;
            }
            if (position == 3)
            {
                Lineup4.Fill = BackofCard;
                Lineup4.DataContext = c;
            }
        }

        public void AddToHand(Character.Card c)
        {
            if(HandIndex >=10)
            {
                //TODO: Alert the program that the UI cannot support more than 10 cards
                return;
            }

            Hand[HandIndex].Fill = c.CardImage;
            Hand[HandIndex].DataContext = c;
            Hand[HandIndex].Visibility = Visibility.Visible;
            ++HandIndex;
        }

        public void RemoveFromHand(int index)
        {
            if (HandIndex <= 0)
            {
                //TODO: Alert the program that the UI cannot support less than 0 cards
                return;
            }

            for(int i = index; i<HandIndex; ++i)
            {
                Hand[i].Fill = Hand[i + 1].Fill;
                Hand[i].DataContext = Hand[i + 1].DataContext;
                Hand[i].Visibility = Hand[i + 1].Visibility;
            }

            Hand[HandIndex].Fill = defaultBrush;
            Hand[HandIndex].DataContext = null;
            Hand[HandIndex].Visibility = Visibility.Hidden;
            --HandIndex;
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
                cardZoom.Margin = new Thickness(596, 0, 0, 0);
            else
                cardZoom.Margin = new Thickness(0, 0, 0, 0);
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
