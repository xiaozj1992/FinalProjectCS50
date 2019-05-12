using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaMovement : MonoBehaviour
{
    public Rigidbody2D rg;
    public Animator animator;
    public float ninjaVelocity;
    private bool isReached;
    private int reachedFloor;

    // Start is called before the first frame update
    void Start()
    {
        rg.velocity = transform.right * ninjaVelocity ;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null) {
            animator.SetFloat("yVelocity", rg.velocity.y);
        }
    }

    // Call upon reaching the floor's end.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If Ninja has already reached the end, skip
        if (isReached) {
            return;
        }
        if (collision.collider.name.Contains("Floor")){
            rg.velocity = new Vector2(0, 0);
            isReached = true;
            if (collision.collider.name.Contains("1")) {
                EndPointCheck(1);
            } else if (collision.collider.name.Contains("2")) {
                EndPointCheck(2);
            } else if (collision.collider.name.Contains("3")) {
                EndPointCheck(3);
            } else {
                EndPointCheck(4);
            }
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            animator.Play("Celi02");

            // Choose the animation to play
            if (reachedFloor == 1) {
                animator.Play("Celi01");
            } else {
                animator.Play("Celi02");
            }
        }  
    }

    void EndPointCheck(int floorNumber)
    {
        // if current floor is not occupied
        if (LevelGenerator.occupiedfloor[floorNumber - 1] == 0) {
            LevelGenerator.occupiedfloor[floorNumber - 1] = 1;
            reachedFloor = floorNumber;
        } else {
        // if current floor IS occupied, occupy the lowest floor possible.
            for (int i = 1; i <= 4; i++) {
                if (LevelGenerator.occupiedfloor[i - 1] == 0) {
                    LevelGenerator.occupiedfloor[i - 1] = 1;
                    reachedFloor = i;
                    isReached = true;
                    //Relocate(i);
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, LevelGenerator.floorData[i - 1].y);
                    break;
                }
            }
        }

        // Mark occupied position
        if (reachedFloor > 0) {
            LevelGenerator.occupiedfloor[reachedFloor - 1] = 1;
        }
    }

    void OnDestroy()
    {
        // reset Marker.
        if (isReached) {
            if (reachedFloor - 1 >= 0) {
                LevelGenerator.occupiedfloor[reachedFloor - 1] = 0;
            }
            // reset countdown timer.
            LevelGenerator.finalCountDown = 2.0f;
        }
    }
}
