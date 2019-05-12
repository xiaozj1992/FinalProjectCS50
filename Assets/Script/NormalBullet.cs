using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject onHitEffect;
    public bool isNormal;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -7.5) {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(onHitEffect, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(collision.gameObject);
        LevelGenerator.enemyTokill -= 1;
        if (isNormal) {
            Destroy(gameObject);
        } else {
            // reset bullet speed after collision.
            rb.velocity = transform.right * speed;
        }
    }
}
