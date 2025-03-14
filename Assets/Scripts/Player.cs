using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    private Vector3 m_MoveLeft = new(-1, 1, 1);
    private Vector3 m_MoveRight = new(1, 1, 1);
    private InputAction m_MoveAction;
    private Rigidbody2D m_Rb;
    private Vector2 m_VecMvmt;
    private Weapon m_Weapon;
    private bool m_ShouldFire;
    private Animator m_Animator;

    public float m_LastDirection;
    public GameObject pickable;
    
    public static UnityEvent onPlayerDeath;

    public void ReceiveDamage(float damage) {
        if (m_Health - damage > 0) {
            m_Health -= damage;
        } else {
            m_Health = 0;
            onPlayerDeath.Invoke();
        }
    }

    void Start()
    {
        m_MoveAction = InputSystem.actions.FindAction("move");
        m_Health = m_MaxHealth;
        m_Rb = GetComponent<Rigidbody2D>();
        m_Weapon = GetComponent<Weapon>();
        m_ShouldFire = false;
        onPlayerDeath ??= new();
        m_Animator = GetComponent<Animator>();
        transform.localScale = m_DefaultScale;
    }

    void Update()
    {
        m_VecMvmt = m_MoveAction.ReadValue<Vector2>();

        if (m_VecMvmt.x != 0 || m_VecMvmt.y != 0) {
            m_Animator.SetBool("isMoving", true);

        } else {
            m_Animator.SetBool("isMoving", false);
        }

        if (Keyboard.current.fKey.ReadValue() > 0) {
            m_ShouldFire = true;
        }

        if (Keyboard.current.qKey.ReadValue() > 0)
        {
            if (pickable != null)
            {
                if (pickable.GetComponent<Pickable>().PickUp())
                {
                    pickable = null;
                }
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
}
