using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace CropApp
{
	 public partial class MainPage : UserControl
	 {
		  public MainPage()
		  {
				InitializeComponent();
		  }

		  private void btnCrop_Click(object sender, RoutedEventArgs e)
		  {
				CroppedImage.Source = cropControl.CropImage();
		  }

		  private void button1_Click(object sender, RoutedEventArgs e)
		  {
				OpenFileDialog imgOpenFileDialog = new OpenFileDialog();

				// Set filter options and filter index.
				imgOpenFileDialog.Filter = "jpg Files (.jpg)|*.jpg|PNG Files (*.png)|*.png";
				imgOpenFileDialog.FilterIndex = 1;

				imgOpenFileDialog.Multiselect = false;

				// Call the ShowDialog method to show the dialog box.
				bool? userClickedOK = imgOpenFileDialog.ShowDialog();

				// Process input if the user clicked OK.
				if (userClickedOK == true)
				{
      
					 BitmapImage bmp = new BitmapImage();
					 FileStream fs = imgOpenFileDialog.File.OpenRead();
					 bmp.SetSource(fs);
					 cropControl.Source = bmp;
				}
		  }
	 }
}
