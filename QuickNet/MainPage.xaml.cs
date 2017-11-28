using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Windows.Input;
using Windows.UI.Input;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QuickNet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Network network;
        public MainPage()
        {
            this.InitializeComponent();
            network = new Network();
            NetworkDebugger.FullSuite();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        // add node button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // adds node
            int index = -1;

            try { index = (int)LayerIndex.SelectedValue; } catch {}
            Int32.TryParse(NodeAmount.Text, out int nodeAmount); // parses node amount

            if (!(index >= network.Layers.Count || index < 0)) // if index is good
            {
                Layer layer = network.Layers[index];
                layer.AddNodes(nodeAmount);
                StackPanel stack = (StackPanel)NetworkLayout.Children[index];
                for (int i = 0; i < nodeAmount; i++)
                {
                    // adds ellipse
                    var ellipse1 = MakeEllipse();
                    stack.Children.Add(ellipse1);
                }

                // fixes size
                foreach (Ellipse ellipse in stack.Children)
                {
                    double diameter = Renderer.GetEllipseSize(layer.Nodes.Count);
                    Animate(ellipse, ellipse.Width, diameter, "Width", 500, 10);
                    Animate(ellipse, ellipse.Height, diameter, "Height", 500, 10);
                }
            }      
        }

        // add layer button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StackPanel newstack = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10),
            };
            network.AddLayer(1); // adds layer to network

            // rendering

            var ellipse1 = MakeEllipse();

            Animate(ellipse1, ellipse1.Width, 100, "Width", 500, 10);
            Animate(ellipse1, ellipse1.Height, 100, "Height", 500, 10);

            ColumnDefinition newcol = new ColumnDefinition // defines column
            {
                Width = new GridLength(40, GridUnitType.Auto),
            };
            NetworkLayout.ColumnDefinitions.Add(newcol); // adds column to columns
            newstack.Children.Add(ellipse1); // adds ellipse to stack
            Grid.SetColumn(newstack, network.Layers.Count - 1);
            Grid.SetRow(newstack, 0);
            NetworkLayout.Children.Add(newstack); // adds stack to column


            LayerIndex.Items.Add(network.Layers.Count - 1);
        }

        private Ellipse MakeEllipse()
        {
            var ellipse1 = new Ellipse // defines ellipse
            {
                Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue),
                Width = 0,
                Height = 0,
                Margin = new Thickness(1)
            };

            ellipse1.PointerEntered += new PointerEventHandler(Target_PointerEntered);

            return ellipse1;
        }

        private void ScrollViewer_ViewChanged_2(object sender, ScrollViewerViewChangedEventArgs e)
        {
        }

        
        public void AnimateEllipse(Ellipse ellipse, double EndSize)
        {
            ellipse.Name = "animate";

            HeightAnimation.From = ellipse.Height;
            WidthAnimation.From = ellipse.Width;
            HeightAnimation.To = EndSize;
            WidthAnimation.To = EndSize;

            Storyboard.Begin();
        }

        public void Animate(DependencyObject target, double from, double to,
                          string propertyPath, int duration, int startTime,
                          SineEase easing = null, Action completed = null)
        {
            if (easing == null)
            {
                easing = new SineEase();
            }

            var db = new DoubleAnimation
            {
                To = to,
                From = from,
                EasingFunction = easing,
                Duration = TimeSpan.FromMilliseconds(duration),
                EnableDependentAnimation = true
        };

            Storyboard.SetTarget(db, target);
            Storyboard.SetTargetProperty(db, propertyPath);

            var sb = new Storyboard
            {
                BeginTime = TimeSpan.FromMilliseconds(startTime)
            };

            if (completed != null)
            {
                sb.Completed += (s, e) => completed();
            }
            sb.Children.Add(db);
            sb.Begin();
        }


        private void Target_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;

            // Multiple, simultaneous mouse button inputs are processed here.
            // Mouse input is associated with a single pointer assigned when 
            // mouse input is first detected. 
            // Clicking additional mouse buttons (left, wheel, or right) during 
            // the interaction creates secondary associations between those buttons 
            // and the pointer through the pointer pressed event. 
            // The pointer released event is fired only when the last mouse button 
            // associated with the interaction (not necessarily the initial button) 
            // is released. 
            // Because of this exclusive association, other mouse button clicks are 
            // routed through the pointer move event.  

            //Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint();
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {

                FrameworkElement element = (FrameworkElement)sender;
                int col = Grid.GetColumn(element);

                Layer layer = network.Layers[col];
                
                if (layer.IsLast == false) // show connections
                {
                    Layer nextLayer = network.Layers[col + 1];
                    int nodesInNext = nextLayer.Nodes.Count;

                    // generates lines for every node in next layer

                    for (int i = 0; i < nodesInNext; i++)
                    {
                        // TODO MAKE LINES
                    }
                }

                // gets stack

                StackPanel stack = (StackPanel)NetworkLayout.Children[col];

                // gets ellipse index

                int ellipseIndex = -1;
                for (int i = 0; i < stack.Children.Count; i++)
                {
                    if (stack.Children[i] == (UIElement)element)
                    {
                        ellipseIndex = i;
                    }
                }

                Node node = network.Layers[col].Nodes[ellipseIndex];

                ToolTip toolTip = new ToolTip();
                toolTip.Content = node.A.ToString();

                ToolTipService.SetToolTip(element, toolTip);

            }

            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }

    }
}
