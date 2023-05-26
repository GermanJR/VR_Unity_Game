using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERMusicManager : MonoBehaviour
{

    private AudioSource[] audios;

    private AudioSource waitMusic;
    private AudioSource playingMusic;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();

        waitMusic = audios[0];
        playingMusic = audios[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToPlayMusic()
    {
        StartCoroutine(FadeToPlayMusicCoroutine());
    }

    IEnumerator FadeToPlayMusicCoroutine()
    {
        Debug.Log("Fading to play music");
        FadeAudioSource.StartFade(waitMusic, 3f, 0f);
        yield return new WaitForSeconds(3f);
        waitMusic.Stop();
        //playingMusic.volume = 0f;
        playingMusic.Play();
        FadeAudioSource.StartFade(playingMusic, 1f, 1f);
    }

    public void ReturnToWaitingMusic()
    {
        StartCoroutine(ReturnToWaitingMusicCoroutine());
    }

    IEnumerator ReturnToWaitingMusicCoroutine()
    {
        Debug.Log("Fading to wait music");
        FadeAudioSource.StartFade(playingMusic, 1f, 0f);
        yield return new WaitForSeconds(1f);
        playingMusic.Stop();
        //waitMusic.volume = 0f;
        waitMusic.Play();
        FadeAudioSource.StartFade(waitMusic, 1f, 1f);
    }
}
