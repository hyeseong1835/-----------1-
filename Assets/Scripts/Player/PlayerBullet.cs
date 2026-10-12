using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [NonSerialized] public PlayerBulletManager bulletManager;
    [NonSerialized] public Action<Enemy> onHit;
    [NonSerialized] public float lifeTime = 5f;
    float speed;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            bulletManager.ReturnBullet(this);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy entity))
        {
            onHit?.Invoke(entity);
            bulletManager.ReturnBullet(this);
        }
    }
    public void SetMoveDelta(Vector2 delta)
    {
        this.speed = delta.magnitude;
        this.transform.right = delta;
    }
    public void SetMoveDelta(float deg, float speed)
    {
        this.speed = speed;
        this.transform.rotation = Quaternion.Euler(0, 0, deg);
    }
}