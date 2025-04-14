using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAndClock : MonoBehaviour
{
public TMP_Text clock;
public TMP_Text leftTime;

public int TimeMinutes = 00;
public int TimeHours = 8;
public int LeftMinutes = 00;
public int LeftHours = 12;
private Fatigue _fatigue;
private void Start() 
{
    _fatigue = gameObject.GetComponent<Fatigue>();
    Time.timeScale = 1f;
    InvokeRepeating("TimeBegin",0,0.25f);
    LeftHours = Random.Range(6,20);
}
private void Update() 
{
    //Win check
    if(LeftHours < 0)
    {
        SceneManager.LoadScene("Win");
    }
    //Cosmetic check's
    if(TimeMinutes < 10) clock.text = TimeHours + ":0" + TimeMinutes;
    else clock.text = TimeHours + ":" + TimeMinutes;

    if(LeftMinutes < 10) leftTime.text = LeftHours + ":0" + LeftMinutes;
    else leftTime.text = LeftHours + ":" + LeftMinutes;
}
private void TimeBegin()
{    
    TimeMinutes++;
    LeftMinutes--;
    if(TimeMinutes == 60)
    {
        TimeMinutes = 0;
        TimeHours++;
    }
    if(LeftMinutes <= 0)
    {
        _fatigue.FatigueIndex--;
        LeftMinutes = 59;
        LeftHours--;
    }
}
}
