using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser : MonoBehaviour
{
    public GameObject Raybody;//레이케스팅을 쏘는 위치
    public GameObject scaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult;//충돌하는 위치에 출력할 결과 
    public Transform rayStartPoint; // 레이저 시작 지점 (고정)
    public float maxDistance = 200f; // 최대 레이저 거리
    public float updateInterval = 0.1f; // 레이저 업데이트 간격 (초)

    private GameObject currentImpact; // 현재 활성화된 임팩트 오브젝트
    private RaycastHit lastHit; // 마지막 히트 정보
    private bool hasTarget = false; // 현재 타겟이 있는지 여부
    private float lastUpdateTime = 0f; // 마지막 업데이트 시간

    // Start is called before the first frame update
    void Start()
    {

        // 레이저 시작 지점이 지정되지 않았다면 현재 오브젝트 위치 사용
        if (rayStartPoint == null)
        {
            rayStartPoint = transform;
        }

        // 레이저 오브젝트를 시작 지점에 위치시킴
        if (scaleDistance != null)
        {
            scaleDistance.transform.position = rayStartPoint.position;
        }


        // 초기 레이저 설정
        UpdateLaser();
    }


    void Update()
    {
        // 일정 간격으로만 레이저 업데이트
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            UpdateLaser();
            lastUpdateTime = Time.time;
        }
    }

    // 레이저 업데이트 함수
    void UpdateLaser()
    {
        RaycastHit hit;
        // 고정된 시작 지점에서 레이캐스트 발사
        if (Physics.Raycast(rayStartPoint.position, transform.forward, out hit, maxDistance))
        {
            if (!hasTarget && hit.collider != lastHit.collider)
            {
                // 레이저 활성화
                scaleDistance.gameObject.SetActive(true);

                // 시작 지점과 히트 지점 사이의 중간 위치 계산
                Vector3 middlePosition = transform.position + (hit.point - transform.position) / 2;

                // 레이저를 시작 지점에 고정
                scaleDistance.transform.position = middlePosition;

                // 레이저가 히트 지점을 향하도록 회전
                scaleDistance.transform.LookAt(hit.point);

                // Z축으로 90도 회전 (필요한 경우)
                //scaleDistance.transform.Rotate(0, 0, 0);

                // 레이저의 길이를 히트 거리에 맞게 조정
                float distance = Vector3.Distance(rayStartPoint.position, hit.point);
                scaleDistance.transform.localScale = new Vector3(0.3f, 0.3f, distance);


                //RayResult.transform.position = hit.point;

                // 현재 히트 정보 저장
                lastHit = hit;
                hasTarget = true;
            }
        }
        else
        {
            // 이전에 타겟이 있었거나 처음 실행하는 경우에만 업데이트
            //if (hasTarget || Time.time < 0.5f)
            //{
            //    // 레이저를 최대 거리까지 표시
            //    scaleDistance.gameObject.SetActive(true);
            //    scaleDistance.transform.position = rayStartPoint.position;
            //    scaleDistance.transform.rotation = Quaternion.LookRotation(transform.forward);
            //    scaleDistance.transform.localScale = new Vector3(0.1f, 0.1f, maxDistance);

            //    hasTarget = false;
            //}
        }
    }

    // 수동으로 레이저 업데이트 호출 (외부에서 필요할 때 호출)
    public void ForceUpdateLaser()
    {
        UpdateLaser();
        lastUpdateTime = Time.time;
    }

    // 플레이어 회전 등 중요한 변화가 있을 때 호출
    public void OnPlayerRotate()
    {
        ForceUpdateLaser();
    }
}
