using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeteor : MonoBehaviour {
    public GameObject meteor;
    public GameObject instantiatedMeteor;
    System.Random rnd = new System.Random();
    int counter = 0;
    double random;
    Camera mainCam;
    float spawnRadius;
    float spawnGuaranteeTracker = 0;
    int maxMeteors  = 10000000; // 10000000

    public float meteorSpawnFrequency; // nonnegative (4)
    public float spawnGuaranteeInterval; // positive (20)
    public int primeBounces; // >= 1 (4)
    public int meteorTimeLimit; // nonnegative (100)

    public List<Sprite> defaultMeteorSprites;
    public List<Sprite> customMeteorSprites; // custom meteor skins

    int randomSpriteIndex;

    void InitVariables() {
        meteorSpawnFrequency = GameSettings.METEOR_SPAWN_FREQUENCY;
        spawnGuaranteeInterval = GameSettings.METEOR_SPAWN_GUARANTEE_INTERVAL;
        primeBounces = GameSettings.PRIME_BOUNCES;
        meteorTimeLimit = GameSettings.METEOR_TIME_LIMIT;
    }

    // Start is called before the first frame update
    void Start() {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        spawnRadius = mainCam.orthographicSize * mainCam.aspect;
        //Debug.Log(spawnRadius);
        InitVariables();
        meteorSpawnFrequency = (meteorSpawnFrequency < 0) ? 0 : meteorSpawnFrequency;
        spawnGuaranteeInterval = (spawnGuaranteeInterval <= 0) ? 1 : spawnGuaranteeInterval;
        // so it doesnt spawn immediately
        spawnGuaranteeTracker = spawnGuaranteeInterval;
        meteor.GetComponent<Meteor>().primeBounces = (primeBounces < 1) ? 1 : primeBounces;
        meteor.GetComponent<Meteor>().meteorTimeLimit = (meteorTimeLimit < 0) ? 0 : meteorTimeLimit;
    }

    // Update is called once per frame
    void Update() {
        random = rnd.NextDouble();
        if (random < meteorSpawnFrequency / 10000 || spawnGuaranteeTracker < Time.timeSinceLevelLoad) {
            if (counter < maxMeteors) {
                float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * spawnRadius;
                spawnGuaranteeTracker = Time.timeSinceLevelLoad + spawnGuaranteeInterval;
                // meteor.GetComponent<Meteor>().RandomHealth();
                instantiatedMeteor = Instantiate(meteor, new Vector3(factor, 250, 0), Quaternion.identity);
                // if skins active
                if (customMeteorSprites.Count > 0) {
                    randomSpriteIndex = rnd.Next(0, customMeteorSprites.Count);
                    instantiatedMeteor.GetComponent<Meteor>().setImage.sprite = customMeteorSprites[randomSpriteIndex];
                } else {
                    randomSpriteIndex = rnd.Next(0, defaultMeteorSprites.Count);
                    instantiatedMeteor.GetComponent<Meteor>().setImage.sprite = defaultMeteorSprites[randomSpriteIndex];
                }
                counter++;
            }
        }
    }
}