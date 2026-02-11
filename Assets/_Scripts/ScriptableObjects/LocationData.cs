using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class LocationData : ScriptableObject
{
    [field: FormerlySerializedAs("<Name>k__BackingField")] [field:SerializeField] public string LocationName { get; private set; }
    [field: SerializeField] public AssetReference CityModel { get; private set; }
    [field: SerializeField, Range(-90, 90)] public float Latitude { get; private set; }
    [field: SerializeField, Range(-180, 180)] public float Longitude { get; private set; }
}
