using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeExplosion : MonoBehaviour
{
    float destroyTime;
    public float explosionDuration;
    
    void Start() {
        // Debug.Log("instantiated at: (" + transform.position.x + ", " + transform.position.y + ")");
        // 100 just to allow for destroyTime to be re-changed.
        destroyTime = Time.timeSinceLevelLoad + explosionDuration;
    }

    void Update() {
        if (Time.timeSinceLevelLoad > destroyTime) {
            Destroy(gameObject);
        }
    }
}
