using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody2D bulletRigid; 

    public float moveSpeed;
    public int bulletDamage = 1;

    public float heightLimit;

    public Image setImage;

    public int GetBulletDamage() {
        return bulletDamage;
    }

    public void SetBulletDamage(int damage) {
        bulletDamage = damage;
    }

    public void ModifyBulletDamage(int change) {
        bulletDamage += change;
    }

    void OnCollisionEnter2D(Collision2D collision) { 
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        if (collision.gameObject.tag == "Circle") {
            Destroy(bullet);
        }
        // Debug.Log("circle collide");
    }

    // Start is called before the first frame update
    void Start() {
        bulletRigid.velocity = new Vector3(0, moveSpeed, 0);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y > heightLimit) {
            Destroy(bullet);
        }
    }
}