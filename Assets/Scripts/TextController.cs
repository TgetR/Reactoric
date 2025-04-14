using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    ReactorController controller;
    public TMP_Text textTemperature;
    public TMP_Text textTemperatureMax;
    public TMP_Text textEnergy;
    public TMP_Text textEnergyMax;
    public GlobalData globalData;
    private int _Fatigue;
    private void Start()
    {
        controller = GetComponent<ReactorController>();
    }
    private void Update()
    {
        textEnergy.text = "ENERGY PRODUCTION:  " + ((float)(int)(controller.EnergyProduction * 100)) / 100 + "MWh";
        textTemperature.text = "Temperature: " + ((float)(int)(controller.Temperature * 100)) / 100 + "C";
        textTemperatureMax.text = "Maximum: " + globalData.TemperatureMax + "C";
        textEnergyMax.text = "Required: " + globalData.EnergyMax + " MWh";
    }
}
