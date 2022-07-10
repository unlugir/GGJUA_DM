using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public  class FadeAudioSource : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float fadeTime;
    [SerializeField] float maxVol;
    [SerializeField] bool playOnStart;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (playOnStart) Fade();
    }
    public void Fade() 
    {
        StartCoroutine(StartFade(source, fadeTime, maxVol));
    }
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        
        float currentTime = 0;
        float start = audioSource.volume;
        audioSource.Play();
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}