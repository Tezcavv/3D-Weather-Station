using System;
using _Scripts.WeatherService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ProviderSwapperButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] ProviderType provider;

 

    private void UpdateProvider()
    {
        switch (provider)
        {
            case ProviderType.OpenMeteo:
                ApplicationContext.Instance.WeatherService.SetOpenMeteoWeatherProvider();
                break;
            case ProviderType.Random:
                ApplicationContext.Instance.WeatherService.SetRandomWeatherProvider();
                break;
        }
        ApplicationContext.Instance.WeatherService.RefreshData().Forget();
    }
    
    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void Awake()
    {
        button.onClick.AddListener(UpdateProvider);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(UpdateProvider);
    }
}

