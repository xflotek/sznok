using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 m_Target;
    public float m_Speed;
    public float m_Damage;

    [SerializeField]
    private GameObject m_ParticleExplosionReference;

    [SerializeField]
    private float m_SpawnDelay = 0.2f;
    private float m_Timer;

    private Vector2 m_Direction;
    private Rigidbody2D m_Rb;

    private bool m_Fired;

    void Start()
    {
        m_Timer = 0;
        m_Fired = false;
        gameObject.transform.localScale = new Vector3(0,0,0);
    }

    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SpawnDelay && !m_Fired) {
        gameObject.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
            m_Direction = m_Target - (Vector2)transform.position;
            m_Direction.Normalize();
            m_Rb = GetComponent<Rigidbody2D>();
            m_Rb.linearVelocity = m_Direction * m_Speed;
            m_Fired = true;
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
