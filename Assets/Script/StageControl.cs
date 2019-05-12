using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StageControl : MonoBehaviour
{
    private Text text;
    public static float countDown;
    public static bool isBetween = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        // Set to transparent 
        Color temp = text.color;
        temp.a = 0;
        text.color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelGenerator.isFailed) {
            // change scenes to Fail
            SceneManager.LoadScene("Failed");
        }

        // change to Between-level-Message
        if (isBetween) {
            GameObject targetObject = gameObject.transform.parent.transform.Find("BetweenLevel").gameObject;
            targetObject.SetActive(true);
            return;
        }
        if (LevelGenerator.incomingNinja.Count == 0) {
            // set to visible after the last Ninja Spawns.
            Color temp = text.color;
            temp.a = 1;
            text.color = temp;
            countDown -= Time.deltaTime;
            float timer = Mathf.Round(countDown * 10f)/10f;
            if (countDown < 0) {
                if (LevelGenerator.wave == 3) {
                    SceneManager.LoadScene("Success");
                } else {
                countDown = 0.0f;
                isBetween = true;
                // set TimerCountDown back to transparent
                temp = text.color;
                temp.a = 0;
                text.color = temp;
                    }
            }
            text.text = "Wave ENds in: " + timer;
        }
    }
}
