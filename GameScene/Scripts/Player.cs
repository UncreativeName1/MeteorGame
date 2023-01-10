using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// ALWAYS DO ACTIONS TO THE TEMPLATE PLAYER NOT THE INSTANITATED PREFAB THIS IS ATTACHED TO.

// remove powerup stacking


public class Player : MonoBehaviour {
    public GameObject player;
    public GameObject projectile;

    // public Collider2D meteor;
    Camera mainCam;
    Tuple<float, float> movementBounds;

    public int maxHealth;
    public int playerHealth;
    public int maxShieldHealth;
    public int shieldHealth;
    public bool isInvincible;
    public int regenerationAmount;
    public float regenerationInterval;
    public float moveSpeed;
    public float fireSpeed;
    public bool powerupStacking = false;
    
    float regenerationTracker = 0;
    float canFire = 0f;

    public List<PowerupType> powerupData = new List<PowerupType>();
    // while this is greater than Time.timeSinceLevelLoad powerup is active
    public List<float> powerupTimers = new List<float>();
    // powerup icons
    public List<Sprite> powerupIcons = new List<Sprite>();

    GameObject instantiatedProjectile;

    public GameObject explosion;
    public Image fireExplosion;
    float showFireExplosionTime;
    public Image playerImage;
    public Sprite bulletSkin;

    public bool gameActive = true;
    bool playerAlive = true;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start() {
        player.GetComponent<Player>().gameActive = true;
        player.GetComponent<Player>().playerAlive = true;
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        movementBounds = new Tuple<float, float> (-1 * mainCam.orthographicSize * mainCam.aspect + player.transform.localScale.x / 2, mainCam.orthographicSize * mainCam.aspect - player.transform.localScale.x / 2);
        //Debug.Log(player.transform.localScale.x / 2);

        // setting default values based on what the player starts as (which can be set in DefaultVariables() above)
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Speed", player.GetComponent<Player>().moveSpeed, 70, 8));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Damage", player.GetComponent<Player>().projectile.GetComponent<Bullet>().GetBulletDamage(), 1, 8));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("FireSpeed", player.GetComponent<Player>().fireSpeed, 5, 8));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Shield", player.GetComponent<Player>().shieldHealth, 25, 999999));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Invincibility", player.GetComponent<Player>().isInvincible ? 1 : 0, 0, 8));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Regeneration", player.GetComponent<Player>().regenerationAmount, 5, 5));
        player.GetComponent<Player>().powerupData.Add(new PowerupType("Healing", 0, 15, 0));

        powerupData = player.GetComponent<Player>().powerupData;

        //Debug.Log(powerupData[4].defaultValue);

        // //Debug.Log(player.GetComponent<Player>().powerupData.Count);

        for (int i = 0; i < player.GetComponent<Player>().powerupData.Count; i++) player.GetComponent<Player>().powerupTimers.Add(0);
    }

    // Update is called once per frame
    void Update() {
        if (gameActive) {
            if (playerAlive) {
                Movement();
                Fire();
                if (player.GetComponent<Player>().regenerationAmount > 0 && Time.timeSinceLevelLoad > regenerationTracker) {
                    // //Debug.Log("got here");
                    regenerationTracker = Time.timeSinceLevelLoad + player.GetComponent<Player>().regenerationInterval;
                    player.GetComponent<Player>().Heal(player.GetComponent<Player>().regenerationAmount);
                }
                EndPowerups();
                Debug.Log("CURRENT POS: (" + transform.position.x + ", " + transform.position.y + ")");
            } else {
                PlayerDeath();
            }
        }
        if (showFireExplosionTime < Time.timeSinceLevelLoad) {
            fireExplosion.enabled = false;
        }
        // instantiated = template version
        UpdateVariables();
        ////Debug.Log(playerHealth);
    }

    void UpdateVariables() {
        playerHealth = player.GetComponent<Player>().playerHealth;
        shieldHealth = player.GetComponent<Player>().shieldHealth;
        moveSpeed = player.GetComponent<Player>().moveSpeed;
        fireSpeed = player.GetComponent<Player>().fireSpeed;
        isInvincible = player.GetComponent<Player>().isInvincible;
        regenerationAmount = player.GetComponent<Player>().regenerationAmount;
        powerupStacking = player.GetComponent<Player>().powerupStacking;

        powerupTimers = player.GetComponent<Player>().powerupTimers;
        gameActive = player.GetComponent<Player>().gameActive;
        playerAlive = player.GetComponent<Player>().playerAlive;
    }

    public int GetCurrentHealth() {
        return playerHealth;
    }

    public int GetCurrentShieldHealth() {
        return shieldHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetMaxShieldHealth() {
        return maxShieldHealth;
    }

    void ShowFireExplosion(float durationToShow) {
        showFireExplosionTime = Time.timeSinceLevelLoad + durationToShow;
        fireExplosion.enabled = true;
    }

    void Fire() {
        if (Input.GetButton("Fire1") && Time.timeSinceLevelLoad > canFire) {
            ShowFireExplosion(0.05f);
            canFire = Time.timeSinceLevelLoad + (1 / fireSpeed);
            instantiatedProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + GetComponent<RectTransform>().rect.height * transform.localScale.y / 2, 0), Quaternion.identity);
            //Debug.Log("x: " + transform.position.x + "y: " + (transform.position.y + GetComponent<RectTransform>().rect.height * transform.localScale.y / 2) + "z: " + 0);
            instantiatedProjectile.GetComponent<Bullet>().setImage.sprite = bulletSkin;
        }
    }

    void Movement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(moveX, 0);
        float addX = moveDirection.x * player.GetComponent<Player>().moveSpeed * Time.deltaTime;
        transform.position += new Vector3(addX, 0f, 0f);
        if (transform.position.x < movementBounds.Item1) {
            transform.position = new Vector3(movementBounds.Item1, transform.position.y, transform.position.z);
        } 
        if (transform.position.x > movementBounds.Item2) {
            transform.position = new Vector3(movementBounds.Item2, transform.position.y, transform.position.z);
        }
    }

    void PlayerDeath() {
        player.GetComponent<Player>().gameActive = false;
        Debug.Log("WILL INSTANTIATE: (" + transform.position.x + ", " + transform.position.y + ")");
        Instantiate(explosion, transform.position, Quaternion.identity);
        player.GetComponent<Player>().moveSpeed = 0;
        player.GetComponent<Player>().fireSpeed = 0;
        playerImage.color = new Color(0, 0, 0, 255);
    }

    public void SetHealth(int healthValue) {
        player.GetComponent<Player>().playerHealth = healthValue;
    }

    public void TakeDamage(int damage) {
        if (player.GetComponent<Player>().isInvincible) return;
        // otherwise not invincible
        // //Debug.Log("Old player health: " + playerHealth);
        player.GetComponent<Player>().playerHealth -= damage;
        if (player.GetComponent<Player>().playerHealth < 0) {
            player.GetComponent<Player>().playerHealth = 0;
            playerAlive = false;
        }
        // //Debug.Log("DAMAGE TAKEN: " + damage);
        //Debug.Log("New player health: " + playerHealth);
    }

    public void Heal(int healAmount) {
        player.GetComponent<Player>().playerHealth += healAmount;
        if (player.GetComponent<Player>().playerHealth > player.GetComponent<Player>().maxHealth) {
            player.GetComponent<Player>().playerHealth = player.GetComponent<Player>().maxHealth;
        }
    }

    public void DamageShield(int damage) {
        player.GetComponent<Player>().shieldHealth -= damage;
        if (player.GetComponent<Player>().shieldHealth < 0) {
            player.GetComponent<Player>().shieldHealth = 0;
        }
    }

    public void AddShield(int shieldAmount) {
        player.GetComponent<Player>().shieldHealth += shieldAmount;
        if (player.GetComponent<Player>().shieldHealth > player.GetComponent<Player>().maxShieldHealth) {
            player.GetComponent<Player>().shieldHealth = player.GetComponent<Player>().maxShieldHealth;
        }
    }


    // ******************* POWERUPS ********************

    public void EndPowerups() {
        if (player.GetComponent<Player>().powerupTimers[0] < Time.timeSinceLevelLoad) SpeedReset();
        if (player.GetComponent<Player>().powerupTimers[1] < Time.timeSinceLevelLoad) DamageReset();
        if (player.GetComponent<Player>().powerupTimers[2] < Time.timeSinceLevelLoad) FireSpeedReset();
        // if (player.GetComponent<Player>().powerupTimers[3] < Time.timeSinceLevelLoad) ShieldReset();
        if (player.GetComponent<Player>().powerupTimers[4] < Time.timeSinceLevelLoad) InvincibilityReset();
        if (player.GetComponent<Player>().powerupTimers[5] < Time.timeSinceLevelLoad) RegenerationReset();
    }

    // float
    public void SpeedBoost(int level, float duration) {
        // either u can stack or no powerup is present
        if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[0] < Time.timeSinceLevelLoad) player.GetComponent<Player>().moveSpeed += level * player.GetComponent<Player>().powerupData[0].levelModifier;
        player.GetComponent<Player>().powerupTimers[0] = Time.timeSinceLevelLoad + duration;
    }
    public void SpeedReset() {
        player.GetComponent<Player>().moveSpeed = player.GetComponent<Player>().powerupData[0].defaultValue;
    }

    // int
    public void DamageBoost(int level, float duration) {
        if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[1] < Time.timeSinceLevelLoad) player.GetComponent<Player>().projectile.GetComponent<Bullet>().ModifyBulletDamage(level * (int)player.GetComponent<Player>().powerupData[1].levelModifier);
        player.GetComponent<Player>().powerupTimers[1] = Time.timeSinceLevelLoad + duration;
    }
    public void DamageReset() {
        player.GetComponent<Player>().projectile.GetComponent<Bullet>().SetBulletDamage((int)player.GetComponent<Player>().powerupData[1].defaultValue);
    }

    // float
    public void FireSpeedBoost(int level, float duration) {
        if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[2] < Time.timeSinceLevelLoad) player.GetComponent<Player>().fireSpeed += level * player.GetComponent<Player>().powerupData[2].levelModifier;
        player.GetComponent<Player>().powerupTimers[2] = Time.timeSinceLevelLoad + duration;
    }
    public void FireSpeedReset() {
        player.GetComponent<Player>().fireSpeed = player.GetComponent<Player>().powerupData[2].defaultValue;
    }

    // int
    public void Shield(int level, float duration = 0) {
        // if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[3] < Time.timeSinceLevelLoad) 
        player.GetComponent<Player>().AddShield(level * (int)player.GetComponent<Player>().powerupData[3].levelModifier); //+= (int)player.GetComponent<Player>().powerupData[3].levelModifier;
        // player.GetComponent<Player>().powerupTimers[3] = Time.timeSinceLevelLoad + duration;
    }
    public void ShieldReset() {
        player.GetComponent<Player>().shieldHealth = (int)player.GetComponent<Player>().powerupData[3].defaultValue;
    }

    // bool
    public void Invincibility(int level, float duration) {
        if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[4] < Time.timeSinceLevelLoad) player.GetComponent<Player>().isInvincible = true;
        player.GetComponent<Player>().powerupTimers[4] = Time.timeSinceLevelLoad + duration;
    }
    public void InvincibilityReset() {
        player.GetComponent<Player>().isInvincible = (player.GetComponent<Player>().powerupData[4].defaultValue == 1) ? true : false;
    }

    // int
    public void Regeneration(int level, float duration) {
        if (player.GetComponent<Player>().powerupStacking || player.GetComponent<Player>().powerupTimers[5] < Time.timeSinceLevelLoad) player.GetComponent<Player>().regenerationAmount += level * (int)player.GetComponent<Player>().powerupData[5].levelModifier;
        player.GetComponent<Player>().powerupTimers[5] = Time.timeSinceLevelLoad + duration;
    }
    public void RegenerationReset() {
        player.GetComponent<Player>().regenerationAmount = (int)player.GetComponent<Player>().powerupData[5].defaultValue;
    }

    // int
    public void Healing(int level, float duration = 0) {
        player.GetComponent<Player>().Heal(level * (int)player.GetComponent<Player>().powerupData[6].levelModifier);
    }
}

public class PowerupType {
    public float defaultValue {get; set;}
    public string name {get; set;}
    public float levelModifier {get; set;}
    public float duration {get; set;}

    public PowerupType(string setName, float setDefaultValue, float setLevelModifier, float setDuration) {
        name = setName;
        defaultValue = setDefaultValue;
        levelModifier = setLevelModifier;
        duration = setDuration;
    }
}

