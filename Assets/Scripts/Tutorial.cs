using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Tutorial : MonoBehaviour
{
    public GameObject First;
    public GameObject Second;
    public GameObject Third;
    public GameObject Fourth;
    //пизда
    private void Start() 
    {
    Time.timeScale = 0f;
    }
    public void two()
    {
    First.SetActive(false);
    Second.SetActive(true);
    }
    public void three()
    {
    Second.SetActive(false);
    Third.SetActive(true);
    }
    public void four()
    {
    Third.SetActive(false);
    Fourth.SetActive(true);
    }
    public void five()
    {
    SceneManager.LoadScene("MainMenu");
    }
}
