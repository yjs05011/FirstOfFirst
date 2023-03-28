using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : GSingleton<SoundManager>
{
    public static SoundManager instance;
    public AudioClip[] bgms;
    public AudioSource bgm;
    public int sfx = 3;
    public int music = 3;

    protected override void Init()
    {
        base.Init();
    }
    public void SfxPlay(string SfxName, AudioClip clip, float volume)
    {
        GameObject go = new GameObject(SfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(go, clip.length);
    }
    public void SfxPlay(string SfxName, AudioClip clip, float Time, float volume)
    {
        GameObject go = new GameObject(SfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.time = Time;
        audioSource.Play();

        Destroy(go, 2f);
    }
    public void BgmSoundPlay(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.loop = true;
        bgm.volume = 0.1f;
        bgm.Play();
    }
    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            if (arg0.name == bgms[i].name)
            {
                BgmSoundPlay(bgms[i]);
            }

        }

    }
}
