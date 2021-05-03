using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Docking : MonoBehaviour {

    private GameObject starship;
    private Rigidbody starshipRigidbody;
    private float angle = 35;
    private Planet planet;
    private ParticleSystem oxygenAndFuel;

    public AudioSource dockingAudio;

    void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        oxygenAndFuel = GameObject.Find("Oxygen Collision").GetComponent<ParticleSystem>();
        starshipRigidbody = starship.GetComponent<Rigidbody>();
        planet = gameObject.GetComponentInParent<Planet>();
    }

    void Update() {

    }

    private void OnTriggerExit(Collider other) {
        oxygenAndFuel.Stop();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Starship") {
            if (starshipRigidbody.velocity.magnitude < 2 && Vector3.Angle(-starship.transform.up, transform.position - starship.transform.position) < angle) {
                Vector3.Lerp(starshipRigidbody.velocity, Vector3.zero, 3 * Time.deltaTime);
                oxygenAndFuel.Play();
                if (dockingAudio != null)
                    dockingAudio.volume = 0.017f;
                if (Starship.oxygen < planet.maxOxygen) {
                    Starship.oxygen += 10 * Time.deltaTime;
                }
                if (Starship.fuel < planet.maxFuel) {
                    Starship.fuel += 10 * Time.deltaTime;
                }

                if (Starship.health < 100) {
                    Starship.health += 10 * Time.deltaTime;
                }
            } else {
                Starship.ApplyDamage(0.23f);
                oxygenAndFuel.Stop();
                if (dockingAudio != null)
                    dockingAudio.volume = 0f;
            }
        }
    }
}
