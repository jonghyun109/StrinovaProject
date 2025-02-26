using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class GunFire : MonoBehaviour
{
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    public Camera maincam;

    public float shootDelay = 0.5f;
    public float bulletSpeed = 20f; // 탄속 추가
    
    private Vector3 lastShotStart;
    private Vector3 lastShotEnd;
    Vector3 shotDir;

    public GameObject scope;
    public bool scopeAction = true;

    [SerializeField] private float timeLastFired;

    private IObjectPool<Bullet> _pool;

    Ray ray;
    private void Awake()
    {
        _pool = new ObjectPool<Bullet>
            (CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet,maxSize:20);
    }

    private Bullet CreateBullet()
    {        
        Bullet bullet = Instantiate(muzzlePrefab, muzzlePosition.transform).GetComponent<Bullet>();
        if (bullet == null)
        {
            Debug.LogError("Bullet null");
            return null;
        }
        bullet.SetPool(_pool);
        return bullet;
    }
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    void Start()
    {
        timeLastFired = 0;

        maincam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButton(0)&& ((timeLastFired + shootDelay) <= Time.time))
        {
            timeLastFired = Time.time;
            //FireWeapon();
            //Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
            var particle = _pool.Get();
            particle.Shoot(muzzlePosition.transform.position);
            ShootRayFromCamera();
        }

    }
    public void FireWeapon()
    {
        // --- Keep track of when the weapon is being fired ---
        

        // --- Spawn muzzle flash ---
        //var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

        // --- Shoot Projectile Object ---       
        GameObject newProjectile = Instantiate(muzzlePrefab,muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);        
    }
    private void ShootRayFromCamera()
    {
        Ray ray = maincam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        lastShotStart = ray.origin;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lastShotEnd = hit.point;
            shotDir = (hit.point - muzzlePosition.transform.position).normalized;
        }
        else
        {
            lastShotEnd = ray.origin + ray.direction * 100f;
            shotDir = ray.direction; // 아무것도 맞지 않으면 기본 카메라 방향으로 발사
        }
    }

    private void OnDrawGizmos()
    {
        if (lastShotStart != Vector3.zero && lastShotEnd != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastShotStart, lastShotEnd);
            Gizmos.DrawSphere(lastShotEnd, 0.2f);
        }
    }
}
