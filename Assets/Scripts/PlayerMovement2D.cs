using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    private Vector2 _dir = Vector2.zero;
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;

    [SerializeField]
    private GameObject _player; 
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Rigidbody _rgbd;
    [SerializeField]
    private Rigidbody Projectile;
    [SerializeField]
    private float speed;
    private Vector3 CursorPos;
    private Vector3 ObjectPos;
    private float angle;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        _dir = transform.up;
        Projectile = GetComponent<Rigidbody>();
        if (!_rgbd)
        {
            _rgbd = GetComponent<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        _dir.x = 0;
        _dir.y = 0;

        CursorPos = Input.mousePosition;
        ObjectPos = Camera.main.WorldToScreenPoint(target.position);
        CursorPos.x = CursorPos.x - CursorPos.x;
        CursorPos.y = CursorPos.y - CursorPos.y;
        angle = Mathf.Atan2(CursorPos.y, CursorPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(Vector2(0, 0, angle));
        transform.LookAt(ObjectPos);
        AimDirection = Vector3.zero;
        Target = Input.mousePosition;

        if (Input.GetKey(KeyCode.W))
            _dir.y += 80;
        if (Input.GetKey(KeyCode.S))
            _dir.y -= 80;
        if (Input.GetKey(KeyCode.A))
            _dir.x -= 80;
        if (Input.GetKey(KeyCode.D))
            _dir.x += 80;
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

    }
}
