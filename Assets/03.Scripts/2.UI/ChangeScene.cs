using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Scene 전환을 위한 클래스
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public RectTransform parentRectTransform;
    public RectTransform childRectTransform;
    public RectTransform childRectTransform2;
    public Image transitionImage; 
    public float duration = 1.0f;

    private Vector2 initialSize; 
    private Vector2 targetSize;

    private void Start()
    {
        targetSize = parentRectTransform.rect.size;
        initialSize = childRectTransform.sizeDelta;
    }

    public void ChangeImageScene(string sceneName)
    {
        StartCoroutine(PlayTransition(sceneName));
    }

    private IEnumerator PlayTransition(string sceneName)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            childRectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSize * 0.55f, t);
            childRectTransform2.sizeDelta = Vector2.Lerp(initialSize, targetSize * 0.55f, t);
            yield return null;
        }

        SceneChange(sceneName);
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("UI_TestScene_Main");
    }
}