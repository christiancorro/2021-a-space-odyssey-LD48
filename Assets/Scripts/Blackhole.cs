using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Blackhole : MonoBehaviour {

    private void Update() {
        transform.Rotate(0, 0, 2f, Space.Self);
    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Asteroids") {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        } else if (other.tag == "Starship") {
            Starship.health = -1;
            Debug.Log("Blackhole: gameover");
        }
    }
}
