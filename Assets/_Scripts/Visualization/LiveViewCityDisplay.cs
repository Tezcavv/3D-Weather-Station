using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LiveViewCityDisplay : MonoBehaviour
{
  AsyncOperationHandle<GameObject> cityInstanceHandle;
  GameObject cityInstance;
  private bool hasCityInstance;

  private LocationData currentLocation = null;

  public async UniTask UpdateAndShowCity(LocationData locationData)
  {
    if (locationData == null) return;
    if (currentLocation != null && currentLocation == locationData) return;

    currentLocation = locationData;

 
    if (hasCityInstance && cityInstanceHandle.IsValid())
    {
      Addressables.Release(cityInstanceHandle);
      hasCityInstance = false;
    }

    // Instantiate the addressable prefab
    cityInstanceHandle = locationData.CityModel.InstantiateAsync(transform);
    cityInstance = await cityInstanceHandle.Task.AsUniTask();
    hasCityInstance = true;
  }

  void Update()
  {
    if (hasCityInstance && cityInstance != null) 
      cityInstance.transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
  }

  private void OnDestroy()
  {
    if (hasCityInstance && cityInstanceHandle.IsValid())
      Addressables.Release(cityInstanceHandle);
  }
}