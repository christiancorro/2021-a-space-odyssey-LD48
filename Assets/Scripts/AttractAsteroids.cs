using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttractAsteroids : MonoBehaviour {

    public float mass = 1;
    public float radius = 20;
    public GameObject[] asteroids;
    public List<GameObject> nearAsteroids;
    public ParticleSystem attractor;

    void Start() {
        try {
            UpdateAsteroids();
        } catch { }
    }


    void Update() {

        if (Input.GetButton("Attract")) {
            attractor.Play();
            try {
                UpdateAsteroids();
                for (int i = 0; i < nearAsteroids.Count; i++) {
                    Vector3 difference = this.transform.position - nearAsteroids[i].transform.position;
                    float dist = difference.magnitude;
                    Vector3 gravityDirection = difference.normalized;
                    float gravity = 6.7f * (mass * 80) / (dist * dist);
                    Vector3 gravityVector = (gravityDirection * gravity);
                    // nearAsteroids[i].GetComponent<Rigidbody>().AddForce(nearAsteroids[i].transform.forward, ForceMode.Acceleration);
                    nearAsteroids[i].GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
                }
            } catch { }
        } else {
            attractor.Stop();
        }
    }

    void UpdateAsteroids() {
        nearAsteroids.Clear();
        asteroids = GameObject.FindGameObjectsWithTag("Asteroids");
        for (int i = 0; i < asteroids.Length; i++) {
            if ((asteroids[i].transform.position - this.transform.position).magnitude < radius) {
                nearAsteroids.Add(asteroids[i]);
            }
        }
    }
}
