using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AstroApp.Model;

namespace AstroApp.DataRetriever.MHDJ
{
	public class AstroRetriever : IAstroRetriever
	{
		private HoroscopeSet _cachedHoroscope;
		private Dictionary<AstroSign, String> _urlToParse = new Dictionary<AstroSign, string>()
		{
			{ AstroManager.Instance.AriesSign,"belier.htm"},
			{ AstroManager.Instance.TaurusSign,"taureau.htm"},
			{ AstroManager.Instance.GeminiSign,"gemeaux.htm"},
			{ AstroManager.Instance.CancerSign,"cancer.htm"},
			{ AstroManager.Instance.LeoSign,"lion.htm"},
			{ AstroManager.Instance.VirgoSign,"vierge.htm"},
			{ AstroManager.Instance.LibraSign,"balance.htm"},
			{ AstroManager.Instance.ScorpioSign,"scorpion.htm"},
			{ AstroManager.Instance.SagittariusSign,"sagittaire.htm"},
			{ AstroManager.Instance.CapricornSign,"capricorne.htm"},
			{ AstroManager.Instance.AquariusSign,"verseau.htm"},
			{ AstroManager.Instance.PiscesSign,"poissons.htm"},
			//{ AstroManager.Instance.AriesSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/belier.htm"},
			//{ AstroManager.Instance.TaurusSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/taureau.htm"},
			//{ AstroManager.Instance.GeminiSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/gemeaux.htm"},
			//{ AstroManager.Instance.CancerSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/cancer.htm"},
			//{ AstroManager.Instance.LeoSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/lion.htm"},
			//{ AstroManager.Instance.VirgoSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/vierge.htm"},
			//{ AstroManager.Instance.LibraSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/balance.htm"},
			//{ AstroManager.Instance.ScorpioSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/scorpion.htm"},
			//{ AstroManager.Instance.SagittariusSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/sagittaire.htm"},
			//{ AstroManager.Instance.CapricornSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/capricorne.htm"},
			//{ AstroManager.Instance.AquariusSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/verseau.htm"},
			//{ AstroManager.Instance.PiscesSign,"http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/poissons.htm"},
		};
		private static readonly Regex _mainRegex = new Regex("<h2 class=\"h2_h lk_1 sp_left\">Votre climat astral<\\/h2>\\s+<p class=\"sp_left sp_right\">([^<]+)<\\/p>");
		private static readonly Regex _topicRegex = new Regex("<div class=\"[^\"]+\"><div style=\"[^\"]+\">((<div class=\"[^\"]+\"></div>)+)</div></div>\\s+<h2 class=\"[^\"]+\">([^<]+)</h2>\\s+<p class=\"[^\"]+\">([^<]+)</p>");


		public async Task<HoroscopeSet> RetrieveHoroscope()
		{
			if (_cachedHoroscope != null)
			{
				return _cachedHoroscope;
			}
			_cachedHoroscope = new HoroscopeSet { Time = DateTime.Today };
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://www.mon-horoscope-du-jour.com/horoscopes/quotidien/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
				client.DefaultRequestHeaders.UserAgent.Clear();
				client.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");
				client.Timeout = TimeSpan.FromSeconds(30);

				foreach (KeyValuePair<AstroSign, string> pageToParse in _urlToParse)
				{
					Horoscope horoscope = await ParsePage(client, pageToParse.Key, pageToParse.Value);
					_cachedHoroscope.Horoscopes.Add(horoscope);
				}
			}
			return _cachedHoroscope;
		}

		private async Task<Horoscope> ParsePage(HttpClient client, AstroSign astroSign, string url)
		{

				HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
				Horoscope horoscope = new Horoscope
				{
					Sign = astroSign
				};
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					string content = await httpResponseMessage.Content.ReadAsStringAsync();
					Match match = _mainRegex.Match(content);

					if (match.Success && match.Groups[1].Success)
					{
						horoscope.GlobalText = match.Groups[1].Value;
					}
					MatchCollection matchCollection = _topicRegex.Matches(content);
					foreach (Match topicMatch in matchCollection)
					{
						int starCount = CountStringOccurrences(topicMatch.Groups[1].Value, "hqson");
						horoscope.Topics.Add(new HoroscopeTopic()
						{
							Title = topicMatch.Groups[3].Value,
							Description = topicMatch.Groups[4].Value,
							Stars = starCount,
							TotalStars = 5
						});
					}

				}


				return horoscope;
		}
		private static int CountStringOccurrences(string text, string pattern)
		{
			// Loop through all instances of the string 'text'.
			int count = 0;
			int i = 0;
			while ((i = text.IndexOf(pattern, i)) != -1)
			{
				i += pattern.Length;
				count++;
			}
			return count;
		}
	}
}
