using System;
using System.Collections.Generic;
using _Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LocationSwapperController : MonoBehaviour
{
   [SerializeField] public List<LocationButton> locationButtons;
   
   private void RegisterButtonEvents()
   {
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
  
      foreach (var locationButton in locationButtons)
      {
         if(locationButton != null)
            locationButton.OnClick -= (UpdateLocation);
      }
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