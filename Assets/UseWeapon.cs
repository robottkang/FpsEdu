using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource; // 오디오 출력

    [SerializeField]
    private GameObject hitMark;        // 피격 판정을 보여주기 위한 게임 오브젝트로 필수는 아님
    [SerializeField]
    private GameObject eyesOfObject;   // 총 발사 시점
    protected RaycastHit collidertHit; // 피격된 오브젝트의 정보
    public LayerMask targetLayerMask;  // 사격 대상의 레이어마스크
    private float lastFireTime;        // 마지막 발사 시간
    protected bool isReload = false;   // 장전 중인가
    protected bool inputReload;        // 장전 키코드를 눌렀는가
    protected bool startFire;          // 발사를 시작할 것인가
    protected bool stopFire;           // 발사를 정지할 것인가

    
    protected void WeaponAction(Gun _gun) // 총기의 발사, 재장전을 수행
    {
        if (!isReload) // 장전 중이 아니라면
        {
            if (startFire)
            {
                StartCoroutine("GunAction", _gun);
            }
            else if (stopFire)
            {
                StopCoroutine("GunAction");
            }
            if (inputReload)
            {
                StartCoroutine(OnReload(_gun));
            }
        }
    }
    // 코루틴에 대하여 https://notyu.tistory.com/62


    private IEnumerator GunAction(Gun _gun) // 총기의 발사 조건들의 판단를 수행
    {
        if (_gun.currentAmmo > 0)
        {
            if (_gun.isAutomaticAttack) // 총기의 자동공격(연사)가 true라면
            {
                while (_gun.currentAmmo > 0 && !isReload) // 총기가 장전중이 아니고 남은 탄이 0 보다 큰 동안 반복
                {
                    Fire(_gun);
                    yield return null;  // 코루틴은 반드시 반환값이 있어야 함으로 null 값이라도 반환하여 줍시다 
                }
            }
            else Fire(_gun);
        }
        yield return null;
    }

    private void Fire(Gun _gun) // 총기의 발사를 수행
    {
        if (Time.time - lastFireTime > _gun.fireRate) // 현재의 시간 - 마지막 발사 시간 = 발사 후 흐른 시간
        {
            Debug.Log("Fire");
            _gun.currentAmmo--;
            Hit(_gun.damage, _gun.range);
            lastFireTime = Time.time;      // 마지막 발사 시간을 담음
            PlaySound(_gun.audioClipFire); // 총기의 발사 사운드클립을 출력
        }
    }

    private void Hit(float _damage, float _range)
    {
        if (Physics.Raycast(eyesOfObject.transform.position, eyesOfObject.transform.forward, out collidertHit, _range, targetLayerMask))
        {
            collidertHit.transform.GetComponent<ObjectInfo>().healthPoint -= _damage;              // 피격된 오브젝트의 ObjectInfo안에 있는 체력을 대미지 만큼 감소
            Instantiate(hitMark, collidertHit.point, Quaternion.identity, collidertHit.transform); // 피격점 표시 마찬가지로 필수 아님
        }
    }

    private IEnumerator OnReload(Gun _gun)
    {
        PlaySound(_gun.audioClipReload);
        isReload = true;
        float _reloadTime = _gun.reloadTime;
        while (_reloadTime > 0)
        {
            _reloadTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("Reload Complete");
        _gun.currentAmmo = _gun.maxAmmo;
        isReload = false;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}