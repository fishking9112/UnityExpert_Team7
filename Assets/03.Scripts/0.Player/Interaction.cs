using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 중심에서 찍히는 물체를 판단하기 위한 스크립트.
/// </summary>

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // 검사 주기
    private float _lastCheckTime;   // 마지막 체크한 시간
    public float maxCheckDist;      // 검사할 레이의 거리 

    public LayerMask layerMask;     // 검사할 레이어 마스크

    public GameObject curInteractionOBJ;    // 현재 찍은 OBJ

    private Camera _camera;                 // 카메라 중심을 위한 카메라

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        //일정 시간마다 Ray 쏘기
        if (Time.time - _lastCheckTime > checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //카메라 중점에서 레이 발사 해서 충돌 했으면 ?
            if (Physics.Raycast(ray, out hit, maxCheckDist, layerMask))
            {
                if (hit.collider.gameObject != curInteractionOBJ)
                {
                    // 정보 가져오기
                    curInteractionOBJ = hit.collider.gameObject;
                    //curInteractable = hit.collider.GetComponent<IInteractable>();

                    //여기서 보성님이 작업해주시면 됩니다.
                }
            }
            //아니면
            else
            {
                // 데이터 초기화
                curInteractionOBJ = null;
                //curInteractable = null;
            }
        }
    }
}
