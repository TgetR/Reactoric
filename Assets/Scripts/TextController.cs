using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    TemperatureControll temperature;
    EnergyControll energy;
    public TMP_Text textTemperature;
    public TMP_Text textTemperatureMax;
    public TMP_Text textEnergy;
    public TMP_Text textEnergyMax;
    public GlobalData globalData;
    private void Start()
    {
        temperature = GetComponent<TemperatureControll>();
        energy = GetComponent<EnergyControll>();
    }
    private void Update()
    {
        textEnergy.text = "ENERGY PRODUCTION:  " + energy.EnergyProduction + "MWh";
        textEnergyMax.text = "Required: " + globalData.EnergyMax + " MWh";
        textTemperature.text = "Temperature: " + temperature.Temperature + "C";
        textTemperatureMax.text = "Maximum: " + globalData.TemperatureMax + "C";

    }
}
