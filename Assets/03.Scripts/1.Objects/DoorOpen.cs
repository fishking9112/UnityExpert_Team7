using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //LP_Bay_Door_snaps y축 5.5까지 이동
    public Transform Door;

    private float Addendposition = 5.5f;
    public float MoveSpeed =1f;


    //이벤트에 추가할 함수를 구현

    public void OpenDoor()
    {
        Vector3 targetPosiotion = Door.transform.position.WithY(Door.transform.position.y + Addendposition);

        Door.transform.position = Vector3.Lerp(Door.transform.position, targetPosiotion,Time.deltaTime * MoveSpeed);
    }
}
