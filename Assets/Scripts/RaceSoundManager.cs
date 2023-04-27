using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSoundManager : MonoBehaviour
{

    private AudioSource[] audioSources;

    private AudioSource ambienceSound;
    private AudioSource raceMusic;
    private AudioSource victorySound;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        try
        {
            ambienceSound = audioSources[0];
            raceMusic = audioSources[1];
            victorySound = audioSources[2];
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.LogError("Index out of song list range. Number of songs found -> " + audioSources.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayVictorySound()
    {
        StartCoroutine(PlayVictoryAndReturnMusicCoroutine());
    }

    IEnumerator PlayVictoryAndReturnMusicCoroutine()
    {
        raceMusic.Pause();
        victorySound.Play();
        yield return new WaitForSeconds(7.3f);
        raceMusic.volume = 0;
        raceMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(raceMusic, 0.5f, 0.8f));
    }

    public void ChangeToStartMusic()
    {
        StartCoroutine(TransitionToRaceMusicCoroutine());
    }

    IEnumerator TransitionToRaceMusicCoroutine()
    {
        StartCoroutine(FadeAudioSource.StartFade(ambienceSound, 1f, 0f));
        yield return new WaitForSeconds(4f);
        raceMusic.volume = 0;
        raceMusic.Play();
        StartCoroutine(FadeAudioSource.StartFade(raceMusic, 0.5f, 0.8f));
    }
}
