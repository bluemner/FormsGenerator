using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FormsGeneratorWebApplication.Utilities
{
    
    public static class QRCodeHtmlHelper
    {
        ///
        /// Produces the markup for an image element that displays a QR Code image, as provided by Google's chart API.
        ///
        ///
        ///The data to be encoded, as a string.
        ///The square length of the resulting image, in pixels.
        ///The width of the border that surrounds the image, measured in rows (not pixels).
        ///The amount of error correction to build into the image.  Higher error correction comes at the expense of reduced space for data.
        ///Optional HTML attributes to include on the image element.
        ///http://www.dogu.no/en/blog/technology/how-to-generate-qr-codes-the-easy-way/
        /// 
        public static MvcHtmlString QRCode(this HtmlHelper htmlHelper, string data, int size = 80, int margin = 4, QRCodeErrorCorrectionLevel errorCorrectionLevel = QRCodeErrorCorrectionLevel.Low, object htmlAttributes = null)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (size < 1)
                throw new ArgumentOutOfRangeException("size", size, "Must be greater than zero.");
            if (margin < 0)
                throw new ArgumentOutOfRangeException("margin", margin, "Must be greater than or equal to zero.");
            if (!Enum.IsDefined(typeof(QRCodeErrorCorrectionLevel), errorCorrectionLevel))
                throw new InvalidEnumArgumentException("errorCorrectionLevel", (int)errorCorrectionLevel, typeof(QRCodeErrorCorrectionLevel));

            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chld={2}|{3}&chs={0}x{0}&chl={1}", size, HttpUtility.UrlEncode(data), errorCorrectionLevel.ToString()[0], margin);

            var tag = new TagBuilder("img");
            if (htmlAttributes != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tag.Attributes.Add("src", url);
            tag.Attributes.Add("width", size.ToString());
            tag.Attributes.Add("height", size.ToString());

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }
    }

    public enum QRCodeErrorCorrectionLevel
    {
        /// Recovers from up to 7% erroneous data.
        Low,
        /// Recovers from up to 15% erroneous data.
        Medium,
        /// Recovers from up to 25% erroneous data.
        QuiteGood,
        /// Recovers from up to 30% erroneous data.
        High
    } 
    
}