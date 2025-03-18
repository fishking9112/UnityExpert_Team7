using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Open, //열린 상태
    Closed,//닫힌상태
    Openning,//열리는 중
    Closing,//닫히는 중
}
public class DoorOpen : MonoBehaviour
{
    
    public Transform Door;
    //[SerializeField]private List<IPressable> pressables =new List<IPressable>();
    public List<ButtonObj> pressables;

    private List<bool> btn_pressed;
    private Vector3 StartPosition;
    Vector3 targetPosiotion;
    private float Addendposition = 5.5f;


    private DoorState currentState = DoorState.Closed;

    private float currentLerpTime = 0f;
    private float lerpDuration = 1f; // 문이 완전히 열리거나 닫히는 데 걸리는 시간

    private void Awake()
    {
        //위치 초기화 
        init();
    }

    private void init()
    {
        btn_pressed = new List<bool>();
        StartPosition = Door.transform.position;
        targetPosiotion = StartPosition.WithY(Door.transform.position.y + Addendposition);
    }


    private void Update()
    {
        bool allButtonPressed, allButtonReleased;
        Chk_BtnPress(out allButtonPressed, out allButtonReleased);

        Chk_DoorState(allButtonPressed, allButtonReleased);

        Interack_Door();

    }

    private void Interack_Door()
    {
        switch (currentState)
        {

            case DoorState.Openning:
                // 열리는 중
                currentLerpTime += Time.deltaTime;
                float opent = currentLerpTime / lerpDuration;
                opent = Mathf.Clamp01(opent); // t 값을 0~1 사이로 제한

                Door.transform.position = Vector3.Lerp(StartPosition, targetPosiotion, opent);
                if(opent >= 0.5f)
                {

                }

                if (opent >= 1.0f)
                {
                    currentState = DoorState.Open;
                    currentLerpTime = 0f;
                    btn_pressed.Clear();
                }
                break;
            case DoorState.Closing:
                // 닫히는 중
                currentLerpTime += Time.deltaTime;
                float closeT = currentLerpTime / lerpDuration;
                closeT = Mathf.Clamp01(closeT); // t 값을 0~1 사이로 제한

                Door.transform.position = Vector3.Lerp(targetPosiotion, StartPosition, closeT);

                if (closeT >= 1.0f)
                {
                    currentState = DoorState.Closed;
                    currentLerpTime = 0f;
                    btn_pressed.Clear();
                }
                break;

            case DoorState.Open:
                break;
            case DoorState.Closed:
                break;
            default:
                break;
        }
    }

    private void Chk_DoorState(bool allButtonPressed, bool allButtonReleased)
    {
        // 모든 버튼이 눌렸을 때만 문 열기
        if (allButtonPressed)
        {
            if (currentState != DoorState.Open && currentState != DoorState.Openning &&
                Door.transform.position.y == StartPosition.y)
            {
                currentState = DoorState.Openning;
                currentLerpTime = 0f;
            }
        }
        // 모든 버튼이 눌리지 않았을 때만 문 닫기
        //처음에 아무것도 안하면 버튼이 안눌려져서 true발생 때문에 door의 위치 조건추가 
        else if (allButtonReleased)
        {
            if (currentState != DoorState.Closed && currentState != DoorState.Closing &&
                Door.transform.position.y == targetPosiotion.y)
            {
                currentState = DoorState.Closing;
                currentLerpTime = 0f;
            }
        }
    }

    private void Chk_BtnPress(out bool allButtonPressed, out bool allButtonReleased)
    {
        // 매 프레임마다 true로 초기화하고 검사 시작
        allButtonPressed = true;
        allButtonReleased = true;

        // 버튼이 하나도 없으면 기본적으로 false로 설정
        if (pressables.Count == 0)
        {
            allButtonPressed = false;
            allButtonReleased = true; // 버튼이 없으면 모두 눌리지 않은 상태로 간주
        }
        else
        {
            // 모든 버튼이 눌렸는지 확인
            for (int i = 0; i < pressables.Count; i++)
            {
                //버튼중에 1개라도 안눌려져있다면 false로 해서 open을 못하게
                if (!pressables[i].IsPressed)
                {
                    allButtonPressed = false;
                }
                //버튼중에 1개라도 눌려져있다면 false해서 close를 못하게
                if (pressables[i].IsPressed)
                {
                    allButtonReleased = false;
                }
            }
        }
    }

    public void OpenDoor()
    {
        //문이열렸을때 이벤트 추가할려면 여기에 추가

    }
    public void CloseDoor()
    {
        //문이 닫혔을때 이벤트 추가할려면 여기에 추가 
    }
}
