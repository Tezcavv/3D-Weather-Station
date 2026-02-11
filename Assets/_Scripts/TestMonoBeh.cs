using System;
using System.Threading;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class TestMonoBeh : MonoBehaviour
{
  private CancellationTokenSource _cts;
  public LocationData locationData;
  private void OnEnable() => _cts = new CancellationTokenSource();
  private void OnDisable() => _cts?.Cancel();

  private void Start()
  {
    
  }

  [Button]
  private async UniTask Run()
  {
    var data = await new MockWeatherProvider().GetWeatherData(locationData);
    Debug.Log($"Temp: {data.CurrentWeather.Temperature2m}°C, Wind: {data.CurrentWeather.WindSpeed10m} km/h");
   
  }

  
}
