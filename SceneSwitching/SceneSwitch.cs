using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SetGameSettings(
        int maxHealth, // positive (100)
        int maxShieldHealth, // positive (50)
        int startingShieldHealth, // nonnegative (0)
        bool alwaysInvincible, // t/f (f)
        int baseRegenerationAmount, // nonnegative (0)
        float regenerationInterval, // positive (0.5)
        int moveSpeed, // nonnegative (175)
        int fireSpeed, // positive (10)
        int bulletDamage, // nonnegative (1)
        bool powerupStacking, // t/f (f)
        float meteorSpawnFrequency, // nonnegative (4)
        float meteorSpawnGuaranteeInterval, // positive (20)
        int primeBounces, // >= 1 (4)
        int meteorTimeLimit, // nonnegative (100)
        float powerupSpawnFrequency, // nonnegative (0.15)
        float powerupSpawnGuaranteeInterval // positive (15)
    ) {
        GameSettings.MAX_HEALTH = (int)maxHealth;
        GameSettings.MAX_SHIELD_HEALTH = (int)maxShieldHealth;
        GameSettings.STARTING_SHIELD_HEALTH = (int)startingShieldHealth;
        GameSettings.ALWAYS_INVINCIBLE = alwaysInvincible;
        GameSettings.BASE_REGENERATION_AMOUNT = (int)baseRegenerationAmount;
        GameSettings.REGENERATION_INTERVAL = (float)regenerationInterval;
        GameSettings.MOVE_SPEED = (int)moveSpeed;
        GameSettings.FIRE_SPEED = (int)fireSpeed;
        GameSettings.BULLET_DAMAGE = (int)bulletDamage;
        GameSettings.POWERUP_STACKING = powerupStacking;

        // meteor
        GameSettings.METEOR_SPAWN_FREQUENCY = (float)meteorSpawnFrequency;
        GameSettings.METEOR_SPAWN_GUARANTEE_INTERVAL = (float)meteorSpawnGuaranteeInterval;
        GameSettings.PRIME_BOUNCES = (int)primeBounces;
        GameSettings.METEOR_TIME_LIMIT = (int)meteorTimeLimit;

        // powerup
        GameSettings.POWERUP_SPAWN_FREQUENCY = (float)powerupSpawnFrequency;
        GameSettings.POWERUP_SPAWN_GUARANTEE_INTERVAL = (float)powerupSpawnGuaranteeInterval;
    }

    public void StartButton() {
        SceneManager.LoadScene(2);
    }
    public void PresetDifficulty(int difficultyIndex) {
        // 0 - easy, 1 - medium, 2 - hard
        switch (difficultyIndex) {
            case 0:
                SetGameSettings(150, 50, 0, false, 0, 0.5f, 200, 10, 1, false, 2f, 20f, 3, 50, 1f, 20f);
                break;
            case 1:
                SetGameSettings(100, 50, 0, false, 0, 0.5f, 175, 10, 1, false, 2.5f, 20f, 4, 100, 0.7f, 20f);
                break;
            case 2:
                SetGameSettings(50, 50, 0, false, 0, 0.5f, 150, 10, 1, false, 3f, 20f, 4, 150, 0.4f, 20f);
                break;
        }
        SceneManager.LoadScene(0);
    }
    public void CustomizeButton() {
        SceneManager.LoadScene(3);
    }
    public void BackButton() {
        SceneManager.LoadScene(1);
    }
}
