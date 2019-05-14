using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public static int Ammo = 3;
    public static int currentFloor = 2;
    public static int bulletsInAir = 10;
    public GameObject player;
    public Transform firePoint0;
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bulletPrefab;
    public GameObject ultBulletPrefab;
    public GameObject ultBullet2Prefab;

    public static bool interactive = true;
    private Vector2 currentPosition;
    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        // import floor positon data
        targetPosition = LevelGenerator.floorData[currentFloor - 1];
        float speed = 10f;
        LocationUpdate(targetPosition, speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Animation cannot be interrupted.
        if (!interactive) {
            return;
        }

        // Pause Controll
        if (LevelGenerator.pause) {
            return;
        }

        // -----< Movements >------
        // Primary Button for up
        if (Input.GetMouseButtonDown(0)) {
            //interactive = false;
            currentPosition = LevelGenerator.floorData[currentFloor - 1];
            currentFloor += 1;
            animator.Play("Player_JumpUp");
        }
        // Secondary Button for down
        if (Input.GetMouseButtonDown(1)) {
            //interactive = false;
            currentPosition = LevelGenerator.floorData[currentFloor - 1];
            currentFloor -= 1;
            animator.Play("Player_JumpDown");
        }
        // bound Floor number within [1,4] 
        if (currentFloor > 4) {
            currentFloor = 4;
        } else if (currentFloor < 1) {
            currentFloor = 1;
        }
        targetPosition = LevelGenerator.floorData[currentFloor - 1];
        // Jumping animation's length is 0.5 sec, divide distance with time to get speed.
        float speed = Vector2.Distance(currentPosition, targetPosition) / 1.0f;
        LocationUpdate(targetPosition, speed);
        

        // -----< Shooting >-----
        // Normal shooting by pressing space
        if (Input.GetKeyDown("space")) {
            //interactive = false;
            animator.Play("Player_Fire");
            Shoot();
        }

        // Ultimate Firing
        // adding constrain
        if (Input.GetKeyDown("z")){
            if (Ammo > 0) {
                //interactive = false;
                animator.Play("Player_UltFire");
                UltShoot();
                Ammo -= 1;
            }
        }
 
    }

    // offset player location at the beginning of the game.
    void LocationUpdate(Vector2 target, float speed)
    {
        // translate player to target location
        player.transform.position = Vector2.MoveTowards(player.transform.position, target, speed);

        // reset player velocity
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // Normal shooting
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint0.position, firePoint0.rotation);
        bulletsInAir -= 1;
    }

    // Ultimate shooting
    void UltShoot()
    {
        Instantiate(ultBulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(ultBullet2Prefab, firePoint2.position, firePoint2.rotation);
    }

    // input control
    public void gainControll()
    {
        interactive = true;
    }

}
