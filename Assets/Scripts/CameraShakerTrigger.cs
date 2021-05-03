using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShakerTrigger : MonoBehaviour {

    public Starship starship;
    private bool shaked = false;

    void Start() {

    }

    void Update() {
        if (GameStatusManager.isGameover() && !shaked) {
            shaked = true;
        }
    }
}
