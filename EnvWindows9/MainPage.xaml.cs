using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnvWindows9
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool status = true;
        bool left = true;
        Storyboard ellipseStoryboard = new Storyboard();
        TranslateTransform largeurMove = new TranslateTransform();
        DoubleAnimation doubleAnimationX = new DoubleAnimation();
        public MainPage()
        {
            this.InitializeComponent();
            
            Rectangle redRect = new Rectangle();
            Ellipse ellipse = new Ellipse();
            Duration duration = new Duration(TimeSpan.FromSeconds(2));

            redRect.Width = 300;
            redRect.Height = 200;
            redRect.Fill = new SolidColorBrush(Colors.Red);
            redRect.PointerPressed += rectRed_PointerPressed;

            ellipse.Width = 200;
            ellipse.Height = 200;
            ellipse.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            ellipse.Fill = new SolidColorBrush(Colors.White);
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 2;
            ellipse.PointerPressed += Ellipse_PointerPressed;

            
            GridContainer.Children.Add(redRect);
            GridContainer.Children.Add(ellipse);
            Grid.SetRow(redRect, 1);
            Grid.SetColumn(redRect, 0);
            Grid.SetRow(ellipse, 2);
            Grid.SetColumn(ellipse, 0);

            doubleAnimationX.Duration = duration;
            ellipseStoryboard.Duration = duration;
            ellipseStoryboard.Children.Add(doubleAnimationX);
            Storyboard.SetTarget(doubleAnimationX, largeurMove);
            Storyboard.SetTargetProperty(doubleAnimationX, "X");
            ellipse.RenderTransform = largeurMove;
            GridContainer.Resources.Add("ellipseStoryboard", ellipseStoryboard);
        }

        private void rectBlue_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            blueStoryboard.Begin();
        }

        private void Ellipse_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Ellipse ellip = sender as Ellipse;
            

            if (ellip != null)
            {
                if (left)
                {
                    largeurMove.X = 0;
                    doubleAnimationX.To = GridContainer.RenderSize.Width - ellip.Width;
                    left = false;
                }
                else
                {
                    largeurMove.X = GridContainer.RenderSize.Width - ellip.Width ;
                    doubleAnimationX.To = 0;
                    left = true;
                }
                ellipseStoryboard.Begin();
            }
            
        }

        private void rectRed_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if(null != rect)
            {
                if (rect.Width == 1000)
                {
                    status = false;
                }
                if (rect.Width == 300)
                {
                    status = true;
                }

                if (rect.Width < 1000 && status)
                {
                    rect.Width += 100;
                }
                if(rect.Width > 300 && status == false)
                {
                    rect.Width -= 100;
                }              
            }
        }
    }
}
