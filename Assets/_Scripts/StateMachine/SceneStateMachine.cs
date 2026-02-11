using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Scripts.StateMachine
{
  public interface IStateMachine
  {
    UniTask ChangeStateAsync(SceneType newSceneTypeType);
    void UpdateCurrentState();
    void Init();
  }

  
  
  public class SceneStateMachine : IStateMachine
  {

    private bool _isTransitioning = false;
    private Dictionary<SceneType, ISceneState> sceneDictionary;
    private ISceneState _current;
    
    public SceneStateMachine(SceneContainer sceneContainer)
    {
      sceneDictionary = new Dictionary<SceneType, ISceneState>();
      sceneDictionary.Add(SceneType.DASHBOARD,new SceneSceneState(sceneContainer.DashboardScene));
      sceneDictionary.Add(SceneType.LIVE,new SceneSceneState(sceneContainer.LiveScene));
      
    }

    public void Init()
    {
      _ = ChangeStateAsync(SceneType.DASHBOARD);
    }

    public async UniTask ChangeStateAsync(SceneType newSceneTypeType)
    {
      if (_isTransitioning) return;
      _isTransitioning = true;
      
      if(_current != null && _current == sceneDictionary[newSceneTypeType]) return;

      if (_current != null) await _current.Exit();
      _current = sceneDictionary[newSceneTypeType];
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

public enum SceneType
{
  DASHBOARD,
  LIVE
  
}