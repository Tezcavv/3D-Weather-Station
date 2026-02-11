using System;
using _Scripts.WeatherService;

public class WeatherService
{
  
  public LocationData LocationData { get; set; }
  public WeatherData CurrentWeatherData { get; private set; }
  public event Action<LocationData ,WeatherData > OnChangeWeatherData = null;
  
  private IWeatherProvider _weatherProvider;
  public WeatherService(IWeatherProvider weatherProvider)
  {
    _weatherProvider = weatherProvider;
  }

  public async void RefreshData()
  {
    CurrentWeatherData = await _weatherProvider.GetWeatherData(LocationData);
    OnChangeWeatherData?.Invoke(LocationData, CurrentWeatherData);
  }
  
  
}