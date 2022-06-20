using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUseWeapon : UseWeapon
{
    [SerializeField]
    private Gun gun;

    private void Update()
    {
        // 다음 시간에 구현
        //startFire = isPlayerTargeting;
        //stopFire  = !isPlayerTargeting;

        inputReload = AutoReload();
    }

    private bool AutoReload()
    {
        if(gun.currentAmmo > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
