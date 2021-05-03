using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float damage = 5;
    public bool hasFuel;
    public float fuel = 10;
    private Rigidbody rb;
    private ParticleSystem fuelCollison;

    private void Start() {
        fuelCollison = GameObject.Find("Asteroid Fuel Collision").GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        damage = 2 * rb.mass;
        if (hasFuel) {
            GetComponent<MeshCollider>().isTrigger = true;
        }

        rb.AddTorque(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
        //rb.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-30, 1), 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Starship") {
            if (hasFuel) {
                Starship.fuel += fuel;
                fuelCollison.Play();
                this.gameObject.SetActive(false);
            } else {
                Starship.ApplyDamage(damage);
                Debug.Log("Asteroid collision demage");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            if (hasFuel) {
                Starship.fuel += fuel;
                fuelCollison.Play();
                this.gameObject.SetActive(false);
            }
        }

    }
}
