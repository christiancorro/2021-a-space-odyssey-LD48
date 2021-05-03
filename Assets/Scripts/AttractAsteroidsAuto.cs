using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttractAsteroidsAuto : MonoBehaviour {


    public float mass = 2;
    public float radius = 50;
    public GameObject[] asteroids;
    public List<GameObject> nearAsteroids;
    private Rigidbody nearAsteroidRigidBody;

    float G = 6.67f;

    void Start() {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroids");
        InvokeRepeating("UpdateAsteroids", 0, 5); // update asteroids every 2 seconds
    }

    void Update() {
        try {
            Attract();
        } catch {

        }
    }

    private void Attract() {

        for (int i = 0; i < nearAsteroids.Count; i++) {
            nearAsteroidRigidBody = nearAsteroids[i].GetComponent<Rigidbody>();
            float distance = Vector3.Distance(this.transform.position, nearAsteroids[i].transform.position);
            float distanceSquared = distance * distance;

            float gravityForce = G * (this.mass * nearAsteroidRigidBody.mass) / (distanceSquared);
            Vector3 heading = (this.transform.position - nearAsteroids[i].transform.position);
            Vector3 gravityVector = (gravityForce * (heading.normalized));
            //nearAsteroidRigidBody.AddForce(nearAsteroids[i].transform.forward * 20, ForceMode.Acceleration);
            nearAsteroidRigidBody.AddForce(gravityVector, ForceMode.Impulse);
        }
    }

    void UpdateAsteroids() {
        nearAsteroids.Clear();
        try {
            for (int i = 0; i < asteroids.Length; i++) {
                if ((asteroids[i].transform.position - this.transform.position).magnitude < radius) {
                    nearAsteroids.Add(asteroids[i]);
                }
            }
        } catch { }
    }
    private void OnDestroy() {
        nearAsteroids.Clear();
        Array.Clear(asteroids, 0, asteroids.Length);
    }
}
