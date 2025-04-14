using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemperatureControll : MonoBehaviour
{
    public float Temperature = 120;
    public bool Normal = true;
    private enum TemperatureChangeType
    {
    None,
    DynamicRise,
    SlowRise,
    SlowReduction,
    DynamicReduction
    }
    private TemperatureChangeType temperatureChangeType = TemperatureChangeType.None;
    [SerializeField] private GlobalData _globalData;
    [SerializeField] private GameObject _alarm;
    [SerializeField] private TMP_Text _temperatureText;
    [SerializeField]private RodsController _rodsController;
    private int _activeRods = 0;

    void Start()
    {
        StartCoroutine(ControlLoop());
    }

    IEnumerator ControlLoop()
    {
        while (true)
        {
            ControlProduction();
            yield return new WaitForSeconds(0.75f);
        }
    }

    void ControlProduction()
    {   
        _activeRods = _rodsController.ActiveRodsCount;
        UpdateAlarmStatus(); //Alarm?
        CheckRods();
        UpdateTemperatureDynamic();
    }
    void CheckRods()
    {
        temperatureChangeType = _activeRods switch
            {
                <= 12 => TemperatureChangeType.DynamicRise,
                > 12 and < 18 => TemperatureChangeType.SlowRise,
                18 => TemperatureChangeType.None,
                > 18 and < 30 => TemperatureChangeType.SlowReduction,
                >= 30 => TemperatureChangeType.DynamicReduction
            };
    }
    void UpdateTemperatureDynamic()
    {
        switch (temperatureChangeType)
        {
            case TemperatureChangeType.None:
                //Stable
            break;
            //Dynamic temperature Rise
            case TemperatureChangeType.DynamicRise:
                Temperature = Temperature + (Temperature / 10f);
            break;
            //Slow temperature Rise
            case TemperatureChangeType.SlowRise:
                Temperature = Temperature + (Temperature / 20f);
            break;
            //Dynamic temperature Reduction
            case TemperatureChangeType.DynamicReduction:
                Temperature = Temperature - (Temperature / 10f);
            break;
            //Slow temperature Reduction
            case TemperatureChangeType.SlowReduction:
                Temperature = Temperature - (Temperature / 20f);
            break;
        }
    }
    void UpdateAlarmStatus()
    {
        if (Temperature  > _globalData.TemperatureMax )
        {
            _temperatureText.text = "-Temperature maximum is exceeded \n" + _temperatureText.text;
            Normal = false;
        }
        else Normal = true;

        
        if (Temperature  > _globalData.TemperatureMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (Temperature > _globalData.TemperatureMax)
        {
            _alarm.SetActive(true);
        }
        else
        {
            _alarm.SetActive(false);
        }
    }

}
