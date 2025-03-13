using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Scene 전환을 위한 클래스
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public Image transitionImage;
    public float duration = 1.0f;

    public void ChangeImageScene(string sceneName)
    {
        StartCoroutine(PlayTransition(sceneName));
    }

    private IEnumerator PlayTransition(string sceneName)
    {
        float time = 0f;
        RectTransform rectTransform = transitionImage.GetComponent<RectTransform>();

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 20f, time / duration);
            rectTransform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
        SceneChange(sceneName);
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
