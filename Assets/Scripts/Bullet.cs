using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 m_Target;
    public float m_Speed;
    public float m_Damage;

    [SerializeField]
    private GameObject m_ParticleExplosionReference;

    [SerializeField]
    private float m_MaximumLifetime = 4f;

    private float m_LifetimeTimer;

    private Vector2 m_Direction;
    private Rigidbody2D m_Rb;

    void Start()
    {
            m_Direction = m_Target - (Vector2)transform.position;
            m_Direction.Normalize();
            m_Rb = GetComponent<Rigidbody2D>();
            m_Rb.linearVelocity = m_Direction * m_Speed;
            m_LifetimeTimer = 0;
    }

    void Update()
    {
        m_LifetimeTimer += Time.deltaTime;
        if (m_LifetimeTimer >= m_MaximumLifetime) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) {
            Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(m_Damage);
        }

        Instantiate(m_ParticleExplosionReference, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
