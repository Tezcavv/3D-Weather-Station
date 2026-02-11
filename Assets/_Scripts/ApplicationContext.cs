using System.Linq;
using _Scripts.ScriptableObjects;
using _Scripts.StateMachine;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class ApplicationContext : MonoBehaviour
{
  public static ApplicationContext Instance { get; private set; }

  [FormerlySerializedAs("DefaultLocationData")] [SerializeField]
  private LocationData defaultLocationData;

  [SerializeField] private LocationDataContainer allLocations;
  [field: SerializeField] public SceneContainer sceneContainer;
  [field: SerializeField] public WeatherSettingsContainer WeatherSettingsContainer { get; private set; }
  [field: SerializeField] public WeatherSamplesContainer WeatherSamplesContainer { get; private set; }
  
  public WeatherService WeatherService { get; private set; }
  public IStateMachine StateMachine { get; private set; }
  
  private LocationData GetLocationData()
  {
    if (!PlayerPrefs.HasKey("LocationData")) return defaultLocationData;
    var savedLoc = PlayerPrefs.GetString("LocationData");
    foreach (var locationData in allLocations.AllLocations)
    {
      if (locationData.LocationName == savedLoc) return locationData;
    }
    return defaultLocationData;
  }

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
    WeatherService = new WeatherService(WeatherSamplesContainer);
    WeatherService.LocationData = GetLocationData();
    WeatherService.SetOpenMeteoWeatherProvider();
    WeatherService.RefreshData().Forget();
    StateMachine = new SceneStateMachine(sceneContainer);
    StateMachine.Init();
  }


  public void Update()
  {
    StateMachine.UpdateCurrentState();

    // if (Input.GetKeyDown(KeyCode.S))
    // {
    //   Debug.Log("Pressed");
    //   StateMachine.ChangeStateAsync(SceneType.LIVE);
    // }
    //
    // if (Input.GetKeyDown(KeyCode.A))
    // {
    //   StateMachine.ChangeStateAsync(SceneType.DASHBOARD);
    // }
  }
}