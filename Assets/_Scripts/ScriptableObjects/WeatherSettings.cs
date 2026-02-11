using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace _Scripts.ScriptableObjects
{
  [CreateAssetMenu]
  public class WeatherSettings : ScriptableObject
  {
    [field: FormerlySerializedAs("<WeatherData>k__BackingField")]
    [field: SerializeField]
    public WeatherType WeatherType { get; private set; }

    [Header("VFX")] [field: SerializeField]
    private AssetReferenceGameObject particles;

    [field: SerializeField] public string WeatherTypeName { get; private set; }

    [Header("Weather Codes")]
    [field: SerializeField]
    private List<int> Codes { get; set; }

    [Header("Lighting")] //assuming it's directional
    [field: SerializeField] public Color LightColor { get; private set; } = Color.white;
    [field: SerializeField] public float LightIntensity { get; private set; } = 1.0f;

    [Header("Post Process")]
    [field: SerializeField]
    public VolumeProfile VolumeProfile { get; private set; }

    public bool ContainsCode(int code)
    {
      return Codes.Contains(code);
    }

    public bool HasParticleEffect()
    {
      return particles != null && particles.RuntimeKeyIsValid();
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