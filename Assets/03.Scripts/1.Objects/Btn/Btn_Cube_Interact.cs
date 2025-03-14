using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Cube_Interact : MonoBehaviour
{
    private Button button;


    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Check_Press(collision, 1);

    }
    private void OnCollisionExit(Collision collision)
    {
        Check_Press(collision,2);
    }

    private void Check_Press(Collision collision, int Pressednum)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            if (button != null)
            {
                if (Pressednum == 1)
                {
                    button.ChkedPress();
                }
                else if (Pressednum == 2)
                {
                    button.ChkOutPress();
                }
            }
        }
    }
}
