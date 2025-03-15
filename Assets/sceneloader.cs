using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{

   public void loadstartscene(){
    SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

   public void loadmenuscene(){
    SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    

    public void loadlvl1(){
        SceneManager.LoadScene("lvl1");
    }
    public void loadlvl2(){
        SceneManager.LoadScene("lvl2");
    }
    public void loadlvl3(){
        SceneManager.LoadScene("lvl3");
    }public void loadkoniec(){
        SceneManager.LoadScene("end");
    }
  
    public void quit()
    {
        Application.Quit();
    }

}
