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

   
   private void RegisterButtonEvents()
   {
      changeViewButton.onClick.AddListener(ChangeState);
      foreach (var locationButton in locationButtons)
      {
         locationButton.OnClick += (UpdateLocation);
      }
   }

   private void UpdateLocation(LocationData locationData)
   {
      ApplicationContext.Instance.WeatherService.LocationData = locationData;
      ApplicationContext.Instance.WeatherService.RefreshData();
   }

   private void UnregisterButtonEvents()
   {
      changeViewButton.onClick.RemoveAllListeners();
      foreach (var locationButton in locationButtons)
      {
         if(locationButton != null)
            locationButton.OnClick -= (UpdateLocation);
      }
   }

   private async void ChangeState()
   {
     await ApplicationContext.Instance.StateMachine.ChangeStateAsync(SceneType.LIVE);
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