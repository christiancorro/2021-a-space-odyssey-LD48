using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] areas;
    private GameObject starship;
    private GameObject destroyArea, previusArea, currentArea;
    private Vector3 currentCenter;
    private float distance;
    public float areaRadius = 250;

    void Start() {
        currentArea = areas[0];
        previusArea = currentArea;
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        currentCenter = starship.transform.position;
    }

    void Update() {
        Debug.DrawLine(starship.transform.position, currentCenter, Color.white, 0.1f);
        distance = Vector3.Distance(starship.transform.position, currentCenter);

        if (distance > areaRadius) {

            int randomAreaIndex = Random.Range(0, areas.Length);
            destroyArea = previusArea;
            currentCenter = starship.transform.position;
            currentArea = Instantiate(areas[randomAreaIndex], currentCenter, Quaternion.Euler(0, 0, Random.Range(0.0f, 180.0f)));
            previusArea = currentArea;
            // destroyArea.SetActive(false);
            Debug.Log("Generating area " + currentArea.gameObject.name);
        }
    }
}
