using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;

    private void Awake ()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start ()
    {
        StartCoroutine(FadeIn(0f, 1f, 1f));
    }

    private IEnumerator FadeIn (float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float elapsedTime = 0f;

        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime;
            var percentage = 1 / (duration / elapsedTime);
            if (startAlpha > endAlpha)
            {
                canvas.alpha = startAlpha - percentage;
            }
            else
            {
                canvas.alpha = startAlpha + percentage;
            }

            yield return new WaitForEndOfFrame();
        }
        canvas.alpha = endAlpha;


    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
