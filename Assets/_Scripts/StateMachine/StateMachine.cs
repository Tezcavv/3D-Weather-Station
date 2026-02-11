using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Scripts.StateMachine
{
  public interface IStateMachine
  {
    UniTask ChangeStateAsync(StateScene newSceneType);
    void UpdateCurrentState();
    void Init();
  }

  
  
  public class StateMachine : IStateMachine
  {

    private bool _isTransitioning = false;
    private Dictionary<StateScene, IState> sceneDictionary;
    private IState _current;
    
    public StateMachine(SceneContainer sceneContainer)
    {
      sceneDictionary = new Dictionary<StateScene, IState>();
      sceneDictionary.Add(StateScene.DASHBOARD,new SceneState(sceneContainer.DashboardScene));
      sceneDictionary.Add(StateScene.LIVE,new SceneState(sceneContainer.LiveScene));
      
    }

    public void Init()
    {
      //enter dashboard
    }

    public async UniTask ChangeStateAsync(StateScene newSceneType)
    {
      if (_isTransitioning) return;
      _isTransitioning = true;
      
      if(_current != null && _current == sceneDictionary[newSceneType]) return;

      if (_current != null) await _current.Exit();
      _current = sceneDictionary[newSceneType];
      await _current.Enter();

      _isTransitioning = false;
    }

    public void UpdateCurrentState()
    {
      if(_isTransitioning) return;
      _current?.Update();
    }
  }
}

public enum StateScene
{
  DASHBOARD,
  LIVE
  
}