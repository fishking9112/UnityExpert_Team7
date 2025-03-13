using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser : MonoBehaviour
{
    public GameObject Raybody;//레이케스팅을 쏘는 위치
    public GameObject scaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult;//충돌하는 위치에 출력할 결과 
    public Transform rayStartPoint; // 레이저 시작 지점 (고정)
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
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // 고정된 시작 지점에서 레이캐스트 발사
        if (Physics.Raycast(rayStartPoint.position, transform.forward, out hit, 200))
        {
            // 레이저 활성화
            scaleDistance.gameObject.SetActive(true);
            
            // 레이저를 시작 지점에 고정
            scaleDistance.transform.position = rayStartPoint.position;
            
            // 레이저가 히트 지점을 향하도록 회전
            scaleDistance.transform.LookAt(hit.point);
            
            // Z축으로 90도 회전 (필요한 경우)
            scaleDistance.transform.Rotate(0, 0, 90);
            
            // 레이저의 길이를 히트 거리에 맞게 조정
            float distance = Vector3.Distance(rayStartPoint.position, hit.point);
            scaleDistance.transform.localScale = new Vector3(0.3f, 0.3f, distance);
            

            RayResult.transform.position = hit.point;
        }
        else
        {
            // 레이캐스트가 아무것도 맞지 않았을 때
            // 레이저를 최대 거리까지 표시하거나 비활성화
            
            // 옵션 1: 최대 거리까지 레이저 표시
            scaleDistance.gameObject.SetActive(true);
            scaleDistance.transform.position = rayStartPoint.position;
            scaleDistance.transform.rotation = Quaternion.LookRotation(transform.forward);
            scaleDistance.transform.Rotate(0, 0, 90); // Z축으로 90도 회전 (필요한 경우)
            scaleDistance.transform.localScale = new Vector3(0.3f, 0.3f, 200); // 최대 거리
            
            // 옵션 2: 레이저 비활성화
            // scaleDistance.gameObject.SetActive(false);
            
            
        }
    }
}
