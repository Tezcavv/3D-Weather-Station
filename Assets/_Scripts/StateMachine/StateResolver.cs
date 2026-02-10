namespace _Scripts.StateMachine
{
  using VContainer;

  public class StateResolver
  {
    private IObjectResolver _container;

    public StateResolver(IObjectResolver container)
    {
      _container = container;
    }

    public TState Resolve<TState>() where TState : IState
    {
      return _container.Resolve<TState>();
    }
  }
}