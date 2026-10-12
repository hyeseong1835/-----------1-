using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    public static PlayerBulletManager Instance { get; private set; }

    [SerializeField] Pool bulletPool;
    [SerializeField] PlayerBullet bulletPrefab;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public PlayerBullet GetBullet()
    {
        PlayerBullet bullet = bulletPool.Get().GetComponent<PlayerBullet>();
        bullet.bulletManager = this;
        return bullet;
    }
    public void ReturnBullet(PlayerBullet bullet)
    {
        bulletPool.Return(bullet.gameObject);
    }
}