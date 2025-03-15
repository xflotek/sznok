using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mcscript : MonoBehaviour
{

    public float movespeed = 5f;
    private Rigidbody2D rb;
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;
    public string scena = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(movehorizontal * movespeed, movevertical * movespeed); 
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //movespeed = 0f;
        Time.timeScale = 0f;

        if (other.gameObject.CompareTag("lvl1"))
        {
            popup1.SetActive(true);
            scena = "lvl1";
        }
        else if (other.gameObject.CompareTag("lvl2"))
        {
            popup2.SetActive(true);
            scena = "lvl2";
        }
        else if (other.gameObject.CompareTag("lvl3"))
        {
            popup3.SetActive(true);
            scena = "lvl3";
        }
        else if (other.gameObject.CompareTag("koniec"))
        {
            popup4.SetActive(true);
            scena = "koniec";
        }
    }

}
