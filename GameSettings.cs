using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {
    // player
    public static int MAX_HEALTH = 100;
    public static int MAX_SHIELD_HEALTH = 50;
    public static int STARTING_SHIELD_HEALTH = 0;
    public static bool ALWAYS_INVINCIBLE = false;
    public static int BASE_REGENERATION_AMOUNT = 0;
    public static float REGENERATION_INTERVAL = 0.5f;
    public static int MOVE_SPEED = 175;
    public static int FIRE_SPEED = 10;
    public static int BULLET_DAMAGE = 1;
    public static bool POWERUP_STACKING = false;

    // meteor
    public static float METEOR_SPAWN_FREQUENCY = 4f;
    public static float METEOR_SPAWN_GUARANTEE_INTERVAL = 20f;
    public static int PRIME_BOUNCES = 4;
    public static int METEOR_TIME_LIMIT = 100;

    // powerup
    public static float POWERUP_SPAWN_FREQUENCY = 3;
    public static float POWERUP_SPAWN_GUARANTEE_INTERVAL = 20;
}
