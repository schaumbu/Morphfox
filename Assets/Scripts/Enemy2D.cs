using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{

[SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private Rigidbody2D _rgbd;
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private Rigidbody2D _eProjectile;

    [SerializeField]
    private float shotCooldownFix = 2f;
    [SerializeField]
    private float shotCooldown = 0.5f;
    private bool shotPossible = true;
    [SerializeField]
    private Transform following;

    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        shotPossible = true;
    }

    // Update is called once per frame
    void Update()
    {

        AimDirection = Vector3.zero;
        Target = Input.mousePosition;

        if (shotPossible == false)
            shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0.0f)
        {
            shotPossible = true;
            shotCooldown = shotCooldownFix;
        }


        if (shotPossible == true)
        {
            shotPossible = false;
            Vector2 ShootDirection;
            ShootDirection = following.transform.position - transform.position;
           
            GameObject curProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            curProjectile.GetComponent<Shot>().ProjectileDirection = ShootDirection.normalized;

        }
    }
}
