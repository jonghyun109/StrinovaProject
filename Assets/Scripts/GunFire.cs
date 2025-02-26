using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GunFire : MonoBehaviour
{
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    public float shootDelay = 0.5f;

    public GameObject scope;
    public bool scopeAction = true;

    [SerializeField] private float timeLastFired;

    private IObjectPool<Bullet> _pool;
    

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
}
