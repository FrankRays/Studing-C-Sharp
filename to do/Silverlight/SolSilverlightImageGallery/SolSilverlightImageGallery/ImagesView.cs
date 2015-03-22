using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace SolSilverlightImageGallery
{
    public static class ImagesView
    {
        public static List<ImageEntity> GetAllImagesData()
        {
            try
            {
                // Load Xml Document
                XDocument XDoc = XDocument.Load("ImageData.xml");

                // Query for retriving all Images data from XML
                var Query = from Q in XDoc.Descendants("Image")
                            select new ImageEntity
                            {
                                ImageName = Q.Element("ImageName").Value,
                                ImagePath = Q.Element("ImagePath").Value
                            };

                // return images data
                return Query.ToList<ImageEntity>();
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
