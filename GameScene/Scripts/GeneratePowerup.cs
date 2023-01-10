using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ADD GUARANTEE SPAWN EVERY 12 SEC

public class GeneratePowerup : MonoBehaviour {
    public GameObject player;
    public GameObject powerup;
    public GameObject instantiatedPowerup;
    System.Random rnd = new System.Random();
    int counter = 0;
    double random;
    int maxPowerups = 10000000;
    Camera mainCam;
    float spawnRadius;
    float spawnGuaranteeTracker;

    public float powerupSpawnFrequency; // nonnegative (0.15)
    public float spawnGuaranteeInterval; // positive (15)

    void InitVariables() {
        powerupSpawnFrequency = GameSettings.POWERUP_SPAWN_FREQUENCY;
        spawnGuaranteeInterval = GameSettings.POWERUP_SPAWN_GUARANTEE_INTERVAL;
    }

    // Start is called before the first frame update
    void Start() {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        spawnRadius = mainCam.orthographicSize * mainCam.aspect;
        InitVariables();
        powerupSpawnFrequency = (powerupSpawnFrequency < 0) ? 0 : powerupSpawnFrequency;
        spawnGuaranteeInterval = (spawnGuaranteeInterval <= 0) ? 1 : spawnGuaranteeInterval;
        // so it doesnt spawn immediately
        spawnGuaranteeTracker = spawnGuaranteeInterval;
        // Instantiate(powerup, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        random = rnd.NextDouble();
        if (random < powerupSpawnFrequency / 10000 || spawnGuaranteeTracker < Time.timeSinceLevelLoad) {
            if (counter < maxPowerups) {
                float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * spawnRadius;
                spawnGuaranteeTracker = Time.timeSinceLevelLoad + spawnGuaranteeInterval;
                // powerup.GetComponent<powerup>().RandomHealth();
                instantiatedPowerup = Instantiate(powerup, new Vector3(factor, 250, 0), Quaternion.identity);
                instantiatedPowerup.GetComponent<Powerup>().SetPowerupType();
                instantiatedPowerup.GetComponent<Powerup>().icon.sprite = player.GetComponent<Player>().powerupIcons[instantiatedPowerup.GetComponent<Powerup>().powerupIndex];
                //Debug.Log("Powerup Index: " + instantiatedPowerup.GetComponent<Powerup>().powerupIndex);
                counter++;
            }
        }
    }
}