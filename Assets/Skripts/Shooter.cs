using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public int curBullet = 30;

    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject prefabBullet;

    public void OnShoot()
    {
        if (curBullet > 0)
        {
            curBullet--;
            GameObject projectile = Instantiate(prefabBullet, shootPos.position, prefabBullet.transform.rotation);
            Vector3 origScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3
                (
                origScale.x * transform.localScale.x > 0 ? 1 : -1,
                origScale.y,
                origScale.z
                );
        }
    }
}
