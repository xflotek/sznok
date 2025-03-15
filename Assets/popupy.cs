using UnityEngine;

public class popupy : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;

    public void close1()
    {
        popup1.SetActive(false);
        Time.timeScale = 1f;
    }
    public void close2()
    {
        popup2.SetActive(false);
        Time.timeScale = 1f;
    }
    public void close3(){
        popup3.SetActive(false);
        Time.timeScale = 1f;
    }
    public void close4(){
        popup4.SetActive(false);
        Time.timeScale = 1f;
    }

}
