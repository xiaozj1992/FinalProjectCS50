using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int wave;                                         //Current Wave 
    public static Vector2[] floorData;                              // Floor Position Data
    public static bool pause;                                       // pause control for Ninja Generation
    public static int enemyTokill;                                  // Number of enemies on the field
    public static int[] occupiedfloor = new int[] { 0, 0, 0, 0 };   // if a floor is occupied, then the index turns to 1.

    public static Queue<int[]> incomingNinja = new Queue<int[]>();  //Remaining Ninjas to spawn
    private static Queue<float> timeTable = new Queue<float>();     // Time table for spawning Ninjas.
    public static float finalCountDown = 2.0f;                      // Timer count down for failure.
    public static bool isFailed = false;                            // State Control for failure
    public static bool waveCleared = false;                         // State Control for success

    private float endWaveTimer = 10.0f;                              // Secs to last until current level ends.
    // If two Ninjas with the same type in the same location were scheduled to spawn in a same, put a delay between them.
    public float delay = 0.5f; 

    // Spawning operators
    private int[] currentNinja;
    private int[] previousNinja;
    private float currentTimeStamp;
    private bool rdy2Spawn = false;


    // Spawning Objects
    public GameObject normalNinja;
    public GameObject flyingNinja;

    void Awake()
    {
        // Initializing
        wave = 1;
        GeneratingLevel(wave);
        floorData = new Vector2[] { new Vector2(3.7082f, -3.83f), new Vector2(3.75f, -2.048f), new Vector2(4.12f, -0.295f), new Vector2(4.62f, 1.59f) };

        StageControl.countDown = endWaveTimer;
        // Prevents player colliding with Ninja
        Physics2D.IgnoreLayerCollision(8, 9);

        // Prevents Ninja colliding with Ninja
        Physics2D.IgnoreLayerCollision(8, 8);

        // Prevents bullet colliding with bullet.
        Physics2D.IgnoreLayerCollision(10, 10);
        // Prevents bullet colliding with checkpoint.
        Physics2D.IgnoreLayerCollision(10, 11);
    }

    // Update is called once per frame
    void Update()
    {
        // geting timing data
        if (!rdy2Spawn && incomingNinja.Count != 0) {
            currentTimeStamp = timeTable.Dequeue();
            currentNinja = incomingNinja.Dequeue();
            // add delay if two Ninja are too close to each other.
            if (previousNinja != null) {
                if (currentNinja[1] == previousNinja[1] && currentTimeStamp < 0.5f) {
                    currentTimeStamp += delay;
                }
            }
            rdy2Spawn = true;
        }


        // spawn a Ninja if Timer is up
        if (currentTimeStamp <= 0 && rdy2Spawn) {
            Vector2 spawningLocation = new Vector2(-9.0f, floorData[currentNinja[1]-1].y);
            if (currentNinja[0] == 0) {
                //"Spawn Normal"
                Instantiate(normalNinja, spawningLocation, Quaternion.Euler(0, 0, 0));
            } else {
                // "Spwan Fly"
                Instantiate(flyingNinja, spawningLocation, Quaternion.Euler(0, 0, 0));
            }
            previousNinja = currentNinja;
            rdy2Spawn = false;
        } else {
            // iterate timer countdown
            currentTimeStamp = currentTimeStamp - Time.deltaTime;
        }

        // Failure check
        int floorOccupied = 0;
        for (int i = 0; i <= 3; i++) {
            floorOccupied += occupiedfloor[i];
        }
        if (floorOccupied == 4) {
            finalCountDown -= Time.deltaTime;
        }
        if (finalCountDown < 0) {
            isFailed = true;
        }

        // End Game Check
        // Might be useless since StageControl is taking over.
        if (incomingNinja.Count == 0) {
            endWaveTimer -= Time.deltaTime;
            if (endWaveTimer < 0) {
                waveCleared = true;
            }
        }
    }

    // Genrate incoming Ninja data
    public static void GeneratingLevel(int wave) {
        // Total # of Ninja
        int ninjaCount = 10 + wave * 5;
        enemyTokill = ninjaCount;
        for (int i = 0; i < ninjaCount; i++) {
            // Data Structure:  isFly   Floor#
            // Data          :  [0,1]   [1,4]
            int isFly = Random.Range(0, 2);
            int floorNum = Random.Range(1, 5);

            // Normal Ninja will only spawn on the ground
            if (isFly == 0) {
                floorNum = 1;
            }
            // If a Ninja is spwan on the ground, it could only be the normal one
            if (floorNum == 1) {
                isFly = 0;
            }

            int[] nextNinja = new int[] { isFly, floorNum };
            incomingNinja.Enqueue(nextNinja);
            timeTable.Enqueue((Random.Range(0.5f, 2.0f)));
        }
    }
}
