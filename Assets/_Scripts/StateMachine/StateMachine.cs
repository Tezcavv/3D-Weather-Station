using System;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Scripts.StateMachine
{
  public interface IStateMachine
  {
    UniTask ChangeStateAsync<TState>() where TState : IState;
    void UpdateCurrentState();
  }

  public class StateMachine : IStateMachine
  {
    private StateResolver _resolver;
    private IState _current;
    private bool _isTransitioning;

    [Inject]
    public StateMachine(StateResolver resolver)
    {
      _resolver = resolver;
    }

    public async UniTask ChangeStateAsync<TState>() where TState : IState
    {
      if (_isTransitioning) return;
      _isTransitioning = true;
      
      IState newState = _resolver.Resolve<TState>();
      
      if (newState == null)
      {
        throw new Exception("New state is null");
      }

      if (_current != null) await _current.Exit();
      _current = newState;
      await _current.Enter();

      _isTransitioning = false;
    }

    public void UpdateCurrentState()
    {
      _current?.Update();
    }
  }
}