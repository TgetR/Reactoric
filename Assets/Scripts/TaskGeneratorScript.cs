using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class TaskGeneratorScript : MonoBehaviour
{
    public float multiplier = 1.0f;
    [SerializeField] TMP_Text _Text;
    private int _AmountOfTasks = 1; //How types of tasks exitis
    private int _StartDurationInSec = 60;
    private int _DurationInSec = 60;
    private ReactorController _reactor;
    private List<string> _Tasks = new List<string>(){/*"EnergyTime" ,*/  "Time"};
    void Awake() 
    {
        float j = _StartDurationInSec * multiplier; 
        _StartDurationInSec = (int)j;
        int i = Random.Range(1,1); //Temporary
        switch ( i )
        {
            case 1:
                InvokeRepeating("Time",0f, 1f); break;
        }
    }
    private void Start() 
    {
        _reactor = gameObject.GetComponent<ReactorController>();
    }
    public void Time()
    {
        if(_reactor.Normal) _DurationInSec--;
        if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0]) _Text.text = "Task: keep normal for the duration: " + _StartDurationInSec + " seconds";
        else if(LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1]) _Text.text = "Задание: сохраняйте все значения в норме в течении" + _StartDurationInSec + " секунд";
    }
}