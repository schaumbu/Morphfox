using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Rigidbody _rgbd;

    [SerializeField]
    private float shotCooldownFix = 5f;
    [SerializeField]
    private float shotCooldown = 0.5f;
    private bool shotPossible = true;

    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody>();
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

                if (shotPossible == true && Input.GetKey(KeyCode.Mouse0))
                {
                    shotPossible = false;

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
                
        }
    }

