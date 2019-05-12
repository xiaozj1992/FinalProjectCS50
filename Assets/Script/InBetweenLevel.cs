using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InBetweenLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // prevent player to control character
        PlayerControl.interactive = false;

        // Cleaning up remaining Ninjas
        GameObject[] ninjasleft = GameObject.FindGameObjectsWithTag("Ninja");
        for (int i = 0; i < ninjasleft.Length; i++) {
            Destroy(ninjasleft[i]);
            LevelGenerator.enemyTokill -= 1;
        }
        // press return to advance to next level
        if (Input.GetButtonDown("Submit")){
            // add 2 more ultimate bullets
            PlayerControl.Ammo += 2;

            // update wave number and generate new level
            LevelGenerator.wave += 1;
            LevelGenerator.GeneratingLevel(LevelGenerator.wave);
            // give player control back
            PlayerControl.interactive = true;
            // Turnoff inBetween State
            StageControl.isBetween = false;
            // Deactive this GameObject.
            gameObject.SetActive(false);
        }

    }
}
