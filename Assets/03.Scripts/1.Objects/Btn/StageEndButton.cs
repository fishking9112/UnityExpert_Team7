using UnityEngine;

public class StageEndButton : ButtonObj
{


    public override void Press(Interaction player)
    {
        GameManager.Instance.StageClear();
        base.Press(player);
    }

}
