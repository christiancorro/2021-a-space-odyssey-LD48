using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public float maxOxygen = 99;
    public float maxFuel = 20;
    public float rotationSpeed = 0.1f;

    void Start() {

    }

    void Update() {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }
}
