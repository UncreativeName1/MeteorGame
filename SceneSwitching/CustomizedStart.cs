using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class CustomizedStart : MonoBehaviour
{
    public TMP_InputField maxHealthInput; // positive (100)
    public TMP_InputField maxShieldHealthInput; // positive (50)
    public TMP_InputField startingShieldHealthInput; // nonnegative (0)
    public Toggle alwaysInvincibleToggle; // t/f (f)
    public TMP_InputField baseRegenerationAmountInput; // nonnegative (0)
    public TMP_InputField regenerationIntervalInput; // positive (0.5)
    public TMP_InputField moveSpeedInput; // nonnegative (175)
    public TMP_InputField fireSpeedInput; // positive (10)
    public TMP_InputField bulletDamageInput; // nonnegative (1)
    public Toggle powerupStackingToggle; // t/f (f)
    public TMP_InputField meteorSpawnFrequencyInput; // nonnegative (4)
    public TMP_InputField meteorSpawnGuaranteeIntervalInput; // positive (20)
    public TMP_InputField primeBouncesInput; // >= 1 (4)
    public TMP_InputField meteorTimeLimitInput; // nonnegative (100)
    public TMP_InputField powerupSpawnFrequencyInput; // nonnegative (0.15)
    public TMP_InputField powerupSpawnGuaranteeIntervalInput; // positive (15)

    public bool SetGameSettings() {
        try {
            GameSettings.MAX_HEALTH = Int32.Parse(maxHealthInput.text);
            GameSettings.MAX_SHIELD_HEALTH = Int32.Parse(maxShieldHealthInput.text);
            GameSettings.STARTING_SHIELD_HEALTH = Int32.Parse(startingShieldHealthInput.text);
            GameSettings.ALWAYS_INVINCIBLE = alwaysInvincibleToggle.isOn;
            GameSettings.BASE_REGENERATION_AMOUNT = Int32.Parse(baseRegenerationAmountInput.text);
            GameSettings.REGENERATION_INTERVAL = Single.Parse(regenerationIntervalInput.text);
            GameSettings.MOVE_SPEED = Int32.Parse(moveSpeedInput.text);
            GameSettings.FIRE_SPEED = Int32.Parse(fireSpeedInput.text);
            GameSettings.BULLET_DAMAGE = Int32.Parse(bulletDamageInput.text);
            GameSettings.POWERUP_STACKING = powerupStackingToggle.isOn;

            // meteor
            GameSettings.METEOR_SPAWN_FREQUENCY = Single.Parse(meteorSpawnFrequencyInput.text);
            GameSettings.METEOR_SPAWN_GUARANTEE_INTERVAL = Single.Parse(meteorSpawnGuaranteeIntervalInput.text);
            GameSettings.PRIME_BOUNCES = Int32.Parse(primeBouncesInput.text);
            GameSettings.METEOR_TIME_LIMIT = Int32.Parse(meteorTimeLimitInput.text);

            // powerup
            GameSettings.POWERUP_SPAWN_FREQUENCY = Single.Parse(powerupSpawnFrequencyInput.text);
            GameSettings.POWERUP_SPAWN_GUARANTEE_INTERVAL = Single.Parse(powerupSpawnGuaranteeIntervalInput.text);
        }
        catch(FormatException e) {
            //Debug.Log("Entered an invalid value type. Perhaps you had invalid characters, such as letters symbols, or excess spaces.");
            return false;
        }
        catch(ArgumentNullException e) {
            //Debug.Log("One or more fields is empty.");
            return false;
        }
        catch(OverflowException e) {
            //Debug.Log("An entered number is too large.");
            return false;
        }
        return true;
    }

    public void BackButton() {
        SceneManager.LoadScene(1);
    }

    public void StartButton() {
        bool valid = SetGameSettings();
        if (valid) SceneManager.LoadScene(0);
    }
}
