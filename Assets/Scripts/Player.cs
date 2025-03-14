using UnityEngine;
using UnityEngine.InputSystem;
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
    
    void Start()
    {
        m_MoveAction = InputSystem.actions.FindAction("move");
        m_Health = m_MaxHealth;
        m_Rb = GetComponent<Rigidbody2D>();
        m_Weapon = GetComponent<Weapon>();
        m_ShouldFire = false;
    }

    void Update()
    {
        m_VecMvmt = m_MoveAction.ReadValue<Vector2>();
        if (Keyboard.current.fKey.ReadValue() > 0) {
            m_ShouldFire = true;
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
