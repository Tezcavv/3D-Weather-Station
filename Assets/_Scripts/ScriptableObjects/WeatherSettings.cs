using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace _Scripts.ScriptableObjects
{
  [CreateAssetMenu]
  public class WeatherSettings : ScriptableObject
  {
    [field: FormerlySerializedAs("<WeatherData>k__BackingField")] [field:SerializeField] public WeatherType WeatherType { get; private set; }
    [field: SerializeField] private AssetReferenceGameObject particles;
    [field:SerializeField] public string WeatherTypeName { get; private set; }
    [field:SerializeField] private List<int> Codes { get; set; }
   
    public bool ContainsCode(int code)
    {
      return Codes.Contains(code);
    }

    public bool HasParticleEffect()
    {
      return particles != null;
    }

    public AssetReferenceGameObject GetParticleEffectAssetRef()
    {
      return particles;
    }
   
  }

  [Serializable]
  public enum WeatherType
  {
    UNKNOWN,
    CLEAR,
    RAIN,
    SNOW
  
  }
}