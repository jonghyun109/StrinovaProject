using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HELLSLAYERCrosshairs
{
    public class Gun : MonoBehaviour
    {
        public Crosshair crosshair; 
        public float gunRecoil;
        public float settleSpeed;
        public float shotsPerSecond;

        float shotRate;
        float nextShotTime;

        void Start()
        {
            crosshair.SetShrinkSpeed(settleSpeed);
            shotRate = 1.0f / shotsPerSecond;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            if (nextShotTime < Time.time)
            {
                crosshair.Expand(gunRecoil);
                nextShotTime = Time.time + shotRate;
            }

        }
    }
}
