using System;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class VisualizationViewController : MonoBehaviour
{
    
  [FormerlySerializedAs("liveViewCityDisplay")] [FormerlySerializedAs("liveViewDisplay")] [SerializeField] VisualizationViewCityDisplay visualizationViewCityDisplay;
  [FormerlySerializedAs("liveViewWeatherDisplay")] [SerializeField] VisualizationViewWeatherDisplay visualizationViewWeatherDisplay;

  private WeatherService WeatherService => ApplicationContext.Instance.WeatherService;


  private void WeatherService_OnChangeWeatherData(LocationData arg1, WeatherData arg2)
  {
    visualizationViewCityDisplay.UpdateAndShowCity(arg1).Forget();
    visualizationViewWeatherDisplay.UpdateAndShowWeather(arg2).Forget();
  }
  private void Start()
  {
    //todo error handling
    if (WeatherService == null) return;

    if (WeatherService.CurrentWeatherData != null)
    {
      visualizationViewCityDisplay.UpdateAndShowCity(WeatherService.LocationData).Forget();
      visualizationViewWeatherDisplay.UpdateAndShowWeather(WeatherService.CurrentWeatherData).Forget();
    }
    
    WeatherService.OnChangeWeatherData += WeatherService_OnChangeWeatherData;
    
  }

  private void OnDestroy()
  {
    WeatherService.OnChangeWeatherData -= WeatherService_OnChangeWeatherData;
  }
}

