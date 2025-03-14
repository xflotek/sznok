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
    private InputAction m_MoveAction;
    private Rigidbody2D m_Rb;
    private Vector2 m_VecMvmt;
    private Weapon m_Weapon;
    private bool m_ShouldFire;
    public float m_LastDirection;
    public GameObject pickable;

    public static UnityEvent onPlayerDeath;

    public void ReceiveDamage(float damage) {
        if (m_Health - damage > 0) {
            m_Health -= damage;
        } else {
            m_Health = 0;
            onPlayerDeath.Invoke();
            Debug.Log("Death");
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
    }

    void Update()
    {
        m_VecMvmt = m_MoveAction.ReadValue<Vector2>();
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
    }

    void FixedUpdate()
    {
        m_Rb.linearVelocity += m_VecMvmt * m_MovementSpeed;
        if (m_ShouldFire) {
            m_Weapon.Fire(transform);
            m_ShouldFire = false;
        }
    }
}
