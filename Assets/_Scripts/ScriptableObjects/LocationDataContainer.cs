using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
internal class LocationDataContainer : ScriptableObject
{
  [field:SerializeField]public List<LocationData> AllLocations {get; private set;}
}