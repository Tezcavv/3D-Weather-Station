using Cysharp.Threading.Tasks;
using VContainer;

namespace _Scripts.StateMachine
{
  public interface IState
  {
    UniTask Enter();
    UniTask Exit();
    void Update();
  }

  public class DashboardViewState : IState
  {
    
    private ISceneLoader sceneLoader;

    [Inject]
    public DashboardViewState(ISceneLoader sceneLoader)
    {
      this.sceneLoader = sceneLoader;
    }
    
    public async UniTask Enter()
    {
      await sceneLoader.LoadAsync("Dashboard View Scene");
    }



    public async UniTask Exit()
    {
      await sceneLoader.UnloadAsync("Dashboard View Scene");
    }

    public void Update()
    {
      
    }
  }
  
  public class LiveViewState : IState
  {
    private ISceneLoader sceneLoader;
    
    [Inject]
    public LiveViewState(ISceneLoader sceneLoader)
    {
      this.sceneLoader = sceneLoader;
    }
    public async UniTask Enter()
    {
      await sceneLoader.LoadAsync("Live View Scene");
    }

    public async UniTask Exit()
    {
      await sceneLoader.UnloadAsync("Live View Scene");
    }

    public void Update()
    {
      
    }
  }
}