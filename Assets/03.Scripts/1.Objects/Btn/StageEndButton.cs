using UnityEngine;

public class StageEndButton : MonoBehaviour,
{
    private bool isPressed = false;
    public bool IsPressed => isPressed;

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


    public void Press(Interaction player)
    {
        // press된 상태인지 구분 
         //<< 잠시 대기 사용할지 안할지 모르겟음 
        if (!isPressed) // 눌렸다면
        {
            isPressed = true;
            GameManager.Instance.StageClear();
            //애니메이션 추가할거면 여기서
        }
    }

}
