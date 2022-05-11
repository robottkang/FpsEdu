using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    [SerializeField]
    LayerMask enemy;
    [SerializeField]
    GameObject eyesOfObject;
    float lastFireTime;
    bool startFire;
    bool stopFire;
    protected bool isFire;
    protected bool isReload;
    protected bool reload;

    protected void WeaponAction(Gun _gun)
    {
        if(startFire)
        {
            isFire = true;
            StartCoroutine(GunAction(_gun));
        }
        else if (stopFire)
        {
            isFire = false;
        }
        if(reload && !isReload)
        {
            StartCoroutine(Reload(_gun));
        }
    }

    private IEnumerator GunAction(Gun _gun)
    {
        while (isFire && !isReload)
        {
            if(_gun.currentAmmo > 0)
            {
                Fire(_gun);
            }
            yield return null;
        }
        yield return null;
    }

    private void Fire(Gun _gun)
    {
        if(Time.time - lastFireTime > _gun.fireRate)
        {
            _gun.currentAmmo--;
            Hit(_gun.damage, _gun.range);
            lastFireTime = Time.time;
        }
    }

    private void Hit(float damage, float range)
    {
        if (Physics.Raycast(eyesOfObject.transform.position, eyesOfObject.transform.forward, out RaycastHit hitObject, range, enemy))
        {
            hitObject.transform.GetComponent<ObjectInfo>().healthPoint -= damage;
        }
    }

    private IEnumerator Reload(Gun _gun)
    {
        isReload = true;
        float _reloadTime = _gun.reloadTime;
        while (_reloadTime > 0)
        {
            _reloadTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _gun.currentAmmo = _gun.maxAmmo;
        isReload = false;
    }
}
