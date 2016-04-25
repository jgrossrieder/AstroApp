using AstroApp.Model;
using System.Threading.Tasks;

namespace AstroApp.DataRetriever
{
	public interface IAstroRetriever
	{
		Task<HoroscopeSet> RetrieveHoroscope();
	}
}
