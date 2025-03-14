using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private GameObject m_WeaponTarget;

    [SerializeField]
    GameObject m_Bullet;

    [SerializeField]
    private float m_WeaponForce = 30f;
    
    [SerializeField]
    private float m_FireRate = 1f;

    [SerializeField]
    private float m_WeaponDamage = 1f;

    [SerializeField]
    private Vector2 m_SpawnOffset = new(0f, 1.2f);

    private float m_WeaponCooldown;
    private float m_CooldownTimer;

    public bool m_CanFire;
    public static UnityEvent onPlayerWeaponFire;

    public bool Fire(Transform origin) {
        if (!m_CanFire) {
            return false;
        }
        onPlayerWeaponFire.Invoke();

        GameObject b = Instantiate(
            m_Bullet,
            origin.position + (Vector3) m_SpawnOffset,
            Quaternion.identity
        );

        Bullet b_comp = b.GetComponent<Bullet>();

        b_comp.m_Speed  = m_WeaponForce;
        b_comp.m_Target = m_WeaponTarget.transform.position;
        b_comp.m_Damage = m_WeaponDamage;

        m_CanFire = false;
        m_CooldownTimer = 0;

        return true;
    }
    
    void Start()
    {
        onPlayerWeaponFire ??= new();
        m_WeaponTarget = GameObject.FindWithTag("WeaponTarget");
        m_WeaponCooldown = 100 / m_FireRate;
        m_CooldownTimer = 0;
        m_CanFire = true;
    }

    void Update()
    {
        Vector2 mouseCoords = new (Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
        m_WeaponTarget.transform.position = Camera.main.ScreenToWorldPoint(mouseCoords);

        m_CooldownTimer += Time.deltaTime;

        if (m_CooldownTimer >= m_WeaponCooldown) {
            m_CanFire = true;
            m_CooldownTimer = 0;
        }

    }
}
