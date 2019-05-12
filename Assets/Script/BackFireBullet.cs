using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFireBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject onHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > 7.5) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // detect if collision is a Ninja
        if (collision.gameObject.layer == 8) {
            Instantiate(onHitEffect, collision.gameObject.transform.position, Quaternion.Euler(0,180,0));
            Destroy(collision.gameObject);
            LevelGenerator.enemyTokill -= 1;
        }
        // reset velocity
        rb.velocity = transform.right * speed;
    }
}
