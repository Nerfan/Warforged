using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
        private string sideText = "Health : {0}\n" +
"Empower: {1}\n" +
"Reinforce: {2}\n" +
"Seal: {3}\n" +
"Last: {4}\n" +
"Negated: {5}\n" +
"Healed: {6}\n" +
"Damage: {7}\n";
        private Label cardZoom = new Label();
        Brush defaultBrush = null;
        Brush BackofCard = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
        public string ImageDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + System.IO.Path.DirectorySeparatorChar + "CardImages" + System.IO.Path.DirectorySeparatorChar;

        bool allowClick = false;

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
        public Dictionary<string, Brush> CardImages = new Dictionary<string, Brush>();
        public Dictionary<string, Brush> OCardImages = new Dictionary<string, Brush>();

        List<Button> Choices = new List<Button>();

        public GameWindowLibrary library = null;
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

            Choices.Add(Choice1);
            Choices.Add(Choice2);
            Choices.Add(Choice3);
            Choices.Add(Choice4);
            Choices.Add(Choice5);
            Choices.Add(Choice6);
            Choices.Add(Choice7);

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
            foreach (Rectangle h in Suspended)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in OSuspended)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in Link1)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in Link2)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in Link3)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in Link4)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in OLink1)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in OLink2)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in OLink3)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            foreach (Rectangle h in OLink4)
            {
                h.Visibility = Visibility.Hidden;
                h.Fill = defaultBrush;
            }
            
            foreach (Rectangle r in grid.Children.OfType<Rectangle>())
            {
                r.MouseLeftButtonUp += cardClicked;
                r.Fill = defaultBrush;
            }
            /*Edros e = new Edros();
            e.setOpponent(new Edros());
            e.opponent.setOpponent(e);
            CharacterSlot.Fill = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir+"Edros"+System.IO.Path.DirectorySeparatorChar+"Edros.png"))
            };
            e.playCard(e.hand[1]);
            e.opponent.playCard(e.opponent.hand[0]);
            e.opponent.declarePhase();
            e.declarePhase();
            e.opponent.damagePhase();
            e.damagePhase();
            e.playCard(e.hand[2]);
            e.opponent.playCard(e.opponent.hand[0]);
            e.opponent.declarePhase();
            e.declarePhase();
            e.opponent.damagePhase();
            e.damagePhase();
            UpdateUI(e, true);
            UpdateOpponentUI(e.opponent, true, true);
            multiPrompt("Test Prompt Text",new List<string>() { "1s", "2s" , "3s" , "4s" , "5s" , "6s" , "7s" },new List<object>() { "1s", "2s", "3s", "4s", "5s", "6s", "7s" });*/
        }
        
        public void highlight(Character.Card card,byte r,byte g,byte b)
        {
            if(PlaySlot.DataContext != null && PlaySlot.DataContext == card)
            {
                PlaySlot.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                PlaySlot.StrokeThickness = 3.0;
                return;
            }
            if (OPlaySlot.DataContext != null && OPlaySlot.DataContext == card)
            {
                OPlaySlot.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                OPlaySlot.StrokeThickness = 3.0;
                return;
            }
            foreach (Rectangle rect in Hand)
            {
                if(rect.DataContext == null)
                {
                    continue;
                }
                if(rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r,g,b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle> (){LeftStandby,MiddleLeftStandby,MiddleRightStandby,RightStandby })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { Lineup1, Lineup2, Lineup3, Lineup4 })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }

            foreach (Rectangle rect in Suspended)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }

            foreach (Rectangle rect in Link1)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link2)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link3)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link4)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }

            //Opponent Cards

            foreach (Rectangle rect in OHand)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { OLeftStandby, OMiddleLeftStandby, OMiddleRightStandby, ORightStandby })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { OLineup1, OLineup2, OLineup3, OLineup4 })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }

            foreach (Rectangle rect in OSuspended)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }

            foreach (Rectangle rect in OLink1)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink2)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink3)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink4)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(r, g, b));
                    rect.StrokeThickness = 3.0;
                    return;
                }
            }
        }


        public void clearHighlight(Character.Card card)
        {
            if (PlaySlot.DataContext != null && PlaySlot.DataContext == card)
            {
                PlaySlot.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                PlaySlot.StrokeThickness = 1.0;
                return;
            }
            if (OPlaySlot.DataContext != null && OPlaySlot.DataContext == card)
            {
                OPlaySlot.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                OPlaySlot.StrokeThickness = 1.0;
                return;
            }
            foreach (Rectangle rect in Hand)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { LeftStandby, MiddleLeftStandby, MiddleRightStandby, RightStandby })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { Lineup1, Lineup2, Lineup3, Lineup4 })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }

            foreach (Rectangle rect in Suspended)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }

            foreach (Rectangle rect in Link1)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link2)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link3)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in Link4)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }

            //Opponent Cards

            foreach (Rectangle rect in OHand)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { OLeftStandby, OMiddleLeftStandby, OMiddleRightStandby, ORightStandby })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in new List<Rectangle>() { OLineup1, OLineup2, OLineup3, OLineup4 })
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }

            foreach (Rectangle rect in OSuspended)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }

            foreach (Rectangle rect in OLink1)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink2)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink3)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
            foreach (Rectangle rect in OLink4)
            {
                if (rect.DataContext == null)
                {
                    continue;
                }
                if (rect.DataContext == card)
                {
                    rect.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    rect.StrokeThickness = 1.0;
                    return;
                }
            }
        }

        public void waitForClick()
        {
            allowClick = true;
        }

        public void yesnoPrompt(string text)
        {
            PromptText.Content = text;
            Choice1.Content = "Yes";
            Choice2.Content = "No";
            Choice1.DataContext = true;
            Choice2.DataContext = false;
            Choice1.Visibility = Visibility.Visible;
            Choice2.Visibility = Visibility.Visible;
        }

        public void setPromptText(string text)
        {
            PromptText.Content = text;
        }

        public void multiPrompt(string text,List<string> buttonTexts,List<object> returnTypes)
        {
            int count = buttonTexts.Count < returnTypes.Count ? buttonTexts.Count:returnTypes.Count;
            count = Math.Min(count, 7);
            for(int i = 0; i<count; ++i)
            {
                Choices[i].Content = buttonTexts[i];
                Choices[i].DataContext = returnTypes[i];
                Choices[i].Visibility = Visibility.Visible;
            }
            PromptText.Content = text;
        }
        public void UpdateOpponentUI(Character ch, bool showCurrCard, bool showHand)
        {
            grid.Children.Remove(cardZoom);
            for (int i = 0; i < 10; i += 1)
            {
                if (ch.hand.Count <= i)
                {
                    OHand[i].Fill = defaultBrush;
                    OHand[i].DataContext = null;
                    OHand[i].Visibility = Visibility.Hidden;
                }
                else if (showHand)
                {
                    OHand[i].Fill = OCardImages[ch.hand[i].name];
                    OHand[i].DataContext = ch.hand[i];
                    OHand[i].Visibility = Visibility.Visible;
                }
                else
                {
                    OHand[i].Fill = BackofCard;
                    OHand[i].DataContext = ch.hand[i];
                    OHand[i].Visibility = Visibility.Visible;
                }
            }
            List<Rectangle> standbyList = new List<Rectangle>() { OLeftStandby, OMiddleLeftStandby, OMiddleRightStandby, ORightStandby };
            for (int i = 0; i < 4; ++i)
            {
                if (ch.standby.Count <= i)
                {
                    standbyList[i].Fill = defaultBrush;
                    standbyList[i].DataContext = null;
                }
                else
                {
                    standbyList[i].Fill = OCardImages[ch.standby[i].name];
                    standbyList[i].DataContext = ch.standby[i];
                }
            }
            List<Rectangle> invocationList = new List<Rectangle>() { OLineup1, OLineup2, OLineup3, OLineup4 };
            for (int i = 0; i < 4; ++i)
            {
                if (ch.invocation.Count <= i)
                {
                    invocationList[i].Fill = defaultBrush;
                    invocationList[i].DataContext = null;
                }
                else if (ch.invocation[i].active)
                {
                    invocationList[i].Fill = OCardImages[ch.invocation[i].name];
                    invocationList[i].DataContext = ch.invocation[i];
                }
                else
                {
                    invocationList[i].Fill = BackofCard;
                    invocationList[i].DataContext = ch.invocation[i];
                }
            }
            if (ch.currCard == null)
            {
                OPlaySlot.Fill = defaultBrush;
                OPlaySlot.DataContext = null;
            }
            else if (showCurrCard)
            {
                OPlaySlot.Fill = OCardImages[ch.currCard.name];
                OPlaySlot.DataContext = ch.currCard;
            }
            else
            {
                OPlaySlot.Fill = BackofCard;
                OPlaySlot.DataContext = ch.currCard;
            }
            string prevCardName = "None";
            if (ch.prevCard != null)
            {
                prevCardName = ch.prevCard.name;
            }
            string fmt = string.Format(sideText, ch.hp, ch.empower, ch.reinforce, "None", prevCardName, false, false, false);
            OSidePanel.Content = fmt;

        }

        public void clearAllHighlighting()
        {
            foreach (Rectangle r in grid.Children.OfType<Rectangle>())
            {
                r.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                r.StrokeThickness = 1.0;
            }
        }

        public void UpdateUI(Character ch, bool showCurrCard)
        {
            grid.Children.Remove(cardZoom);
            for (int i =0; i<10; i+=1)
            {
                if(ch.hand.Count <=i)
                {
                    Hand[i].Fill = defaultBrush;
                    Hand[i].DataContext = null;
                    Hand[i].Visibility = Visibility.Hidden;
                }
                else
                {
                    Hand[i].Fill = CardImages[ch.hand[i].name];
                    Hand[i].DataContext = ch.hand[i];
                    Hand[i].Visibility = Visibility.Visible;
                }
            }
            List<Rectangle> standbyList = new List<Rectangle>() { LeftStandby,MiddleLeftStandby,MiddleRightStandby,RightStandby};
            for(int i =0;i<4;++i)
            {
                if(ch.standby.Count <= i)
                {
                    standbyList[i].Fill = defaultBrush;
                    standbyList[i].DataContext = null;
                }
                else
                {
                    standbyList[i].Fill = CardImages[ch.standby[i].name];
                    standbyList[i].DataContext = ch.standby[i];
                }
            }
            List<Rectangle> invocationList = new List<Rectangle>() {Lineup1,Lineup2,Lineup3,Lineup4 };
            for (int i = 0; i < 4; ++i)
            {
                if (ch.invocation.Count <= i)
                {
                    invocationList[i].Fill = defaultBrush;
                    invocationList[i].DataContext = null;
                }
                else if(ch.invocation[i].active)
                {
                    invocationList[i].Fill = CardImages[ch.invocation[i].name];
                    invocationList[i].DataContext = ch.invocation[i];
                }
                else
                {
                    invocationList[i].Fill = BackofCard;
                    invocationList[i].DataContext = ch.invocation[i];
                }
            }
            if (ch.currCard == null)
            {
                PlaySlot.Fill = defaultBrush;
                PlaySlot.DataContext = null;
            }
            else if(showCurrCard)
            {
                PlaySlot.Fill = CardImages[ch.currCard.name];
                PlaySlot.DataContext = ch.currCard;
            }
            else
            {
                PlaySlot.Fill = BackofCard;
                PlaySlot.DataContext = ch.currCard;
            }
            string prevCardName = "None";
            if(ch.prevCard != null)
            {
                prevCardName = ch.prevCard.name;
            }
            string fmt = string.Format(sideText,ch.hp, ch.empower, ch.reinforce, "None", prevCardName, false, false, false);
            SidePanel.Content = fmt;

        }


        public void setupEdros(Dictionary<string,Brush> CardImages)
        {
            
            CardImages.Add("Celestial Surge", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Celestial Surge.png"))
            });
            CardImages.Add("Purging Lightning", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Purging Lightning.png"))
            });
            CardImages.Add("Crashing Sky", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Crashing Sky.png"))
            });
            CardImages.Add("Edros", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Edros.png"))
            });

            CardImages.Add("Faith Unquestioned", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Faith Unquestioned.png"))
            });
            CardImages.Add("Grace of Heaven", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Grace of Heaven.png"))
            });
            CardImages.Add("Hand of Toren", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Hand of Toren.png"))
            });

            CardImages.Add("Pillar of Lightning", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Pillar of Lightning.png"))
            });
            CardImages.Add("Rolling Thunder", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Rolling Thunder.png"))
            });
            CardImages.Add("Scorn of Thunder", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Scorn of Thunder.png"))
            });

            CardImages.Add("Sky Blessed Shield", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Sky Blessed Shield.png"))
            });
            CardImages.Add("Toren's Favored", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Toren's Favored.png"))
            });
            CardImages.Add("Wrath of Lightning", new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@ImageDir + "Edros" + System.IO.Path.DirectorySeparatorChar + "Wrath of Lightning.png"))
            });
        }

        //Adds the card to the standby position.
        //0 is the rightmost position
        //3 is the leftmost position
        public void AddToStandby(Character.Card c,int position)
        {
            if(position == 0)
            {
                RightStandby.Fill = CardImages[c.name];
                RightStandby.DataContext = c;
            }
            if(position == 1)
            {
                MiddleRightStandby.Fill = CardImages[c.name];
                MiddleRightStandby.DataContext = c;
            }
            if (position == 2)
            {
                MiddleLeftStandby.Fill = CardImages[c.name];
                MiddleLeftStandby.DataContext = c;
            }
            if (position == 3)
            {
                LeftStandby.Fill = CardImages[c.name];
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

            Hand[HandIndex].Fill = CardImages[c.name];
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
            if (rect.Fill == defaultBrush || ! (rect.Fill is ImageBrush))
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


        private void ChoiceClicked(object sender, RoutedEventArgs e)
        {
            
            Button choice = (Button)sender;
            foreach (Button b in Choices)
            {
                b.Visibility = Visibility.Hidden;
            }
            allowClick = false;
            library.setReturnObject(choice.DataContext);
        }

        public void cardClicked(object sender, RoutedEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            if(r.Fill != defaultBrush && allowClick)
            {
                allowClick = false;
                foreach (Button b in Choices)
                {
                    b.Visibility = Visibility.Hidden;
                }
                library.setReturnObject(r.DataContext);
            }
        }
    }
}
