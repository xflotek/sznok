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
    

}
