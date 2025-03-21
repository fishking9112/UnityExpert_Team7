
using UnityEngine;
using UnityEngine.Events;

public class ButtonObj : MonoBehaviour, IPressable
{
    private bool isPressed = false;
    public bool IsPressed => isPressed;

    [SerializeField] private UnityEvent onPress;
    [SerializeField] private UnityEvent onRelease;
    public void Interact(Interaction player)
    {
        if (gameObject.layer == 10)
        {
            return;
        }
        else
        {
            Press(player);
        }
    }


    public virtual void Press(Interaction player)
    {
        // press된 상태인지 구분 
        isPressed = !isPressed; //<< 잠시 대기 사용할지 안할지 모르겟음 
        if (isPressed) // 눌렸다면
        {
            onPress.Invoke();
            //애니메이션 추가할거면 여기서
        }
        else //안눌렸다면 
        {
            onRelease.Invoke();
            //애니메이션 추가할거면 여기서
        }
    }

    public bool CanInteract(Interaction player)
    {
        // 버튼이 눌려져있지 않을때 상호작용 가능 
        return !isPressed;
    }


    public string GetInteractionPrompt()
    {
        return "누르기";
    }

    public virtual void ChkedPress()
    {
        isPressed = true;
    }
    public void ChkOutPress()
    {
        isPressed = false;
    }


}
