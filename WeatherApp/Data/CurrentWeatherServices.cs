using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Data
{
	public class CurrentWeatherServices
	{
		private readonly HttpClient _httpClient = new HttpClient();
		UriBuilder UriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5");

		public async Task<WeatherData> GetCurrentWeather(string place)
		{
			WeatherData data = new WeatherData();
			UriBuilder CurrentWeatherUri = new UriBuilder(UriBuilder.ToString());
			CurrentWeatherUri.Path = CurrentWeatherUri.Path + "/weather";
			CurrentWeatherUri.Query = $"q={place}" + "&" + "APPID=21b17ce1518a5be6b1097495053cb5eb" + "&" + "units=metric";
			//Console.WriteLine(CurrentWeatherUri.ToString());
			HttpResponseMessage response = await _httpClient.GetAsync(CurrentWeatherUri.ToString());
			if (response.IsSuccessStatusCode)
			{
				try
				{
                    var result = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<WeatherData>(result);
                }
                catch (Exception ex)
				{
					Console.WriteLine(ex.Message.ToString());
				}

				return data;
            }
			else
			{
				return null;
			}
        }

		public async Task<WeatherForecast.Forecast> GetWeatherForecastAsync(string city)
		{
			WeatherForecast.Forecast forecast = new WeatherForecast.Forecast();
			UriBuilder ForecastUri = new UriBuilder(UriBuilder.ToString());
			ForecastUri.Path = ForecastUri.Path + "/forecast";
			ForecastUri.Query = $"q={city}" + "&" + "APPID=21b17ce1518a5be6b1097495053cb5eb" + "&" + "units=metric";
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(ForecastUri.ToString());
			if(responseMessage.IsSuccessStatusCode)
			{
				var result = responseMessage.Content.ReadAsStringAsync().Result;
				forecast = JsonConvert.DeserializeObject<WeatherForecast.Forecast>(result);
				return forecast;
			}
			else
			{
				return null;
			}
		}
	}
}