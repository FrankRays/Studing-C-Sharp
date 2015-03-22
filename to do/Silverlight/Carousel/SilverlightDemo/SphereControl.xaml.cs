using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightDemo
{
	public partial class SphereControl : UserControl
	{
		public SphereControl()
		{
			InitializeComponent();
		}

        public Color InnerColorBrush
        {
            get { return InnerColor.Color; }
            set { InnerColor.Color = value; }
        }
        public Color OuterColorBursh
        {
            get { return OuterColor.Color; }
            set { OuterColor.Color = value; }
        }
        public Brush SphereFill
        {
            get { return Ellipse.Fill; }
        }

        private void evt_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        private void evt_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }


    }
}