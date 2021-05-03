using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour {

    private GameObject starship;

    private void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            Debug.Log("Wormhole");
            starship.transform.position = new Vector3(starship.transform.position.x, starship.transform.position.y + 1000, 0);

        }
    }
}
