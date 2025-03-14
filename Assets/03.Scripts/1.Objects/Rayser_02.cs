using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser_02 : MonoBehaviour
{
    public GameObject Raybody; //레이저 쏘는 몸통
    public GameObject ScaleDistance; //거리에 따른 스케일 변화를 위한 오브젝트 대상
    public GameObject RayResult; // 충돌하느 위치에 촐력할 결과 임펙트
    private float maxDistance;
    private GameObject lastHitCube = null;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        GameObject currentHitCube = null;

        //쏘는 위치,방향,결과값,최대인식거리
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            int layserIndex = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(layserIndex);
            //Debug.Log("Hit object layer: " + layerName + " (index: " + layserIndex + ")");

            // 시작 지점과 히트 지점 사이의 중간 위치 계산
            Vector3 middlePosition = transform.position + (hit.point - transform.position) / 2;
            //레이저를 중간위치에 설정(우리의 sprite는 한쪽방향이 아닌 양쪽으로 증가하기 때문)
            ScaleDistance.transform.position = middlePosition;

            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            // 레이저가 히트 지점을 향하도록 회전
            //ScaleDistance.transform.LookAt(hit.point);

            //거리에 따른 레이저 스케일변화 
            ScaleDistance.transform.localScale = new Vector3(0.1f, hit.distance, 0.1f);


            
            //레이캐스트가 땋는곳에 오브젝트를 옮긴다.
            //RayResult.transform.position = hit.point;

            //해당하는 오브젝트의 회전값을 닿은 면적의 노멀방향와 일치시킨다.
            //RayResult.transform.rotation = Quaternion.LookRotation(hit.normal);

            //레이저 큐브와 충돌했을때 
            if (layserIndex == LayerMask.NameToLayer("LayserCube"))
            {

                currentHitCube = hit.collider.gameObject;
                // hit.point 에서 ray를 다시 쏘기
                hit.collider.GetComponent<Cube_Rayser>().ChkRayserLayser();
            }
            else
            {

            }

        }

        if (lastHitCube != null && lastHitCube != currentHitCube)
        {
            Cube_Rayser lastCubeRayser = lastHitCube.GetComponent<Cube_Rayser>();
            if (lastCubeRayser != null)
            {
                lastCubeRayser.ChkOutRayserLayser();
            }
        }

        lastHitCube = currentHitCube;
    }
}
