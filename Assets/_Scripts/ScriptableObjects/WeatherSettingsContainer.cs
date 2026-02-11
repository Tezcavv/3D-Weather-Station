using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
  [CreateAssetMenu]
  public class WeatherSettingsContainer : ScriptableObject
  {
    [field: SerializeField] public List<WeatherSettings> AllWeatherCodes { get; private set; }

    public WeatherType GetWeatherTypeFromCode(int code)
    {
      foreach (var weatherCodeData in AllWeatherCodes)
      {
        if (weatherCodeData.ContainsCode(code))
          return weatherCodeData.WeatherType;
      }
      return WeatherType.CLEAR;
    }

    public bool TryGetWeatherSettings(WeatherType weatherType, out WeatherSettings weatherSettings)
    {
      weatherSettings = AllWeatherCodes.FirstOrDefault(weatherCode => weatherCode.WeatherType == weatherType);
      return weatherSettings != null;
    }
  }
}