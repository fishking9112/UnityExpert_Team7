using UnityEngine;

public class EscButton : MonoBehaviour
{
    public GameObject targetObject;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = isActive ? 1 : 0;
            isActive = !isActive;
            targetObject.SetActive(isActive);
        }
    }
}
