using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Scripts.StateMachine
{
  public interface IState
  {
    UniTask Enter();
    UniTask Exit();
    void Update();
  }
  
  public class SceneState : IState
  {
    readonly AssetReference sceneAsset;
    AsyncOperationHandle<SceneInstance> sceneHandle;
    bool loaded;

    public SceneState(AssetReference sceneAsset)
    {
      this.sceneAsset = sceneAsset;
      loaded = false;
    }

    public virtual async UniTask Enter()
    {
      if (loaded) return;

      sceneHandle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, activateOnLoad: true);
      await sceneHandle.Task.AsUniTask();
      loaded = true;
    }

    public virtual async UniTask Exit()
    {
      if (!loaded) return;
      if (!sceneHandle.IsValid()) { loaded = false; return; }

      await Addressables.UnloadSceneAsync(sceneHandle).Task.AsUniTask();
      loaded = false;
    }

    public virtual void Update() { }
  }
  
}