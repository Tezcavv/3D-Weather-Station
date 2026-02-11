using System;
using TMPro;
using UnityEngine;

public class FPSCounterDisplay : MonoBehaviour
{
    
    [SerializeField]private TextMeshProUGUI fpsText;
    
    float _dt = 0;

    void Awake()
    {
        //in project timescale never changes
        _dt = Time.deltaTime;
    }

    void Update()
    {
        // smoothing
        _dt = Mathf.Lerp(_dt, Time.deltaTime, 0.1f);

        float fps = 1f / _dt;
        fpsText.text = $"{fps:0.} FPS";
    }
}
