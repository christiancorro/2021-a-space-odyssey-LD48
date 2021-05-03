using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomTorque : MonoBehaviour {

    public float maxValue = 0.2f;
    public float minValue = -0.2f;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(Random.Range(minValue, maxValue), Random.Range(minValue, maxValue), Random.Range(minValue, maxValue)), ForceMode.Impulse);
    }

    void Update() {

    }
}
