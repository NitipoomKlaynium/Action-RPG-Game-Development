using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private Material skyboxDay;
    [SerializeField] private Material skyboxNight;


    [SerializeField] private float cycleTime = 1440;
    [SerializeField] private float currentTime = 0;
    private float prevTime = 0;

    [SerializeField] public TMP_Text text;



    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //text.SetText("Time : " + (currentTime / 60).ToString("0") + ":" + (currentTime % 60).ToString("00"));
        //CycleUpdate();
    }

    private void CycleUpdate()
    {
        prevTime = currentTime;
        currentTime += Time.deltaTime * 80;



        if (currentTime > cycleTime)
        {
            currentTime = 0;
            RenderSettings.skybox = skyboxDay;
            Debug.Log("Day");
        }

        if (prevTime < cycleTime / 2 && currentTime > cycleTime / 2)
        {
            RenderSettings.skybox = skyboxNight;
            Debug.Log("Night");
        }


    }
}
