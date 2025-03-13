using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser : MonoBehaviour
{
    public GameObject Raybody;//레이케스팅을 쏘는 위치
    public GameObject scaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult;//충돌하는 위치에 출력할 결과 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // 레이캐스트 발사
        if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
        {
            // 레이저 활성화
            scaleDistance.gameObject.SetActive(true);
            
            // 시작 지점과 히트 지점 사이의 중간 위치 계산
            Vector3 middlePosition = transform.position + (hit.point - transform.position) / 2;
            
            // 레이저를 중간 위치로 이동
            scaleDistance.transform.position = middlePosition;
            
            // 레이저가 시작 지점에서 히트 지점을 향하도록 회전
            scaleDistance.transform.LookAt(hit.point);
            
            // 레이저의 길이를 히트 거리에 맞게 조정
            // 스케일의 z축이 길이를 나타낸다고 가정
            //scaleDistance.transform.rotation= Quaternion.Euler(0f, 0f, 90f);
            scaleDistance.transform.Rotate(0, 0, 90);
            scaleDistance.transform.localScale = new Vector3(0.1f, 0.1f, hit.distance);
            

            RayResult.transform.position = hit.point;
        }
        else
        {
            // 레이캐스트가 아무것도 맞지 않았을 때 레이저 비활성화 (선택적)
            scaleDistance.gameObject.SetActive(false);
        }

        //레이캐스트가 닿은 곳에 오브젝트를 옮긴다.(임펙트)
        

        //반사
        //해당하는 오브젝트의 회전값을 닿은 면적의 노멀방향과 일치시킨다.
        //RayResult.transform.rotation = Quaternion.LookRotation(hit.normal);
    }
}
