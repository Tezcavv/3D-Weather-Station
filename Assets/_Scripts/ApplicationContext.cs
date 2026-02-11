using System;
using _Scripts.StateMachine;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ApplicationContext : MonoBehaviour
{
  public static ApplicationContext Instance { get; private set; }

  [field: SerializeField] public SceneContainer sceneContainer; 
  
  IStateMachine stateMachine;


    private void Awake()
    {
      if (Instance != null)
      {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      stateMachine = new StateMachine(sceneContainer);
      stateMachine.Init();
    }
  


  public void Update()
  {
    stateMachine.UpdateCurrentState();
    
     if (Input.GetKeyDown(KeyCode.S))
     {
       Debug.Log("Pressed");
         stateMachine.ChangeStateAsync(StateScene.LIVE);
     }
    
     if (Input.GetKeyDown(KeyCode.A))
     {
         stateMachine.ChangeStateAsync(StateScene.DASHBOARD);
     }
  }
}

[Serializable]
public class SceneContainer
{
  [field: SerializeField] public AssetReference DashboardScene { get; private set; }
  [field: SerializeField] public AssetReference LiveScene { get; private set; }
}