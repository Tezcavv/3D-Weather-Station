using System;
using System.Collections.Generic;
using System.Threading;
using _Scripts.ScriptableObjects;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Scripts.WeatherService
{
  public interface IWeatherProvider
  {
    UniTask<WeatherData> GetWeatherData( LocationData locationData, CancellationToken ct = default);
  }
  
  public enum ProviderType
  {
    OpenMeteo,
    Random
  }

  public class RandomWeatherProvider : IWeatherProvider
  {
    private WeatherSamplesContainer _wsc;
    public RandomWeatherProvider(WeatherSamplesContainer container)
    {
      _wsc = container;
    }
    public UniTask<WeatherData> GetWeatherData(LocationData locationData, CancellationToken ct = default)
    {
      return UniTask.FromResult(_wsc.GetRandomSample().WeatherData);
    }
  }
  
  // public class SelectedWeatherTypeWeatherProvider : IWeatherProvider
  // {
  //  
  // }
  
  public class OpenMeteoWeatherProvider : IWeatherProvider
  {
   
    public async UniTask<WeatherData> GetWeatherData(LocationData locationData,
      CancellationToken ct = default)
    {
      string url =
        "https://api.open-meteo.com/v1/forecast" +
        $"?latitude={locationData.Latitude}" +
        $"&longitude={locationData.Longitude}" +
        "&current=temperature_2m,rain,showers,snowfall,wind_speed_10m,weather_code" +
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
    [field:SerializeField][JsonProperty("current_units")] public Units Units { get; set; }
    [field:SerializeField][JsonProperty("current")] public CurrentWeather CurrentWeather { get; set; }
  }

  [Serializable]
  public class Units
  {
    [field: SerializeField]
    [JsonProperty("temperature_2m")]
    public string Temperature2m { get; set; } = "°C";

    [field: SerializeField]
    [JsonProperty("rain")]
    public string Rain { get; set; } = "mm";

    [field: SerializeField]
    [JsonProperty("weather_code")]
    public string WeatherCode { get; set; } = "wmo";

    [field: SerializeField]
    [JsonProperty("snowfall")]
    public string Snowfall { get; set; } = "cm";

    [field: SerializeField]
    [JsonProperty("wind_speed_10m")]
    public string WindSpeed10m { get; set; } = "km/h";
  }

  [Serializable]
  public class CurrentWeather
  {
    [field:SerializeField][JsonProperty("temperature_2m")] public double Temperature2m { get; set; }
    [field:SerializeField][JsonProperty("rain")] public double Rain { get; set; }
    [field:SerializeField][JsonProperty("weather_code")]  public int WeatherCode { get; set; }
    [field:SerializeField][JsonProperty("snowfall")] public double Snowfall { get; set; }
    [field:SerializeField][JsonProperty("wind_speed_10m")] public double WindSpeed10m { get; set; }
  }
}