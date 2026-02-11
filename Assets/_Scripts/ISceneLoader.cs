using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;

public interface ISceneLoader
{
  UniTask LoadAsync(string sceneName);
  UniTask UnloadAsync(string sceneName);
}


public sealed class SceneLoader : ISceneLoader
{
  public async UniTask LoadAsync(string sceneName)
  {
    if (IsSceneLoaded(sceneName))
      return;

    var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    if (asyncOperation == null)
      throw new InvalidOperationException($"LoadSceneAsync failed for scene '{sceneName}'.");

    await asyncOperation.ToUniTask(); 

    var scene = SceneManager.GetSceneByName(sceneName);
    if (scene.IsValid() && scene.isLoaded)
      SceneManager.SetActiveScene(scene);
  }

  public async UniTask UnloadAsync(string sceneName)
  {
    if (!IsSceneLoaded(sceneName))
      return;

    var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
    if (asyncOperation == null)
      return;

    await asyncOperation.ToUniTask();
  }
  
  private bool IsSceneLoaded(string sceneName)
  {
    var scene = SceneManager.GetSceneByName(sceneName);
    return scene.IsValid() && scene.isLoaded;
  }
}