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

        List<Rectangle> Link1 = new List<Rectangle>();
        List<Rectangle> Link2 = new List<Rectangle>();
        List<Rectangle> Link3 = new List<Rectangle>();
        List<Rectangle> Link4 = new List<Rectangle>();

        List<Rectangle> OLink1 = new List<Rectangle>();
        List<Rectangle> OLink2 = new List<Rectangle>();
        List<Rectangle> OLink3 = new List<Rectangle>();
        List<Rectangle> OLink4 = new List<Rectangle>();

        List<Rectangle> Suspended = new List<Rectangle>();

        List<Rectangle> OSuspended = new List<Rectangle>();
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

            Link1.Add(Link1_1);
            Link1.Add(Link1_2);
            Link1.Add(Link1_3);
            Link1.Add(Link1_4);
            Link1.Add(Link1_5);
            Link1.Add(Link1_6);
            Link1.Add(Link1_7);
            Link1.Add(Link1_8);

            Link2.Add(Link2_1);
            Link2.Add(Link2_2);
            Link2.Add(Link2_3);
            Link2.Add(Link2_4);
            Link2.Add(Link2_5);
            Link2.Add(Link2_6);
            Link2.Add(Link2_7);
            Link2.Add(Link2_8);


            Link3.Add(Link3_1);
            Link3.Add(Link3_2);
            Link3.Add(Link3_3);
            Link3.Add(Link3_4);
            Link3.Add(Link3_5);
            Link3.Add(Link3_6);
            Link3.Add(Link3_7);
            Link3.Add(Link3_8);

            Link4.Add(Link4_1);
            Link4.Add(Link4_2);
            Link4.Add(Link4_3);
            Link4.Add(Link4_4);
            Link4.Add(Link4_5);
            Link4.Add(Link4_6);
            Link4.Add(Link4_7);
            Link4.Add(Link4_8);

            OLink1.Add(OLink1_1);
            OLink1.Add(OLink1_2);
            OLink1.Add(OLink1_3);
            OLink1.Add(OLink1_4);
            OLink1.Add(OLink1_5);
            OLink1.Add(OLink1_6);
            OLink1.Add(OLink1_7);
            OLink1.Add(OLink1_8);

            OLink2.Add(OLink2_1);
            OLink2.Add(OLink2_2);
            OLink2.Add(OLink2_3);
            OLink2.Add(OLink2_4);
            OLink2.Add(OLink2_5);
            OLink2.Add(OLink2_6);
            OLink2.Add(OLink2_7);
            OLink2.Add(OLink2_8);


            OLink3.Add(OLink3_1);
            OLink3.Add(OLink3_2);
            OLink3.Add(OLink3_3);
            OLink3.Add(OLink3_4);
            OLink3.Add(OLink3_5);
            OLink3.Add(OLink3_6);
            OLink3.Add(OLink3_7);
            OLink3.Add(OLink3_8);

            OLink4.Add(OLink4_1);
            OLink4.Add(OLink4_2);
            OLink4.Add(OLink4_3);
            OLink4.Add(OLink4_4);
            OLink4.Add(OLink4_5);
            OLink4.Add(OLink4_6);
            OLink4.Add(OLink4_7);
            OLink4.Add(OLink4_8);

            Suspended.Add(Suspend1);
            Suspended.Add(Suspend2);
            Suspended.Add(Suspend3);
            Suspended.Add(Suspend4);
            Suspended.Add(Suspend5);
            Suspended.Add(Suspend6);
            Suspended.Add(Suspend7);
            Suspended.Add(Suspend8);
            Suspended.Add(Suspend9);
            Suspended.Add(Suspend10);

            OSuspended.Add(OSuspend1);
            OSuspended.Add(OSuspend2);
            OSuspended.Add(OSuspend3);
            OSuspended.Add(OSuspend4);
            OSuspended.Add(OSuspend5);
            OSuspended.Add(OSuspend6);
            OSuspended.Add(OSuspend7);
            OSuspended.Add(OSuspend8);
            OSuspended.Add(OSuspend9);
            OSuspended.Add(OSuspend10);

            foreach (Rectangle h in Hand)
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
            Lineup1.Fill = e.invocation[0].CardImage;
            Lineup2.Fill = e.invocation[1].CardImage;
            Lineup3.Fill = e.invocation[2].CardImage;
            Lineup4.Fill = e.invocation[3].CardImage;


            OLineup1.Fill = e.invocation[0].CardImage;
            OLineup2.Fill = e.invocation[1].CardImage;
            OLineup3.Fill = e.invocation[2].CardImage;
            OLineup4.Fill = e.invocation[3].CardImage;
            for (int i = 0; i < 10; ++i)
            {
                if(i >=8 )
                {
                    Suspended[i].Fill = CharacterSlot.Fill;
                    OSuspended[i].Fill = CharacterSlot.Fill;
                }
                else if(i >=4)
                {
                    Link1[i].Fill = e.standby[i - 4].CardImage;
                    Link2[i].Fill = e.standby[i - 4].CardImage;
                    Link3[i].Fill = e.standby[i - 4].CardImage;
                    Link4[i].Fill = e.standby[i - 4].CardImage;
                    OLink1[i].Fill = e.standby[i - 4].CardImage;
                    OLink2[i].Fill = e.standby[i - 4].CardImage;
                    OLink3[i].Fill = e.standby[i - 4].CardImage;
                    OLink4[i].Fill = e.standby[i - 4].CardImage;
                    Suspended[i].Fill = e.standby[i - 4].CardImage;
                    OSuspended[i].Fill = e.standby[i - 4].CardImage;
                }
                else
                {
                    Link1[i].Fill = e.hand[i].CardImage;
                    Link2[i].Fill = e.hand[i].CardImage;
                    Link3[i].Fill = e.hand[i].CardImage;
                    Link4[i].Fill = e.hand[i].CardImage;
                    OLink1[i].Fill = e.hand[i].CardImage;
                    OLink2[i].Fill = e.hand[i].CardImage;
                    OLink3[i].Fill = e.hand[i].CardImage;
                    OLink4[i].Fill = e.hand[i].CardImage;
                    Suspended[i].Fill = e.hand[i].CardImage;
                    OSuspended[i].Fill = e.hand[i].CardImage;
                }
            }
            
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
