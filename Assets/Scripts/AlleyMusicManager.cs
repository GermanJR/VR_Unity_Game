using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyMusicManager : MonoBehaviour
{
    AudioSource[] audioSources;

    AudioSource ambience;
    AudioSource combat;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        try
        {
            ambience = audioSources[0];
            combat = audioSources[1];
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

    public void ChangeToCombatMusic()
    {
        StartCoroutine(ChangeToCombatMusicCoroutine());
    }

    IEnumerator ChangeToCombatMusicCoroutine()
    {
        StartCoroutine(FadeAudioSource.StartFade(ambience, 2f, 0f));
        yield return new WaitForSeconds(2f);
        ambience.Stop();
        combat.volume = 0f;
        combat.Play();
        StartCoroutine(FadeAudioSource.StartFade(combat, 1f, 1f));
    }

    public void FadeCombatMusic()
    {
        StartCoroutine(FadeCombatMusicCoroutine());
    }

    IEnumerator FadeCombatMusicCoroutine()
    {
        StartCoroutine(FadeAudioSource.StartFade(combat, 5f, 0f));
        yield return new WaitForSeconds(5f);
        combat.Stop();
    }
}
