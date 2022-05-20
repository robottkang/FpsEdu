using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip audioClipFire;
    public AudioClip audioClipReload;

    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float reloadTime;
    public float range;
    public float damage;
    public bool isAutomaticAttack;
}
