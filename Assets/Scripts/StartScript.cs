using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
public float delay = 5f;
float timer;
GlobalData data;
    private void Start()
    {
        data = new GlobalData();
        data.LoadFromPrefs();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
