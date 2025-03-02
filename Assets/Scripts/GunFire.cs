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

    public GameObject bulletImpactPrefab; //벽 용)
    public GameObject enemyHitEffectPrefab; // 적 전용

    public float shootDelay = 0.5f;
    public float bulletSpeed = 20f;

    private Vector3 lastShotStart;
    private Vector3 lastShotEnd;
    Vector3 shotDir;

    public GameObject scope;
    public bool scopeAction = true;

    [SerializeField] private float timeLastFired;

    GameManager gameManager;
    private IObjectPool<Bullet> _pool;

    Ray ray;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        _pool = new ObjectPool<Bullet>
            (CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 20);
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
        if (Input.GetMouseButton(0) && ((timeLastFired + shootDelay) <= Time.time))
        {
            timeLastFired = Time.time;
            var particle = _pool.Get();
            particle.Shoot(muzzlePosition.transform.position);
            ShootRayFromCamera();
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
                gameManager.StartGame();
                if (StartButton != null)
                {
                    StartButton.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHead")) // 🆕 헤드샷 판정
            {
                HandleEnemyHit(hit, true);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EnemyBody")) // 🆕 바디샷 판정
            {
                HandleEnemyHit(hit, false);
            }
            else // 적이 아닌 표면 (땅, 벽 등) 맞음
            {
                if (bulletImpactPrefab != null)
                {
                    GameObject impact = Instantiate(bulletImpactPrefab, hit.point, Quaternion.identity);
                    Destroy(impact, 2f);
                }
            }
        }
        else
        {
            lastShotEnd = ray.origin + ray.direction * 100f;
            shotDir = ray.direction;
        }
    }

    // 적 맞았을 때 처리하는 함수 (헤드샷 / 바디샷 구분)
    private void HandleEnemyHit(RaycastHit hit, bool isHeadshot)
    {
        Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(isHeadshot);

            // 적 맞았을 때만 피격 이펙트 실행
            if (enemyHitEffectPrefab != null)
            {
                GameObject hitEffect = Instantiate(enemyHitEffectPrefab, hit.point, Quaternion.identity);
                Destroy(hitEffect, 2f);
            }
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
