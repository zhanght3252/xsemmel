using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Brushes = System.Windows.Media.Brushes;

namespace XSemmel.Schema.Parser {

    public class XsdSimpleType : XsdNode, IXsdHasName, IXsdIsType
    {

        public XsdSimpleType(XmlNode node) : base(node)
        {
            {
                string attr = VisualizerHelper.GetAttr(node, "name");
                if (attr != null)
                {
                    Name = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "final");
                if (attr != null)
                {
                    Final = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "id");
                if (attr != null)
                {
                    Id = attr;
                }
            }
        }

        public string Final { get; set; }
        public string Id { get; set; }


        public override string ToString()
        {
            return Name;
        }


        public override UIElement GetPaintComponent(XsdVisualizer.PaintOptions po, int fontSize)
        {
            if (fontSize <= 0) return null;

            StackPanel pnl = new StackPanel();
            pnl.Children.Add(GetPaintTitle(po, fontSize));
            pnl.Children.Add(GetPaintChildren(po, fontSize - 1));

            addMouseEvents(po, pnl, this);

            return new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1), Child = pnl, Margin = new Thickness(5) };
        }


        protected override UIElement GetPaintTitle(XsdVisualizer.PaintOptions po, int fontSize)
        {
            return new TextBlock
            {
                Text = ToString(),
                Background = new LinearGradientBrush(Colors.MistyRose, Colors.Transparent, 90),
                FontSize = fontSize,
                FontWeight = FontWeights.DemiBold
            };
        }

    }
}