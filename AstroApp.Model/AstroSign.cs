using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace AstroApp.Model
{
    public class AstroSign :IComparable<AstroSign>
    {
	    public String Name { get; set; }
        public int Order { get; set; }
		
	    public Uri IconUri { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int CompareTo(AstroSign other)
        {
            return other == null ? 1 : Order.CompareTo(other.Order);
        }
    }
}
