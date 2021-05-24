using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsRandom : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private int clipIndex;
    private AudioSource audio;
    //private bool audioPlaying = false;

    public float minWait = 120f;
    public float maxWait = 240f;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!audio.isPlaying)
        {

            clipIndex = Random.Range(0, clips.Length);
            audio.clip = clips[clipIndex];
            audio.PlayDelayed(Random.Range(minWait, maxWait));
            Debug.Log("Nothing playing, we set new audio to " + audio.clip.name);
        }
    }
}
