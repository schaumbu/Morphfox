using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    private Vector3 _dir = Vector3.zero;
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Rigidbody rig;
    [SerializeField]
    private Rigidbody Projectile;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Projectile = GetComponent<Rigidbody>();
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _dir.x = 0;
        _dir.z = 0;

        AimDirection = Vector3.zero;
        Target = Input.mousePosition;
        Vector3 fromCenter = transform.position - Vector3.zero;
        Quaternion rot = Quaternion.LookRotation(-fromCenter,-transform.forward) * Quaternion.Euler(90,0,0);
       // transform.up = -fromCenter;
       // transform.rotation *= Quaternion.AngleAxis(0, -fromCenter);
        Vector2 mov;
        mov.x = Input.GetAxis("Horizontal");
        mov.y = Input.GetAxis("Vertical");

        var rotMov = rot * new Vector3(mov.x, 0, mov.y);
        rig.AddForce(rotMov * speed);
        



        /*

        if (Input.GetKey(KeyCode.W))
            _dir.z += 80;
        if (Input.GetKey(KeyCode.S))
            _dir.z -= 80;
        if (Input.GetKey(KeyCode.A))
            _dir.x -= 80;
        if (Input.GetKey(KeyCode.D))
            _dir.x += 80;
        if (Input.GetKey(KeyCode.Space))
            _dir.y += 80;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 ShootDirection;
            ShootDirection = Input.mousePosition;
            ShootDirection.z = 0.0f;
            ShootDirection = Camera.main.ScreenToWorldPoint(ShootDirection);
            ShootDirection.z = 0.0f;
            ShootDirection = (ShootDirection - transform.position).normalized;
            ShootDirection.z = 0.0f;

            GameObject curProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(ShootDirection.y, ShootDirection.x) * Mathf.Rad2Deg - 90));
            curProjectile.GetComponent<Shot>().ProjectileDirection = new Vector2(ShootDirection.x + Random.Range(0, 11) / 20f - 0.25f, ShootDirection.y + Random.Range(0, 11) / 20f - 0.5f).normalized;
        }

        _dir.Normalize();
        _rgbd.velocity = _dir * speed * Time.deltaTime;
        */
       

        rig.rotation = rot;

    }
    private void FixedUpdate()
    {
        Vector3 fromCenter = transform.position - Vector3.zero;
        rig.AddForce(fromCenter.normalized * 9.81f);
    }
}
