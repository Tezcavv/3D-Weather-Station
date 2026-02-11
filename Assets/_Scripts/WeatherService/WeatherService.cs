using System;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeatherService
{
  private LocationData _locationData;

  public LocationData LocationData
  {
    get
    {
      {
        return _locationData;
      }
    }
    set
    {
      _locationData = value;
      PlayerPrefs.SetString("LocationData", _locationData.LocationName);
      OnChangeWeatherData?.Invoke(_locationData, CurrentWeatherData);
    }
  }

  public WeatherData CurrentWeatherData { get; private set; }
  public event Action<LocationData, WeatherData> OnChangeWeatherData = null;

  private IWeatherProvider _openMeteoWeatherProvider;
  private IWeatherProvider _randomWeatherProvider;
  
  private IWeatherProvider _currentWeatherProvider;
  
  public WeatherService(WeatherSamplesContainer container)
  {
    _openMeteoWeatherProvider = new OpenMeteoWeatherProvider();
    _randomWeatherProvider = new RandomWeatherProvider(container);
  }

  public void SetOpenMeteoWeatherProvider()
  {
    if (_currentWeatherProvider != null && _currentWeatherProvider == _openMeteoWeatherProvider)
    {
      return;
    }
    _currentWeatherProvider = _openMeteoWeatherProvider;
  }

  public void SetRandomWeatherProvider()
  {
    if (_currentWeatherProvider != null && _currentWeatherProvider == _randomWeatherProvider)
    {
      return;
    }
    _currentWeatherProvider = _randomWeatherProvider;

  }
  
  
  public async UniTask RefreshData()
  {
    CurrentWeatherData = await _currentWeatherProvider.GetWeatherData(LocationData);
    OnChangeWeatherData?.Invoke(LocationData, CurrentWeatherData);
  }
}