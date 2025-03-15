using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickable : MonoBehaviour
{
    [SerializeField] private bool glass;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().pickable = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().pickable = null;
        }
    }

    public bool PickUp()
    {
        if (glass)
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
            return true;
        }
        else
        {
            Destroy(gameObject);
            print("textbox");
            return true;
        }
    }
}
