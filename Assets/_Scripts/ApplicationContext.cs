using _Scripts.StateMachine;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ApplicationContext : MonoBehaviour
{
  public static ApplicationContext Instance { get; private set; }

  [field: SerializeField] private LocationData defaultLocationData;
  [field: SerializeField] public SceneContainer sceneContainer; 
  public WeatherService WeatherService { get; private set; }
  public IStateMachine StateMachine {get; private set;}


    private void Awake()
    {
      if (Instance != null)
      {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      WeatherService = new WeatherService(new MockWeatherProvider());
      WeatherService.LocationData = defaultLocationData;
   
      StateMachine = new SceneStateMachine(sceneContainer);
      StateMachine.Init();
      WeatherService.RefreshData();
    }
  


  public void Update()
  {
    StateMachine.UpdateCurrentState();
    
     if (Input.GetKeyDown(KeyCode.S))
     {
       Debug.Log("Pressed");
         StateMachine.ChangeStateAsync(SceneType.LIVE);
     }
    
     if (Input.GetKeyDown(KeyCode.A))
     {
         StateMachine.ChangeStateAsync(SceneType.DASHBOARD);
     }
  }
}