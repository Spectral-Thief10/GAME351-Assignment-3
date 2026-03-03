using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public enum MusicState
    {
        None,
        Default,
        Suspense,
        Fight
    }

    public AudioClip defaultClip;
    public AudioClip suspenseClip;
    public AudioClip fightClip;

    public float fadeDuration = 1.5f;

    private AudioSource currentSource;
    private AudioSource nextSource;
    private MusicState baseState = MusicState.Default;
    private bool inCombat = false;

    private MusicState currentState = MusicState.None;

    private void Start()
    {
        SwitchState(MusicState.Default);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        currentSource = gameObject.AddComponent<AudioSource>();
        nextSource = gameObject.AddComponent<AudioSource>();

        currentSource.loop = true;
        nextSource.loop = true;
    }

    public void SwitchState(MusicState newState)
    {
        if (newState == currentState)
            return;

        AudioClip newClip = GetClipFromState(newState);
        if (newClip == null)
            return;

        currentState = newState;

        StopAllCoroutines();
        StartCoroutine(Crossfade(newClip));
    }

    private AudioClip GetClipFromState(MusicState state)
    {
        switch (state)
        {
            case MusicState.Default:
                return defaultClip;
            case MusicState.Suspense:
                return suspenseClip;
            case MusicState.Fight:
                return fightClip;
            default:
                return null;
        }
    }

    private IEnumerator Crossfade(AudioClip newClip)
    {
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            currentSource.volume = 1f - t;
            nextSource.volume = t;

            yield return null;
        }

        currentSource.Stop();

        // Swap references
        AudioSource temp = currentSource;
        currentSource = nextSource;
        nextSource = temp;
    }

    private void PlayState(MusicState state)
    {
        AudioClip newClip = GetClipFromState(state);
        if (newClip == null) return;

        StopAllCoroutines();
        StartCoroutine(Crossfade(newClip));
    }

    public void EndCombat()
    {
        inCombat = false;
        PlayState(baseState);
    }
}
