using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip audioClipFire;   // 발사 오디오클립
    public AudioClip audioClipReload; // 재장전 오디오 클립

    public int maxAmmo;      // 탄창에 보관 가능 총알 수
    public int currentAmmo;  // 현재 총알 수
    public float fireRate;   // 발사 속도
    public float reloadTime; // 재장전 시간
    public float range;      // 사정거리
    public float damage;
    public bool isAutomaticAttack;     //자동 발사(연사) 여부
}
