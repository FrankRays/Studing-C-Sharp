using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace SolSilverlightImageGallery
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// Required to initialize variables
			InitializeComponent();

			try
			{
				BindImages(); // Call Bind Image Function
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message); 
			}
		}

		/// <summary>
		/// Bind Images in List Box
		/// </summary>
		private void BindImages()
		{
			try
			{
				// Store Data in List Box.
				List<ImageEntity> ListImagesObj = ImagesView.GetAllImagesData();

				// Check the List Object Count
				if (ListImagesObj.Count > 0)
				{
					// Bind data in List Box
					LsImageGallery.DataContext = ListImagesObj;    
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}