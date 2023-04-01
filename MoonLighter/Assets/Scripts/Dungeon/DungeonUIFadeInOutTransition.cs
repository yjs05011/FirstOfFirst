using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIFadeInOutTransition : MonoBehaviour
{
    private void Start()
    {
        DungeonManager.Instance.mTransitionUI = this;
    }

    [SerializeField] protected CanvasGroup m_canvasGroup = null;

    public IEnumerator TransitionFadeIn(float fadeDuration = 1.0f)
    {
        return this.FadeIn(fadeDuration, m_canvasGroup);
    }

    public IEnumerator TransitionFadeOut(float fadeDuration = 1.0f)
    {
        return this.FadeOut(fadeDuration, m_canvasGroup);
    }

    private IEnumerator FadeIn(float fadeDuration, CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;

        yield return this.StartCoroutine(Fade(0.0f, fadeDuration, canvasGroup));

        canvasGroup.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(float fadeDuration, CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;

        canvasGroup.gameObject.SetActive(true);

        yield return this.StartCoroutine(Fade(1.0f, fadeDuration, canvasGroup));
    }

    private IEnumerator Fade(float finalAlpha, float fadeDuration, CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(1);

        float speed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha, speed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
    }
}
