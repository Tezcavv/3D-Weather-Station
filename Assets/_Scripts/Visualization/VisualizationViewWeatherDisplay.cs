using _Scripts.ScriptableObjects;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class VisualizationViewWeatherDisplay : MonoBehaviour
{
  
  [Header("Scene References For visuals")]
  [SerializeField] Light sceneLight;
  [SerializeField] private Volume volume;
  [SerializeField] private VolumeProfile defaultVolumeProfile;
  [Header("")]
  [FormerlySerializedAs("weatherCodeContainer")] [SerializeField] WeatherSettingsContainer weatherSettingsContainer;
  private WeatherType _currentWeatherType = WeatherType.UNKNOWN;
  
  AsyncOperationHandle<GameObject> particleEffectHandle;
  private bool hasParticleInstance = false;
  
  
  public async UniTask UpdateAndShowWeather(WeatherData newWeatherData)
  {
    var newWeatherType = weatherSettingsContainer.GetWeatherTypeFromCode(newWeatherData.CurrentWeather.WeatherCode);
    if (_currentWeatherType == newWeatherType ||  newWeatherType == WeatherType.UNKNOWN)
      return;
    
    _currentWeatherType = newWeatherType;
    if (weatherSettingsContainer.TryGetWeatherSettings(_currentWeatherType, out var weatherSettings))
    {
      await UpdateParticles(weatherSettings);
      UpdateLighting(weatherSettings);
      UpdateVolume(weatherSettings);
    }//todo unknown handling
    
  }

  private void UpdateVolume(WeatherSettings weatherSettings)
  {
    if (weatherSettings.VolumeProfile == null)
    {
      volume.profile = defaultVolumeProfile;
      return;
    }
    volume.profile = weatherSettings.VolumeProfile;
  }

  private void UpdateLighting(WeatherSettings weatherSettings)
  {
    sceneLight.intensity = weatherSettings.LightIntensity;
    sceneLight.color = weatherSettings.LightColor;
  }


  private async UniTask UpdateParticles( WeatherSettings weatherSettings)
  {
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

  private void OnDestroy()
  {
    if (hasParticleInstance && particleEffectHandle.IsValid())
    {
      Addressables.Release(particleEffectHandle);
      hasParticleInstance = false;
    }
  }

}
