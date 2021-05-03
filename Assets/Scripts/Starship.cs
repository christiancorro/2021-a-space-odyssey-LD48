using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Starship : MonoBehaviour {

    [Header("Main settings")]
    public static float health = 100;
    public static float fuel = 100;
    public static float fuelUsage = 0.040f;
    public static float oxygen = 100;
    public static float oxygenUsage = 0.4f;

    public static float distance = 0;

    [Header("Movement")]
    public float accelerationForce = 6f;
    public float turboForce = 30f;
    public float rotationSpeed = 3f;
    public float maxSpeed = 10f;

    public ParticleSystem engine, turbo, damageLight, damageMedium, damageCritical, explosion;

    public AudioSource engineActive, turboActive, explosionAudio;
    private float maxEngineVolume = 0.033f;

    private static Rigidbody rbody;

    public static void init() {
        health = 100;
        oxygen = 100;
        fuel = 100;
    }

    void Start() {
        rbody = this.GetComponent<Rigidbody>();
    }

    void Update() {
        if (GameStatusManager.isInit()) {
            init();
        }
        // if (GameStatusManager.isStarted()) {
        // Controls
        float rotation = Input.GetAxis("Horizontal");
        float acceleration = Input.GetAxis("Vertical");
        StarshipController(rotation, acceleration);
        UpdateDistance();

        oxygen -= oxygenUsage * Time.deltaTime;

        if (health < 100) {
            health += 0.5f * Time.deltaTime;
        }

        if (health > 100) {
            health = 100;
        }
        if (fuel > 100) {
            fuel = 100;
        }

        if (oxygen > 100) {
            oxygen = 100;
        }

        if (oxygen <= 0) {
            health = -0.1f;
        }

        if (fuel <= 0) {
            fuel = 0;
            engineActive.volume = 0f;
            turboActive.volume = 0f;
        }

        if (health <= 75 && health >= 40) {
            damageLight.Play();
        } else {
            damageLight.Stop();
        }

        if (health < 40 && health >= 20) {
            damageMedium.Play();
        } else {
            damageMedium.Stop();
        }

        if (health < 20) {
            damageCritical.Play();
        } else {
            damageCritical.Stop();
        }

        if (health <= 0 && !GameStatusManager.isGameover()) {
            explosion.Play();
            explosionAudio.Play();
            StartCoroutine(Gameover());
            health = 0;
            //Explode
            Debug.Log("BOOOOOM!");
        }
        // }
    }


    private void UpdateDistance() {
        distance = (Vector3.zero - transform.position).magnitude;
    }

    private void StarshipController(float rotation, float acceleration) {
        transform.Rotate(0, 0, -rotation * rotationSpeed * Time.deltaTime);

        if (fuel > 0 && GameStatusManager.isStarted()) {
            if (Input.GetAxis("Vertical") != 0 && !Input.GetButton("Turbo")) {
                engine.Play();
                engineActive.volume = maxEngineVolume;
                fuel -= fuelUsage;
            } else {
                engine.Stop();
                engineActive.volume = 0f;
            }
            // Turbo?
            if (Input.GetButton("Turbo") && Input.GetAxis("Vertical") != 0) {
                fuel -= 2 * fuelUsage;
                turbo.Play();
                turboActive.volume = maxEngineVolume;
                rbody.AddForce(transform.up * turboForce * acceleration);
            } else {
                turbo.Stop();
                turboActive.volume = 0f;
                rbody.AddForce(transform.up * accelerationForce * acceleration);
            }
        } else {
            turbo.Stop();
            engine.Stop();
        }
        rbody.velocity = new Vector2(Mathf.Clamp(rbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rbody.velocity.y, -maxSpeed, maxSpeed));
    }

    public static void ApplyDamage(float damage) {
        health -= damage;
    }

    public static float getVelocity() {
        return rbody.velocity.magnitude;
    }

    // Force freeze rotation on X e Y when bouncing
    void FixedUpdate() {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }

    IEnumerator Gameover() {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0);
        //gameObject.SetActive(false);
        GameStatusManager.Gameover();
    }

}
