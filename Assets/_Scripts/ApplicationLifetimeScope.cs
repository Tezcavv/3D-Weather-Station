
using VContainer;
using VContainer.Unity;
using _Scripts.StateMachine;

namespace _Scripts
{
  /// <summary>
  /// Main Scene (Permanent) Injector
  /// </summary>
  public class ApplicationLifetimeScope : LifetimeScope
  {
    public ApplicationManager applicationManager;
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterComponent(applicationManager);
      builder.Register<StateMachine.StateMachine>(Lifetime.Singleton).As<IStateMachine>();
      builder.Register<StateResolver>(Lifetime.Singleton);
      builder.Register<LiveViewState>(Lifetime.Singleton);
      builder.Register<DashboardViewState>(Lifetime.Singleton);
      builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();
    }
  }
}