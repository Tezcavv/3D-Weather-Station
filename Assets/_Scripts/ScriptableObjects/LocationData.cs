using UnityEngine;

[CreateAssetMenu]
public class LocationData : ScriptableObject
{
    [field: SerializeField, Range(-90, 90)] public float Latitude { get; private set; }
    [field: SerializeField, Range(-180, 180)] public float Longitude { get; private set; }
}
