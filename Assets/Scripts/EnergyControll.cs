using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyControll : MonoBehaviour
{
    public float EnergyProduction = 0f;
    public bool Normal = true;

    private enum EnergyChangeType
    {
        None,
        DynamicRise,
        SlowRise,
        SlowReduction,
        DynamicReduction
    }

    private EnergyChangeType _energyChangeType = EnergyChangeType.None;

    [SerializeField] private GlobalData _globalData;
    [SerializeField] private GameObject _alarm;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private RodsController _rodsController;

    private int _activeRods = 0;

    private void Start()
    {
        StartCoroutine(ControlLoop());
    }

    private IEnumerator ControlLoop()
    {
        while (true)
        {
            UpdateEnergy();
            yield return new WaitForSeconds(0.75f);
        }
    }

    private void UpdateEnergy()
    {
        _activeRods = _rodsController.ActiveRodsCount;

        CheckRods();
        UpdateEnergyDynamic();
        UpdateAlarmStatus();
    }

    private void CheckRods()
    {
        if (_activeRods < 6) _energyChangeType = EnergyChangeType.DynamicReduction;
        else if (_activeRods < 12) _energyChangeType = EnergyChangeType.SlowReduction;
        else if (_activeRods < 18) _energyChangeType = EnergyChangeType.SlowRise;
        else if (_activeRods < 25) _energyChangeType = EnergyChangeType.DynamicRise;
        else if (_activeRods < 30) _energyChangeType = EnergyChangeType.SlowRise;
        else if (_activeRods < 34) _energyChangeType = EnergyChangeType.SlowReduction;
        else _energyChangeType = EnergyChangeType.DynamicReduction;
    }

    private void UpdateEnergyDynamic()
    {
        switch (_energyChangeType)
        {
            case EnergyChangeType.None:
                break;
            case EnergyChangeType.DynamicRise:
                EnergyProduction += EnergyProduction / 10f;
                break;
            case EnergyChangeType.SlowRise:
                EnergyProduction += EnergyProduction / 20f;
                break;
            case EnergyChangeType.DynamicReduction:
                EnergyProduction -= EnergyProduction / 10f;
                break;
            case EnergyChangeType.SlowReduction:
                EnergyProduction -= EnergyProduction / 20f;
                break;
        }
    }

    private void UpdateAlarmStatus()
    {
        if (EnergyProduction > _globalData.EnergyMax)
        {
            _energyText.text = "-Energy production plan overdrawn\n" + _energyText.text;
            Normal = true;
        }
        else Normal = false;

        if (EnergyProduction > _globalData.EnergyMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            _alarm.SetActive(EnergyProduction > _globalData.EnergyMax);
        }
    }
}
