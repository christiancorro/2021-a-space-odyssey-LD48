using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Star : MonoBehaviour {

    public float demage = 0.8f;
    public AudioMixerSnapshot defaultSnapshot, starSnaphost;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Starship") {
            starSnaphost.TransitionTo(1f);
            Starship.ApplyDamage(demage);
            Debug.Log("Star damage");
        }
    }

    private void OnTriggerExit(Collider other) {
        defaultSnapshot.TransitionTo(4f);
    }
}
