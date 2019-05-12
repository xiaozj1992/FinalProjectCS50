using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeJump : MonoBehaviour
{
    private float jumpSpeed;
    //public float gravityDrag;

    // Collider Hit
    void OnTriggerExit2D(Collider2D other)
    {
        int jumpFloor = Random.Range(1, 4);

        if (jumpFloor == 2 || jumpFloor == 3) {
            jumpSpeed = CalculateJumpSpeed(jumpFloor-1, other.attachedRigidbody);
        }

        if (other.name.Contains("Ninja")){
            other.attachedRigidbody.velocity += new Vector2(0, jumpSpeed);
        }
    }

    // Calcualte Jumping Speed towards target floor. Offet .3f in X-axis, .6f in Y-axis.
    float CalculateJumpSpeed(int floor, Rigidbody2D self) {
        float time = (LevelGenerator.floorData[floor].x+.3f) / self.velocity.x;
        float height = LevelGenerator.floorData[floor].y + 0.6f - self.position.y;
        float speed = (height + 9.8f*time*time *.5f)/time;

        return speed;
    } 
}
