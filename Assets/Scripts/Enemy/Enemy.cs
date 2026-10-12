using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public Pool pool;
    float hp;
    [SerializeField] float maxHp;

    PlayerController playerController;
    [SerializeField] float moveSpeed = 1f;

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        playerController = PlayerController.Instance;
    }
    public void Init()
    {
        this.hp = maxHp;
    }
    void Update()
    {
        if (playerController == null)
            return;
            
        Vector2 dir = (playerController.transform.position - transform.position).normalized;
        rigidbody.linearVelocity = dir * moveSpeed;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        pool.Return(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.TakeDamage(1);
            Die();
        }
    }
}