using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public string PlayerName = string.Empty;
    public int Day = 0;
    public int ReactorStatus = 0;
    public int Heat = 0;
    public int Cold = 0;
    public int EnergyMax = 175;
    public int TemperatureMax = 350;
    private void Start()
    {
        UploadData();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    public void UploadData()
    {
        Day = PlayerPrefs.GetInt("Day");
        ReactorStatus = PlayerPrefs.GetInt("ReactorStatus");
        Heat = PlayerPrefs.GetInt("Heat");
        Cold = PlayerPrefs.GetInt("Cold");
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Day", Day);
        PlayerPrefs.SetInt("ReactorStatus", ReactorStatus);
        PlayerPrefs.SetInt("Heat", Heat);
        PlayerPrefs.SetInt("Cold", Cold);
    }

}
