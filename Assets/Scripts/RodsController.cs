using UnityEngine;
public class RodsController : MonoBehaviour
{
    GameObject[] rods;
    public int ActiveRodsCount =0;
    void Start()
    {
        UploadRods();
    }
    private void Update()
    {
        ActiveRodsCount = 36;
        GameObject[] rodsTemp = GameObject.FindGameObjectsWithTag("Rod");
        for(int i = 0; i < rodsTemp.Length; i++)
        {
            if (!rodsTemp[i].GetComponent<RodCon>().Active)
            {
                ActiveRodsCount--;
            }
        }
    }
    void OnApplicationQuit()
    {
        SaveRods();
    }
    void UploadRods()
    {
        Debug.Log("Uploading rods...");
        rods = GameObject.FindGameObjectsWithTag("Rod");
        for(int i = 0; i < rods.Length; i++)
        {
            rods[i].gameObject.GetComponent<RodCon>().number = i;
            //bool rod status load (1 - true; 0 - false)
            if(PlayerPrefs.GetInt("Rod" + i) == 1) rods[i].gameObject.GetComponent<RodCon>().Active = true;
            else rods[i].gameObject.GetComponent<RodCon>().Active = false;
        }
    }
    void SaveRods()
    {
        Debug.Log("Saving rods...");
        rods = GameObject.FindGameObjectsWithTag("Rod");
        for (int i = 0; i < rods.Length; i++)
        {
            //bool rod status load (1 - true; 0 - false)
            if (rods[i].gameObject.GetComponent<RodCon>().Active == true) PlayerPrefs.SetInt("Rod" + i, 1);
            else PlayerPrefs.SetInt("Rod" + i, 0);
        }
    }
}
