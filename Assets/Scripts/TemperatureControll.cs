using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemperatureControll : MonoBehaviour
{
    public float Temperature = 120;
    public bool Normal = true;
    public bool dynamicTemperatureReduction;
    public bool slowTemperatureReduction;
    public bool dynamicTemperatureRise;
    public bool slowTemperatureRise;
    [SerializeField] private GlobalData _GlobalData;
    [SerializeField] private GameObject _Alarm;
    [SerializeField] private TMP_Text _Text;
    private RodsController _RodsController;
    private int _ActiveRods = 0;
    void Start()
    {
        _RodsController = GameObject.Find("RodsInfo").GetComponent<RodsController>();
        InvokeRepeating("ControlProduction", 0f, 0.75f);
    }

    void ControlProduction()
    {   
        if (Temperature  > _GlobalData.TemperatureMax )
        {
            _Text.text = "-Temperature maximum is exceeded \n" + _Text.text;
            Normal = false;
        }
        else Normal = true;
        if (Temperature  > _GlobalData.TemperatureMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (Temperature > _GlobalData.TemperatureMax)
        {
            _Alarm.SetActive(true);
        }

        else
        {
            _Alarm.SetActive(false);
        }
        _ActiveRods = _RodsController.ActiveRodsCount;
        if (_ActiveRods <= 12)
        {
            Temperature = Temperature + (Temperature / 10f);
            //DynamicTemperatureRise
            dynamicTemperatureReduction = false;
            slowTemperatureReduction = false;
            dynamicTemperatureRise = true;
            slowTemperatureRise = false;
        }
        else if (_ActiveRods > 12 && _ActiveRods < 18)
        {
            Temperature = Temperature + (Temperature / 20f);
            //SlowTemperatureRise
            dynamicTemperatureReduction = false;
            slowTemperatureReduction = false;
            dynamicTemperatureRise = false;
            slowTemperatureRise = true;
        }
        else if (_ActiveRods == 18)
        {
            dynamicTemperatureReduction = false;
            slowTemperatureReduction = false;
            dynamicTemperatureRise = false;
            slowTemperatureRise = false;
        }
        else if (_ActiveRods > 18 && _ActiveRods < 30)
        {
            Temperature = Temperature - (Temperature / 20f);
            //SlowTemperatureReduction
            dynamicTemperatureReduction = false;
            slowTemperatureReduction = true;
            dynamicTemperatureRise = false;
            slowTemperatureRise = false;
        }
        else if (_ActiveRods >= 30)
        {
            Temperature = Temperature - (Temperature / 10f);
            //DynamicTemperatureReduction
            dynamicTemperatureReduction = true;
            slowTemperatureReduction = false;
            dynamicTemperatureRise = false;
            slowTemperatureRise = false;
        }

    }

}
