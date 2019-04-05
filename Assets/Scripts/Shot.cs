using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;
    public Vector2 ProjectileDirection = Vector2.zero;
    [SerializeField]
    private float speed;

    private Rigidbody2D _rgbd;

    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        speed = 20;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            Object.Destroy(Projectile);
        if (collision.gameObject.tag == "Floor")
            Object.Destroy(Projectile);
        if (collision.gameObject.tag == "Enemy")
        {
            Object.Destroy(Projectile);
            //EnemyCondition hp = collision.gameObject.GetComponent<EnemyCondition>();
            //hp.TakeDamage(10f);
        }

        if (collision.gameObject.tag == "Ceiling")
            Object.Destroy(Projectile);

    }

    // Update is called once per frame
    void Update()
    {
        _rgbd.velocity = ProjectileDirection * speed;
    }
}

