using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class SceneContainer
{
  [field: SerializeField] public AssetReference DashboardScene { get; private set; }
  [field: SerializeField] public AssetReference LiveScene { get; private set; }
}