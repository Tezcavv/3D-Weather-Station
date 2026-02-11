using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class WeatherSamplesContainer : ScriptableObject
{
  //dictionary would be way better but no Odin
  public List<WeatherSample> ClearWeatherSamples;
  public List<WeatherSample> RainyWeatherSamples;
  public List<WeatherSample> SnowyWeatherSamples;

  //todo ignore duplicates
  public WeatherSample GetRandomSample()
  {
    int allTypes = Enum.GetValues(typeof(WeatherType)).Length;
    var randomWeather = (WeatherType)Random.Range(0, allTypes);
    switch (randomWeather)
    {
      case WeatherType.CLEAR:
        return ClearWeatherSamples[Random.Range(0, ClearWeatherSamples.Count)];

      case WeatherType.RAIN:
        return RainyWeatherSamples[Random.Range(0, RainyWeatherSamples.Count)];

      case WeatherType.SNOW:
        return SnowyWeatherSamples[Random.Range(0, SnowyWeatherSamples.Count)];

      default:
        return GetRandomSample();
    }
  }
}