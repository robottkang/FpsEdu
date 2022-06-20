using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseWeapon : UseWeapon
{
    [SerializeField]
    private GameObject[] inventory;    // 인벤토리 배열(아이템을 담을 변수)
    private GameObject handleWeapon;   // 손에 들고 있는 무기
    private int currentSlotNumber = 0; // 현재 인벤토리 슬롯의 번호

    private void Awake()
    {
        // 무기 생성
        handleWeapon = Instantiate(inventory[currentSlotNumber], eyesOfObject.transform.position + eyesOfObject.transform.forward * 0.5f + eyesOfObject.transform.up * -0.5f + eyesOfObject.transform.right * 0.5f, eyesOfObject.transform.rotation, eyesOfObject.transform);
    }

    private void Update()
    {
        inputReload = Input.GetKeyDown(KeyCode.R);

        startFire = Input.GetMouseButtonDown(0);
        stopFire  = Input.GetMouseButtonUp(0);

        Aiming();
        SwapWeapon();
        ChangeShotMode();

        // 상속받은 UseWeapon의 PlaySound에 쓰임
        // 손에 들고 있는 무기의 AudioSource를 담음
        audioSource = handleWeapon.GetComponent<AudioSource>();
        
        // 손에 들고 있는 무기의 Gun 컴포넌트를 인스턴스로 함수를 실행
        WeaponAction(handleWeapon.GetComponent<Gun>());
    }

    private void Aiming() // 조준
    {
        if (Input.GetMouseButtonDown(1))    // 클릭 시 총을 중앙으로 이동
        {
            handleWeapon.transform.position = eyesOfObject.transform.position + eyesOfObject.transform.forward * 0.5f + eyesOfObject.transform.up * -0.5f;
        }
        else if (Input.GetMouseButtonUp(1)) // 마우스 때면 조준 해제
        {
            handleWeapon.transform.position = eyesOfObject.transform.position + eyesOfObject.transform.forward * 0.5f + eyesOfObject.transform.up * -0.5f + eyesOfObject.transform.right * 0.5f;
        }
    }

    private void SwapWeapon()
    {
        int defaultSlotNumber = currentSlotNumber;  // 기존 슬롯 넘버를 기록

        // (조건에 !isReload 제거하고 스왑시 장전을 멈추는 걸로 변경할 예정)
        if (!isReload && !Input.GetMouseButton(0))  // 발사 중이 아닐때
        {
            for(int i = 0; i < inventory.Length; i++)
            {
                // (KeyCode)49 = KeyCode.Alpha1 = 키보드 Q 위에 있는 숫자키 1
                // (KeyCode)50 = KeyCode.Alpha2 = 키보드 W 위에 있는 숫자키 2 ...
                if (Input.GetKeyDown((KeyCode)(i + 49))) currentSlotNumber = i;
            }
        }

        if (defaultSlotNumber != currentSlotNumber) // 슬롯 넘버가 변경됐다면
        {
            DestroyAndInstantiate();
        }
    }

    private void ChangeShotMode()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            // 손에 들고 있는 무기의 Gun 컴포넌트의 isAutomaticAttack의 불 값을 반전
            handleWeapon.GetComponent<Gun>().isAutomaticAttack = !handleWeapon.GetComponent<Gun>().isAutomaticAttack;
        }
    }

    private void DestroyAndInstantiate() // 손에 들고 있는 무기 오브젝트를 파괴하고 새로 생성
    {
        Destroy(handleWeapon);
        handleWeapon = Instantiate(inventory[currentSlotNumber], transform.position + eyesOfObject.transform.forward * 0.5f + eyesOfObject.transform.up * -0.5f + eyesOfObject.transform.right * 0.5f, eyesOfObject.transform.rotation, eyesOfObject.transform);
    }
}
