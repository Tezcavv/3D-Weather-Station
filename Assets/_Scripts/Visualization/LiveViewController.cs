using System;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class LiveViewController : MonoBehaviour
{
    
  [FormerlySerializedAs("liveViewDisplay")] [SerializeField] LiveViewCityDisplay liveViewCityDisplay;
  [SerializeField] LiveViewWeatherDisplay liveViewWeatherDisplay;

  private WeatherService WeatherService => ApplicationContext.Instance.WeatherService;


  private void WeatherService_OnChangeWeatherData(LocationData arg1, WeatherData arg2)
  {
    liveViewCityDisplay.UpdateAndShowCity(arg1).Forget();
    liveViewWeatherDisplay.UpdateAndShowWeather(arg2).Forget();
  }
  private void Start()
  {
    //todo error handling
    if (WeatherService == null) return;

    if (WeatherService.CurrentWeatherData != null)
    {
      liveViewCityDisplay.UpdateAndShowCity(WeatherService.LocationData).Forget();
      liveViewWeatherDisplay.UpdateAndShowWeather(WeatherService.CurrentWeatherData).Forget();
    }
    
    WeatherService.OnChangeWeatherData += WeatherService_OnChangeWeatherData;
    
  }

  private void OnDestroy()
  {
    WeatherService.OnChangeWeatherData -= WeatherService_OnChangeWeatherData;
  }
}

