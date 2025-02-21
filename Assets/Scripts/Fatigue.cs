using System;
using UnityEngine;
using UnityEngine.UI;

public class Fatigue : MonoBehaviour
{
    public float multiplier = 1;
    public int MaxFatigueIndex = 10;
    public int FatigueIndex = 10;
    [SerializeField] private Animator _anim;
    private Image _image;
    void Start()
    {
        _image = GameObject.Find("Image").GetComponent<Image>();
        InvokeRepeating("CheckDark",0,5);
    }
    void Update()
    {
        float i = FatigueIndex;
        _image.fillAmount = i / MaxFatigueIndex;
    }
    void CheckDark()
    {
        if(FatigueIndex <=0)
        {
            _anim.SetTrigger("Dark");
        }
    }
}
