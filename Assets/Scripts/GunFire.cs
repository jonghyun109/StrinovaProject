using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class GunFire : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    public Camera maincam;
    public GameObject bulletImpactPrefab;
    public GameObject enemyHitEffectPrefab;

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
            //Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
            var particle = _pool.Get();
            particle.Shoot(muzzlePosition.transform.position);
            ShootRayFromCamera();
            ApplyRecoil();
        }
    }
    //** 반동 적용 함수 추가 **//
    void ApplyRecoil()
    {
        float recoilAmount = 1f; // 기본 반동 크기
        float aimRecoilAmount = 0.3f; // 정조준 시 반동 감소

        if (Camera.main != null)
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();
            if (camController != null)
            {
                bool isAiming = Input.GetMouseButton(1);
                camController.ApplyCameraRecoil(isAiming ? aimRecoilAmount : recoilAmount);
            }
        }
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
            if (hit.collider.CompareTag("StartTarget"))
            {
                StartButton.SetActive(false);
                Debug.Log("게임 시작");
                hit.collider.GetComponent<GameStarter>().StartGame();
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHead"))
            {
                Debug.Log("헤드샷");
                hit.collider.GetComponentInParent<Enemy>().TakeDamage(true);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
            {
                Debug.Log("바디샷");
                hit.collider.GetComponentInParent<Enemy>().TakeDamage(false);
            }
           
            if (hit.collider.CompareTag("Enemy"))
            {
                if (enemyHitEffectPrefab != null)
                {
                    GameObject hitEffect = Instantiate(enemyHitEffectPrefab, hit.point, Quaternion.identity);
                    Destroy(hitEffect, 2f);
                }
            }
            else if (bulletImpactPrefab != null)
            {
                GameObject impact = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.point));
                Destroy(impact, 2f);
            }
            
        }
        else
        {
            lastShotEnd = ray.origin + ray.direction * 100f;
            shotDir = ray.direction; 
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
