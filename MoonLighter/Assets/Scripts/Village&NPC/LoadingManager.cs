using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingManager : MonoBehaviour
{
    public static string nextScene;
    public static bool IsLoadingStart = false;
    
    public GameObject mFadeImage;
    public GameObject mLoadingAnimation;
    private Canvas mCanvas;
    private float mTimer;
    private void Awake()
    {
        mCanvas = GetComponent<Canvas>();
        mCanvas.worldCamera= Camera.main;
        DontDestroyOnLoad(gameObject);
        mFadeImage.SetActive(false);
        mLoadingAnimation.SetActive(false);
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        IsLoadingStart = true;
    }

    private void Update()
    {
        if (IsLoadingStart)
        {
            mTimer = 0;
            
            StartCoroutine(FadeOutStart());
            IsLoadingStart = false;
        }
        mCanvas.worldCamera = Camera.main;
    }
    public IEnumerator Loading()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        bool IsDone = false;
        op.allowSceneActivation = false;
        while (!IsDone && mTimer < 15)
        {
            yield return null;
            mTimer += Time.deltaTime;
            if (op.progress > 0.9f || Mathf.Approximately(0.9f, op.progress))
            {
                if(LoadingAni.IsAnimationFinished || mTimer < 0.1f)
                {
                    op.allowSceneActivation = true;
                }
            }
            if(op.progress > 1f || Mathf.Approximately(1f, op.progress))
            {
                IsDone= true;
            }
            
           
        }
        
        
        StartCoroutine(FadeInStart());

    }
    public IEnumerator FadeInStart()
    {
        LoadingAni.IsAnimationFinished = false;
        mLoadingAnimation.SetActive(false);
        Color c = mFadeImage.GetComponent<Image>().color;
        c.a = 1;
        mFadeImage.GetComponent<Image>().color = c;
        //yield return new WaitForSecondsRealtime(1f);
        for (float f = 1f; f > 0; f -= 0.05f)
        {
            c.a = f;
            mFadeImage.GetComponent<Image>().color = c;
            yield return null;
        }

        //yield return new WaitForSecondsRealtime(1);
        mFadeImage.SetActive(false);
    }
    public IEnumerator FadeOutStart()
    {

        mFadeImage.SetActive(true);
        Color c = mFadeImage.GetComponent<Image>().color;
        c.a = 0;
        mFadeImage.GetComponent<Image>().color = c;
        //yield return new WaitForSecondsRealtime(0.5f);
        for (float f = 0; f < 1f; f += 0.05f)
        {
            c.a = f;
            mFadeImage.GetComponent<Image>().color = c;
            yield return null;
        }
        //yield return new WaitForSecondsRealtime(1);
        mLoadingAnimation.SetActive(true);
        StartCoroutine(Loading());

    }

    public void AnimationFinish()
    {
        LoadingAni.IsAnimationFinished = true;
    }
}
