using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace _Scripts.WeatherService
{
  public interface IWeatherProvider
  {
    UniTask<WeatherData> GetWeatherData( LocationData locationData, CancellationToken ct = default);
  }

  public class MockWeatherProvider : IWeatherProvider
  {
    public LocationData LocationData { get; set; }

    public async UniTask<WeatherData> GetWeatherData(LocationData locationData,
      CancellationToken ct = default)
    {
      string url =
        "https://api.open-meteo.com/v1/forecast" +
        $"?latitude={locationData.Latitude}" +
        $"&longitude={locationData.Longitude}" +
        "&current=temperature_2m,rain,precipitation,showers,snowfall,wind_speed_10m,weather_code" +
        "&timezone=auto";

      using var req = UnityWebRequest.Get(url);
      await req.SendWebRequest().ToUniTask(cancellationToken: ct);

      if (req.result != UnityWebRequest.Result.Success)
      {
        throw new Exception($"OpenMeteo request failed: {req.result} : {req.error}");
      }

      var json = req.downloadHandler.text;
      var data = JsonConvert.DeserializeObject<WeatherData>(json);

      if (data == null)
        throw new Exception("OpenMeteo: failed to deserialize response.");

      return data;
    }
  }



  [Serializable]
  public class WeatherData
  {
    [JsonProperty("current_units")] public Units Units { get; set; }
    [JsonProperty("current")] public CurrentWeather CurrentWeather { get; set; }
  }

  [Serializable]
  public class Units
  {
    [JsonProperty("time")] public string Time { get; set; }

    [JsonProperty("interval")] public string Interval { get; set; }

    [JsonProperty("temperature_2m")] public string Temperature2m { get; set; }

    [JsonProperty("rain")] public string Rain { get; set; }

    [JsonProperty("weather_code")]  public string WeatherCode { get; set; }

    [JsonProperty("showers")] public string Showers { get; set; }

    [JsonProperty("snowfall")] public string Snowfall { get; set; }

    [JsonProperty("wind_speed_10m")] public string WindSpeed10m { get; set; }
  }

  [Serializable]
  public class CurrentWeather
  {
    //todo add Time based on zone if not already
    [JsonProperty("time")] public string Time { get; set; }

    //todo meaning of Interval
    [JsonProperty("interval")] public int Interval { get; set; }
    [JsonProperty("temperature_2m")] public double Temperature2m { get; set; }

    [JsonProperty("rain")] public double Rain { get; set; }

    [JsonProperty("weather_code")]  public int WeatherCode { get; set; }

    [JsonProperty("showers")] public double Showers { get; set; }

    [JsonProperty("snowfall")] public double Snowfall { get; set; }
    [JsonProperty("wind_speed_10m")] public double WindSpeed10m { get; set; }
  }
}