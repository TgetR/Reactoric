using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyControll : MonoBehaviour
{
    public bool Normal;
    public float EnergyProduction = 0f;
    public bool dynamicEnergyReduction;
    public bool slowEnergyReduction;
    public bool dynamicEnergyRise;
    public bool slowEnergyRise;
    [SerializeField] private GlobalData _GlobalData;
    [SerializeField] private GameObject _Alarm;
    [SerializeField] private Animator _Picture;
    [SerializeField] private TMP_Text _Text;
    private int _ActiveRods = 0;
    private RodsController _RodsController;
    private void Start()
    {
        _RodsController = GameObject.Find("RodsInfo").GetComponent<RodsController>();
        InvokeRepeating("ControlProduction", 0f, 0.75f);
    }
    private void ControlProduction()
    {

        if (EnergyProduction  > _GlobalData.EnergyMax)
        {
            _Text.text = "-Energy production plan overdrawn \n" + _Text.text;
            Normal = true;
        } 
        else  Normal = false;
        if (EnergyProduction  > _GlobalData.EnergyMax + 400)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (EnergyProduction > _GlobalData.EnergyMax)
        {
            _Alarm.SetActive(true);
            _Picture.SetBool("Alarm",true);
        }
        else
        {
            _Alarm.SetActive(false);
            _Picture.SetBool("Alarm",false);
        }

        _ActiveRods = _RodsController.ActiveRodsCount;
        Debug.Log("ECAR: " + _ActiveRods);
        if (_ActiveRods < 6)
        {
            EnergyProduction = EnergyProduction - (EnergyProduction / 10f);
            //DynamicEnergyReduction
            dynamicEnergyReduction = true;
            slowEnergyReduction = false;
            dynamicEnergyRise = false;
            slowEnergyRise = false;
        }
        if (_ActiveRods > 6 && _ActiveRods < 12)
        {
            EnergyProduction = EnergyProduction - (EnergyProduction / 20f);
            //SlowEnergyReduction
            dynamicEnergyReduction = false;
            slowEnergyReduction = true;
            dynamicEnergyRise = false;
            slowEnergyRise = false;
        }
        if (_ActiveRods >= 12 && _ActiveRods < 18)
        {
            EnergyProduction = EnergyProduction + (EnergyProduction / 20f);
            //SlowEnergyRise
            dynamicEnergyReduction = false;
            slowEnergyReduction = false;
            dynamicEnergyRise = false;
            slowEnergyRise = true;
        }
        else if (_ActiveRods >= 18 && _ActiveRods < 25)
        {
            EnergyProduction = EnergyProduction + (EnergyProduction / 10f);
            //DynamicEnergyRise
            dynamicEnergyReduction = false;
            slowEnergyReduction = false;
            dynamicEnergyRise = true;
            slowEnergyRise = false;
        }
        else if (_ActiveRods >= 25 && _ActiveRods < 30)
        {
            EnergyProduction = EnergyProduction + (EnergyProduction / 20f);
            //SlowEnergyRise
            dynamicEnergyReduction = false;
            slowEnergyReduction = false;
            dynamicEnergyRise = false;
            slowEnergyRise = true;
        }
        else if (_ActiveRods >= 30 && _ActiveRods < 34)
        {
            EnergyProduction = EnergyProduction + (EnergyProduction / 10f);
            //SlowEnergyReduction
            dynamicEnergyReduction = false;
            slowEnergyReduction = true;
            dynamicEnergyRise = false;
            slowEnergyRise = false;
        }
        else if (_ActiveRods >= 34)
        {
            EnergyProduction = EnergyProduction + (EnergyProduction / 10f);
            //DynamicEnergyReduction
            dynamicEnergyReduction = false;
            slowEnergyReduction = true;
            dynamicEnergyRise = false;
            slowEnergyRise = false;
        }
    }

}
