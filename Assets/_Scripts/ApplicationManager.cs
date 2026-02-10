
using System;
using _Scripts.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

using VContainer;


public class ApplicationManager : MonoBehaviour
{
    public AssetReferenceT<SceneAsset> dashboardScene;
    public AssetReferenceT<SceneAsset> liveViewScene;

    AsyncOperationHandle<SceneInstance> dashboardSceneHandle;
    AsyncOperationHandle<SceneInstance> liveViewSceneHandle;
    
     IStateMachine stateMachine;
    
    [Inject]
    public void Construct(IStateMachine sm)
    {
        this.stateMachine = sm;
    }
    



    public void Update()
    {
        stateMachine.UpdateCurrentState();
        //temp
        if (Input.GetKeyDown(KeyCode.S))
        {
            stateMachine.ChangeStateAsync<LiveViewState>();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.ChangeStateAsync<DashboardViewState>();
        }
    }
}