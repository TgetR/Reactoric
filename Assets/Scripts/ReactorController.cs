using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReactorController : MonoBehaviour
{

[Header("Core Data")]
    public float Temperature = 200f;
    public float EnergyProduction = 120f;
    public bool Normal = true;

[Header("References")]
    [SerializeField] private GlobalData _globalData;
    [SerializeField] private GameObject _alarm;
    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private RodsController _rodsController;

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

        ApplyTemperatureChange();
        ApplyEnergyChange();

        UpdateAlarms();
    }
    void ApplyTemperatureChange()
    {
    float multiplier = GetDynamicMultiplier(_activeRods, 6, 30, 0.25f, -0.2f);
    float delta = Temperature * multiplier;

    if (Mathf.Approximately(Temperature, 0f))
        delta = 0.1f;

    Temperature += delta;
    }

    void ApplyEnergyChange()
    {
        float baseEfficiency = 1f - Mathf.Clamp01(Temperature / (_globalData.TemperatureMax * 2f));
        float multiplier = GetDynamicMultiplier(_activeRods, 0, 34, -0.15f, 0.2f);
        float delta = EnergyProduction * multiplier * baseEfficiency;

        if (Mathf.Approximately(EnergyProduction, 0f))
            delta = 0.1f;

        EnergyProduction += delta;
    }

    float GetDynamicMultiplier(int value, int min, int max, float minMult, float maxMult)
    {
        float t = Mathf.InverseLerp(min, max, value);
        return Mathf.Lerp(minMult, maxMult, t);
    }

    void UpdateAlarms()
    {
        if (Temperature > _globalData.TemperatureMax || EnergyProduction > _globalData.EnergyMax)
        {
            _infoText.text = "-Warning: limits exceeded\n" + _infoText.text;
            Normal = false;
            if (_alarm.activeSelf != true) _alarm.SetActive(true);
        }
        else Normal = true;

        if (Temperature > _globalData.TemperatureMax + 400 || EnergyProduction > _globalData.EnergyMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }

        
    }

}
