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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        textEnergy.text = "Энерговыработка: " + energy.EnergyProduction + " МВат.час";
        textEnergyMax.text = "Максимум: " + globalData.EnergyMax + " МВат.час";
        textTemperature.text = "Температура: " + temperature.Temperature + "°C";
        textTemperatureMax.text = "Максимум: " + globalData.TemperatureMax + "°C";
=======
=======
>>>>>>> Stashed changes
        textEnergy.text = "ENERGY PRODUCTION:  " + ((float)(int)(energy.EnergyProduction * 100)) / 100 + "MWh";
        textEnergyMax.text = "Required: " + globalData.EnergyMax + " MWh";
        textTemperature.text = "Temperature: " + ((float)(int)(temperature.Temperature * 100)) / 100 + "C";
        textTemperatureMax.text = "Maximum: " + globalData.TemperatureMax + "C";
>>>>>>> Stashed changes

    }
}
