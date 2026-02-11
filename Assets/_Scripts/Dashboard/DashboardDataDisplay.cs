using System;
using _Scripts.ScriptableObjects;
using _Scripts.WeatherService;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class DashboardDataDisplay : MonoBehaviour
  {
    [Header("Data")]
    [SerializeField] private TextMeshProUGUI windTxt;
    [FormerlySerializedAs("rainTxt")] [SerializeField] private TextMeshProUGUI precipitationTxt;
    [SerializeField] private TextMeshProUGUI tempTxt;
    [SerializeField] private TextMeshProUGUI weatherTypeTxt;
    
    [Space]
    [SerializeField] private TextMeshProUGUI cityTxt;
    
    
    private WeatherService WeatherService =>  ApplicationContext.Instance.WeatherService;
    private WeatherSettingsContainer  WeatherSettingsContainer =>  ApplicationContext.Instance.WeatherSettingsContainer;

    private void WeatherService_OnChangeWeatherData(LocationData arg1, WeatherData arg2)
    {
      cityTxt.text = arg1.LocationName;
      UpdateWeatherData(arg2);
    }

    public void UpdateWeatherData(WeatherData weatherData)
    {
      windTxt.text = $"{weatherData.CurrentWeather.WindSpeed10m} {weatherData.Units.WindSpeed10m}";
      tempTxt.text = $"{weatherData.CurrentWeather.Temperature2m} {weatherData.Units.Temperature2m}";
      
      var wtype =WeatherSettingsContainer.GetWeatherTypeFromCode(weatherData.CurrentWeather.WeatherCode);
      if (!WeatherSettingsContainer.TryGetWeatherSettings(wtype, out var weatherSettings))
      {
        precipitationTxt.text = "0 mm";
        weatherTypeTxt.text = "Clear";
        return;
      }

      switch (wtype)
      {
        case WeatherType.RAIN:
          precipitationTxt.text = $"{weatherData.CurrentWeather.Rain} {weatherData.Units.Rain}";
          break;
        case WeatherType.SNOW:
          precipitationTxt.text = $"{weatherData.CurrentWeather.Snowfall} {weatherData.Units.Snowfall}";
          break;
        default:
          precipitationTxt.text = $"{weatherData.CurrentWeather.Rain} {weatherData.Units.Rain}";
          break;
      }
      weatherTypeTxt.text = $"{weatherSettings.WeatherTypeName}";
    }
    
    private void Awake()
    {
      if (WeatherService == null) return;

      if (WeatherService.LocationData != null)
      {
        cityTxt.text = WeatherService.LocationData.LocationName;
      }

      if (WeatherService.CurrentWeatherData != null)
      {
        UpdateWeatherData(WeatherService.CurrentWeatherData);
      }
      
      WeatherService.OnChangeWeatherData += WeatherService_OnChangeWeatherData;
    }

    private void OnDestroy()
    {
      if (WeatherService != null)
      {
        WeatherService.OnChangeWeatherData -= WeatherService_OnChangeWeatherData;
      }
    }
  }
