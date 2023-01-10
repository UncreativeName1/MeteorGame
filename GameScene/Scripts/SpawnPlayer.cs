using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject pointer;
    public GameObject instantiatedPlayer;
    public GameObject instantiatedPointer;

    public Sprite playerSkin;

    // variables to be set
    public int maxHealth; // positive
    public int maxShieldHealth; // positive
    public int startingShieldHealth; // nonnegative
    public bool alwaysInvincible; // t/f
    public int baseRegenerationAmount; // nonnegative
    public float regenerationInterval; // positive
    public int moveSpeed; // nonnegative
    public int fireSpeed; // positive
    public int bulletDamage; // nonnegative
    public bool powerupStacking; // t/f

    void InitVariables() {
        maxHealth = GameSettings.MAX_HEALTH;
        maxShieldHealth = GameSettings.MAX_SHIELD_HEALTH;
        startingShieldHealth = GameSettings.STARTING_SHIELD_HEALTH;
        alwaysInvincible = GameSettings.ALWAYS_INVINCIBLE;
        baseRegenerationAmount = GameSettings.BASE_REGENERATION_AMOUNT;
        regenerationInterval = GameSettings.REGENERATION_INTERVAL;
        moveSpeed = GameSettings.MOVE_SPEED;
        fireSpeed = GameSettings.FIRE_SPEED;
        bulletDamage = GameSettings.BULLET_DAMAGE;
        powerupStacking = GameSettings.POWERUP_STACKING;
    }


    // do error handling here!!!!!!
    // bullet dmg dosnt work everythign else does
    void DefaultVariables() {
        // check for valid
        maxHealth = (maxHealth <= 0) ? 1 : maxHealth;
        maxShieldHealth = (maxShieldHealth <= 0) ? 1 : maxShieldHealth;
        startingShieldHealth = (startingShieldHealth < 0) ? 0 : startingShieldHealth;
        baseRegenerationAmount = (baseRegenerationAmount < 0) ? 0 : baseRegenerationAmount;
        regenerationInterval = (regenerationInterval <= 0) ? 0.5f : regenerationInterval;
        moveSpeed = (moveSpeed < 0) ? 0 : moveSpeed;
        fireSpeed = (fireSpeed <= 0) ? 1 : fireSpeed;
        bulletDamage = (bulletDamage < 0) ? 0 : bulletDamage;

        // set default
        player.GetComponent<Player>().maxHealth = maxHealth; //100
        player.GetComponent<Player>().SetHealth(maxHealth);
        player.GetComponent<Player>().maxShieldHealth = maxShieldHealth; // 50
        player.GetComponent<Player>().shieldHealth = 0;
        player.GetComponent<Player>().AddShield(startingShieldHealth); // 0
        player.GetComponent<Player>().isInvincible = alwaysInvincible; // false

        player.GetComponent<Player>().regenerationAmount = baseRegenerationAmount; // 0
        player.GetComponent<Player>().regenerationInterval = regenerationInterval; // 0.5f

        player.GetComponent<Player>().moveSpeed = moveSpeed; // 175
        player.GetComponent<Player>().fireSpeed = fireSpeed; // 10
        player.GetComponent<Player>().projectile.GetComponent<Bullet>().SetBulletDamage(bulletDamage); // 1

        player.GetComponent<Player>().powerupStacking = powerupStacking; // false

        player.GetComponent<Player>().powerupData = new List<PowerupType>();
        player.GetComponent<Player>().powerupTimers = new List<float>();
    }


    // Start is called before the first frame update
    void Start() {
        InitVariables();
        DefaultVariables();
        instantiatedPlayer = Instantiate(player, new Vector3(0, -146, 0), Quaternion.identity);
        instantiatedPlayer.GetComponent<Player>().playerImage.sprite = playerSkin;
        instantiatedPlayer.GetComponent<Player>().playerImage.color = new Color(255, 255, 255, 255);
        instantiatedPlayer.GetComponent<Player>().player = player;
        
        // player.GetComponent<Player>().SetHealth(player.GetComponent<Player>().maxHealth);

        instantiatedPointer = Instantiate(pointer, new Vector3(instantiatedPlayer.transform.position.x, pointer.GetComponent<Pointer>().pointerY, 0), Quaternion.identity);
        instantiatedPointer.GetComponent<Pointer>().player = instantiatedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
