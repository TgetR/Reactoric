using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReactorController : MonoBehaviour
{
public enum TemperatureChangeType
{
    None,
    DynamicRise,
    SlowRise,
    Stable,
    SlowReduction,
    DynamicReduction
}
public enum EnergyChangeType
{
    None,
    DynamicRise,
    SlowRise,
    Stable,
    SlowReduction,
    DynamicReduction
}



[Header("Core Data")]
    public float Temperature = 120f;
    public float EnergyProduction = 0f;
    public bool Normal = true;

[Header("References")]
    [SerializeField] private GlobalData _globalData;
    [SerializeField] private GameObject _alarm;
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private RodsController _rodsController;

    private TemperatureChangeType _temperatureChangeType;
    private EnergyChangeType _energyChangeType;

    private int _activeRods = 0;
    void Start()
    {
    StartCoroutine(ControlLoop());
    }

    IEnumerator ControlLoop()
    {
        while (true)
        {
            UpdateReactorState();
            yield return new WaitForSeconds(0.75f);
        }
    }
    void UpdateReactorState()
    {
        _activeRods = _rodsController.ActiveRodsCount;

        EvaluateTemperatureChange();
        EvaluateEnergyChange();

        ApplyTemperatureChange();
        ApplyEnergyChange();

        UpdateAlarms();
    }
    void EvaluateTemperatureChange()
    {
        if (_activeRods <= 12)
            _temperatureChangeType = TemperatureChangeType.DynamicRise;
        else if (_activeRods < 18)
            _temperatureChangeType = TemperatureChangeType.SlowRise;
        else if (_activeRods == 18)
            _temperatureChangeType = TemperatureChangeType.Stable;
        else if (_activeRods < 30)
            _temperatureChangeType = TemperatureChangeType.SlowReduction;
        else
            _temperatureChangeType = TemperatureChangeType.DynamicReduction;
    }

    void EvaluateEnergyChange()
    {
        if (_activeRods < 6)
            _energyChangeType = EnergyChangeType.DynamicReduction;
        else if (_activeRods < 12)
            _energyChangeType = EnergyChangeType.SlowReduction;
        else if (_activeRods < 18)
            _energyChangeType = EnergyChangeType.SlowRise;
        else if (_activeRods < 25)
            _energyChangeType = EnergyChangeType.DynamicRise;
        else if (_activeRods < 34)
            _energyChangeType = EnergyChangeType.SlowRise;
        else
            _energyChangeType = EnergyChangeType.DynamicReduction; // overload
    }
    void ApplyTemperatureChange()
    {
        float multiplier = GetTemperatureChangeMultiplier(_temperatureChangeType);
        Temperature += Temperature * multiplier;
    }
    void ApplyEnergyChange()
    {
        float baseEfficiency = 1f - Mathf.Clamp01(Temperature / (_globalData.TemperatureMax * 2f)); // жарко — меньше эффективность
        float multiplier = GetEnergyChangeMultiplier(_energyChangeType);
        EnergyProduction += EnergyProduction * multiplier * baseEfficiency;
        Debug.Log("EnergyProduction: " + EnergyProduction);
    }
    float GetTemperatureChangeMultiplier(TemperatureChangeType type)
    {
        switch (type)
        {
            case TemperatureChangeType.DynamicRise: return +0.12f;
            case TemperatureChangeType.SlowRise: return +0.06f;
            case TemperatureChangeType.Stable: return 0f;
            case TemperatureChangeType.SlowReduction: return -0.05f;
            case TemperatureChangeType.DynamicReduction: return -0.10f;
            default: return 0f;
        }
    }

    float GetEnergyChangeMultiplier(EnergyChangeType type)
    {
        switch (type)
        {
            case EnergyChangeType.DynamicRise: return +0.08f;
            case EnergyChangeType.SlowRise: return +0.04f;
            case EnergyChangeType.Stable: return 0f;
            case EnergyChangeType.SlowReduction: return -0.03f;
            case EnergyChangeType.DynamicReduction: return -0.07f;
            default: return 0f;
        }
    }

    void UpdateAlarms()
    {
        if (Temperature > _globalData.TemperatureMax || EnergyProduction > _globalData.EnergyMax)
        {
            _infoText.text = "-Warning: limits exceeded\n" + _infoText.text;
            Normal = false;
        }
        else Normal = true;

        if (Temperature > _globalData.TemperatureMax + 400 || EnergyProduction > _globalData.EnergyMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }

        _alarm.SetActive(Temperature > _globalData.TemperatureMax || EnergyProduction > _globalData.EnergyMax);
    }

}
