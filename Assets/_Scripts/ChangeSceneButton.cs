using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField] private SceneType scene;
    
    private void ChangeScene()
    {
        ApplicationContext.Instance.StateMachine.ChangeStateAsync(scene);
    }
    
    private void Awake()
    {
        button.onClick.AddListener(ChangeScene);
    }


    private void OnDestroy()
    {
        button?.onClick.RemoveListener(ChangeScene);
    }

    private void Reset()
    {
        button = GetComponent<Button>();
    }
}
