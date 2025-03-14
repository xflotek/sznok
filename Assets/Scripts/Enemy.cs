using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float attack_cooldown = 40.5f;

    private float attack_timer = 0f;

    private Rigidbody2D rg;
    private GameObject player;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        attack_timer += Time.deltaTime;
        Move();
        Attack();
    }

    private void Attack()
    {
        if (rg.linearVelocity == new Vector2(0f, 0f) && attack_timer > attack_cooldown)
        {
            attack_timer = 0f;
            print("Attack");

            //player.GetComponent</*Nazwa skryptu*/>().ReciveDamage(damage);
        }
    }

    public void ReciveDamage(float damage)
    {
        health -= damage;
        if (health < 0f)
        {
            Destroy(rg.gameObject);
        }
    }

    private void Move()
    {
        Vector3 player_pos = player.transform.position;
        Vector3 rg_pos = rg.transform.position;

        Vector2 vel = new Vector2(Stop_vel(player_pos.x - rg_pos.x), Stop_vel(player_pos.y - rg_pos.y)).normalized;

        vel *= new Vector2(speed, speed);
        rg.linearVelocity = vel;
    }

    private float Stop_vel(float vel)
    {
        if(!(Mathf.Abs(vel) > range))
        {
            return 0f;
        }
        return vel;
    }
}
