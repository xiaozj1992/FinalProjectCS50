using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tryagain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit")){
            //Could not get following codes to work.
            //GetComponents<AudioSource>()[0].Play();
            SceneManager.LoadScene("Main");
            LevelGenerator.isFailed = false;
            PlayerControl.Ammo = 3;
            LevelGenerator.wave = 1;
        }
    }
}
