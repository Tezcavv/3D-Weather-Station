using System;
using _Scripts.WeatherService;
using NaughtyAttributes;
using TMPro;
using UnityEngine;


  public class DashboardDataDisplay : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI windTxt;
    [SerializeField] private TextMeshProUGUI rainTxt;
    [SerializeField] private TextMeshProUGUI tempTxt;
    
    //property as shortcut
    private WeatherService WeatherService =>  ApplicationContext.Instance.WeatherService;


    private void WeatherServiceOnOnChangeWeatherData(LocationData arg1, WeatherData arg2)
    {
      UpdateWeatherData(arg2);
    }

    public void UpdateWeatherData(WeatherData weatherData)
    {
      windTxt.text = $"{weatherData.CurrentWeather.WindSpeed10m} {weatherData.Units.WindSpeed10m}";
      rainTxt.text = $"{weatherData.CurrentWeather.Rain} {weatherData.Units.Rain}";
      tempTxt.text = $"{weatherData.CurrentWeather.Temperature2m} {weatherData.Units.Temperature2m}";
    }
    
    private void Awake()
    {
      if (WeatherService == null) return;

      if (WeatherService.CurrentWeatherData != null)
      {
        UpdateWeatherData(WeatherService.CurrentWeatherData);
      }
      
      WeatherService.OnChangeWeatherData += WeatherServiceOnOnChangeWeatherData;
    }

    private void OnDestroy()
    {
      if (WeatherService != null)
      {
        WeatherService.OnChangeWeatherData -= WeatherServiceOnOnChangeWeatherData;
      }
    }
  }
