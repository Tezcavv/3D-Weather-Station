using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DashboardController : MonoBehaviour
{

  [SerializeField] private AssetReferenceGameObject uiPrefabRef;

  private AsyncOperationHandle<GameObject> uiInstanceHandle;

  private async void Start()
  {
    uiInstanceHandle = uiPrefabRef.InstantiateAsync();
    var uiGo = await uiInstanceHandle.Task;
    
  }

  private void OnDestroy()
  {
    if (uiInstanceHandle.IsValid())
      Addressables.Release(uiInstanceHandle); // releases instance + deps
  }
}
