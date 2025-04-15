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
        LoadFromPrefs();
    }

    private void OnApplicationQuit()
    {
        SaveToPrefs();
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetInt("Day", Day);
        PlayerPrefs.SetInt("ReactorStatus", ReactorStatus);
        PlayerPrefs.SetInt("Heat", Heat);
        PlayerPrefs.SetInt("Cold", Cold);
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.Save();
    }

    public void LoadFromPrefs()
    {
        Day = PlayerPrefs.GetInt("Day", 0);
        ReactorStatus = PlayerPrefs.GetInt("ReactorStatus", 0);
        Heat = PlayerPrefs.GetInt("Heat", 0);
        Cold = PlayerPrefs.GetInt("Cold", 0);
        PlayerName = PlayerPrefs.GetString("PlayerName", "Unknown");
    }
}
