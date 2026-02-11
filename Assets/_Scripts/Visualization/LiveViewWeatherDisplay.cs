using _Scripts.ScriptableObjects;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class LiveViewWeatherDisplay : MonoBehaviour
{
  
  [FormerlySerializedAs("weatherCodeContainer")] [SerializeField] WeatherSettingsContainer weatherSettingsContainer;
  private WeatherType _currentWeatherType = WeatherType.UNKNOWN;
  
  AsyncOperationHandle<GameObject> particleEffectHandle;
  private bool hasParticleInstance = false;
  
  
  public void UpdateAndShowWeather(WeatherData newWeatherData)
  {
    var newWeatherType = weatherSettingsContainer.GetWeatherTypeFromCode(newWeatherData.CurrentWeather.WeatherCode);
    if (_currentWeatherType == newWeatherType ||  newWeatherType == WeatherType.UNKNOWN)
      return;
    
    _currentWeatherType = newWeatherType;
    if (weatherSettingsContainer.TryGetWeatherSettings(_currentWeatherType, out var weatherSettings))
    {
      UpdateParticles(weatherSettings).Forget();
    }//todo unknown handling
    
  }

  private async UniTask UpdateParticles( WeatherSettings weatherSettings)
  {
    //todo check if show or rain or nothing
    if (hasParticleInstance && particleEffectHandle.IsValid())
    {
      Addressables.Release(particleEffectHandle);
      hasParticleInstance = false;
    }

    if (!weatherSettings.HasParticleEffect()) return;
    var assetReference = weatherSettings.GetParticleEffectAssetRef();
    
    particleEffectHandle = assetReference.InstantiateAsync(transform);
    await particleEffectHandle.Task.AsUniTask();
    hasParticleInstance = true;
  }

#if UNITY_EDITOR
  public WeatherSettings ws;
  [Button]
  public void Change()
  {
    UpdateParticles(ws).Forget();
  }
#endif
}
