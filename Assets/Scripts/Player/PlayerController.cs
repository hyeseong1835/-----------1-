using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] Animator animator;
    Rigidbody2D rigidbody;

    float hp;
    [SerializeField] float maxHp;

    [SerializeField] float moveSpeed = 5f;

    PlayerBulletManager bulletManager;

    [SerializeField] float shootCooldown = 0.1f;
    float shootTimer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        this.bulletManager = PlayerBulletManager.Instance;
        this.hp = maxHp;
    }
    void Update()
    {
        Vector2 dir = UserInput.moveInput;
        if (dir.magnitude > 0)
        {
            if (dir.x > 0.5)
            {
                this.animator.SetInteger("Direction", 2);
            }
            else if (dir.x < -0.5)
            {
                this.animator.SetInteger("Direction", 3);
            }
            else if (dir.y > 0.5)
            {
                this.animator.SetInteger("Direction", 1);
            }
            else if (dir.y < -0.5)
            {
                this.animator.SetInteger("Direction", 0);
            }

            this.animator.SetBool("IsMoving", true);
        }
        else
        {
            this.animator.SetBool("IsMoving", false);
        }
        
        this.rigidbody.linearVelocity = moveSpeed * dir;

        if (UserInput.shootInput && this.shootTimer <= 0)
        {
            Shoot();
        }
        else
        {
            this.shootTimer -= Time.deltaTime;
        }
    }
    void Shoot()
    {
        PlayerBullet bullet = bulletManager.GetBullet();
        bullet.transform.position = transform.position;
        bullet.onHit = (enemy) =>
        {
            enemy.TakeDamage(1);
        };
        Vector2 dir = (UserInput.mouseWorldPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        bullet.SetMoveDelta(dir * 10f);
        bullet.lifeTime = 5f;

        shootTimer = shootCooldown;
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
        Debug.Log("Player Died");
        CameraManager.Instance.MainCamera.transform.SetParent(null);
        GameObject.Destroy(gameObject);
    }
}
