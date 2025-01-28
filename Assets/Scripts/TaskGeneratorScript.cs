using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;
public class TaskGeneratorScript : MonoBehaviour
{
    public float multiplier = 1.0f;
    [SerializeField] TMP_Text _Text;
    private int _AmountOfTasks = 1; //How types of tasks exitis
    private int _StartDurationInSec = 60;
    private int _DurationInSec = 60;
    private TemperatureControll _Temperature;
    private EnergyControll _Energy;
    private List<string> _Tasks = new List<string>(){"EnergyTime" , "Time"};
    void Awake() 
    {
        float j = _StartDurationInSec * multiplier; 
        _StartDurationInSec = (int)j;
        int i = Random.Range(0, _AmountOfTasks );
        switch ( i )
        {
            case 0:
                InvokeRepeating("EnergyTime",0f, 1f); break;
            case 1:
                InvokeRepeating("Time",0f, 1f); break;
        }
    }
    private void Start() 
    {
        _Energy = gameObject.GetComponent<EnergyControll>();
        _Temperature = gameObject.GetComponent<TemperatureControll>();
    }
    public void EnergyTime()
    {
        if(_Energy.Normal) _DurationInSec--;
        _Text.text = "Task: keep energy production normal for the duration: " + _StartDurationInSec + " seconds";
    }
    public void Time()
    {
        if(_Energy.Normal && _Temperature.Normal) _DurationInSec--;
        _Text.text = "Task: keep normal for the duration: " + _StartDurationInSec + " seconds";
    }
}