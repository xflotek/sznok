using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public GameObject pause;
    public bool ispaused;
    void Start()
    {
        pause.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (ispaused)
            {
                resumegame();
            }
            else
            {
                pausegame();
            }
        }
    }

    public void pausegame()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
    }
    public void resumegame()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
        ispaused = false;
    }

    public void restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
