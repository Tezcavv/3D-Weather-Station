using System;
using System.Collections.Generic;
using _Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class DashboardUI : MonoBehaviour
{
   [SerializeField] public List<LocationButton> locationButtons;
   [SerializeField] public Button changeViewButton;


   private StateMachine stateMachine;
   [Inject]
   private void Construct(StateMachine stateMachine)
   {
      this.stateMachine = stateMachine;
   }
   
   private void RegisterButtonEvents()
   {
      changeViewButton.onClick.AddListener(ChangeState);
   }
   
   private void UnregisterButtonEvents()
   {
      changeViewButton.onClick.RemoveAllListeners();
   }

   private async void ChangeState()
   {
     await stateMachine.ChangeStateAsync<LiveViewState>();
   }


   private void Start()
   {
      RegisterButtonEvents();
   }

   private void OnDestroy()
   {
      UnregisterButtonEvents();
   }


}