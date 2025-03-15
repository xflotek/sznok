using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_Health = 100f;
    [SerializeField]
    private float m_MaxHealth = 100f;
    [SerializeField] 
    private float m_MovementSpeed = 1f;
    [SerializeField]
    private Vector3 m_DefaultScale = new(1.5f, 1.5f, 1f);
    [SerializeField]
    private GameObject m_HealthBar;
    private float m_DodgeDelay = 2f;

    private Vector3 m_MoveLeft = new(-1, 1, 1);
    private Vector3 m_MoveRight = new(1, 1, 1);

    private InputAction m_MoveAction;
    private InputAction m_DodgeAction;
    private InputAction m_PickUpAction;
    private InputAction m_ShootAction;

    private Rigidbody2D m_Rb;
    private Vector2 m_VecMvmt;
    private Weapon m_Weapon;
    private bool m_ShouldFire;
    private Animator m_Animator;
    private float m_DodgeTimer;
    private bool m_CanDodge;

    public float m_LastDirection;
    public GameObject pickable;
    
    public static UnityEvent onPlayerDeath;

    public bool isInvincible;
    
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;

    public string scena = "";

    public void ReceiveDamage(float damage) {
        if (!isInvincible) {
            if (m_Health - damage > 0) {
                m_Health -= damage;
            } else {
                m_Health = 0;
                onPlayerDeath.Invoke();
            }
        }
        m_HealthBar.GetComponent<HealthBar>().curr = (int)m_Health;
    }

    void Start()
    {
        m_MoveAction   = InputSystem.actions.FindAction("Move");
        m_PickUpAction = InputSystem.actions.FindAction("PickUp");
        m_DodgeAction  = InputSystem.actions.FindAction("Dodge");
        m_ShootAction  = InputSystem.actions.FindAction("Shoot");


        m_Health = m_MaxHealth;
        m_Rb = GetComponent<Rigidbody2D>();
        m_Weapon = GetComponent<Weapon>();
        m_ShouldFire = false;
        onPlayerDeath ??= new();
        m_Animator = GetComponent<Animator>();
        transform.localScale = m_DefaultScale;
        isInvincible = false;
        m_CanDodge = true;
        m_DodgeTimer = 0;
    }

    void Update()
    {
        m_DodgeTimer += Time.deltaTime;

        if (isInvincible && m_DodgeTimer >= (m_DodgeDelay / 4) ) {
            isInvincible = false;
        }

        if (m_DodgeTimer >= m_DodgeDelay) {
            m_DodgeTimer = 0;
            m_CanDodge = true;
        }

        m_VecMvmt = m_MoveAction.ReadValue<Vector2>();

        if (m_VecMvmt.x != 0 || m_VecMvmt.y != 0) {
            m_Animator.SetBool("isMoving", true);

        } else {
            m_Animator.SetBool("isMoving", false);
        }

        if (m_ShootAction.ReadValue<float>() > 0) {
            m_ShouldFire = true;
        }

        if (m_PickUpAction.ReadValue<float>() > 0)
        {
            if (pickable != null)
            {
                if (pickable.GetComponent<Pickable>().PickUp())
                {
                    pickable = null;
                }
            }
        }

        if (m_DodgeAction.ReadValue<float>() > 0) {
            if (m_CanDodge) {
                isInvincible = true;
                m_Animator.SetTrigger("Dodge");
                m_CanDodge = false;
                m_DodgeTimer = 0;
            }
        }

        if (m_VecMvmt.x > 0) {
            transform.localScale = new Vector3(
                m_DefaultScale.x * m_MoveRight.x,
                m_DefaultScale.y * m_MoveRight.y,
                m_DefaultScale.z * m_MoveRight.z
            );
        }

        if (m_VecMvmt.x < 0) {
            transform.localScale = new Vector3(
                m_DefaultScale.x * m_MoveLeft.x,
                m_DefaultScale.y * m_MoveLeft.y,
                m_DefaultScale.z * m_MoveLeft.z
            );
        }
    }

    void FixedUpdate()
    {
        m_Rb.linearVelocity += m_VecMvmt * m_MovementSpeed;

        if (m_ShouldFire) {
            
            if (m_Weapon.Fire(transform)) {
                m_Animator.SetTrigger("Shoot");
            }

            m_ShouldFire = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
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
