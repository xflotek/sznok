using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 m_Target;
    public float m_Speed;
    private Vector2 m_Direction;
    private Rigidbody2D m_Rb;
    
    void Start()
    {
        m_Direction = m_Target - (Vector2)transform.position;
        m_Direction.Normalize();
        m_Rb = GetComponent<Rigidbody2D>();
        m_Rb.linearVelocity = m_Direction * m_Speed;
    }
}
