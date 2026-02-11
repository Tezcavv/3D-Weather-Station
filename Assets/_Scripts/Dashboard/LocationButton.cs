using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationButton : MonoBehaviour
{
  [SerializeField] public Button locationButton;
  [SerializeField] public LocationData locationData;

  
  public event Action<LocationData> OnClick;
  private void Awake()
  {
    locationButton.onClick.AddListener(() => OnClick?.Invoke(locationData));
    locationButton.GetComponentInChildren<TextMeshProUGUI>().text = locationData.name;
  }

  private void OnDestroy()
  {
    locationButton.onClick.RemoveAllListeners();
  }
}