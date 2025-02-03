using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
   public void NewGame()
    {
        panel.SetActive(true);
    }
    public void NewGameConfirm()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainScene");
    }
    public void NewGameCancel()
    {
        panel.SetActive(false);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
