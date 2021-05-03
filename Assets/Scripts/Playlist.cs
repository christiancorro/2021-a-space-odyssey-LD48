using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour {

    public AudioClip[] music;
    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();

        if (!audio.playOnAwake) {
            audio.clip = music[Random.Range(0, music.Length)];
            audio.Play();
            audio.loop = true;
        }
    }

    void PlayNextSong() {
        audio.clip = music[Random.Range(0, music.Length)];
        audio.Play();
        Invoke("PlayNextSong", audio.clip.length);
    }
}
