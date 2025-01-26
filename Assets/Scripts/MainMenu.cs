using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
   public void NewGame()
    {
        panel.SetActive(true);
    }
    public void NewGameConfirm()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartAnimationScene");
    }
    public void NewGameCancel()
    {
        panel.SetActive(false);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("StartAnimationScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
