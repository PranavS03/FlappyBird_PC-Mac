using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private Text highsc;
    private Text ScoreText;
    private void Awake(){
        ScoreText=transform.Find("ScoreText").GetComponent<Text>();
        highsc=transform.Find("highsc").GetComponent<Text>();

    }
    private void Start(){
        highsc.text="HighScore: "+Score.GetHighscore().ToString();
    }
    private void Update(){
        ScoreText.text  =Level.Getinstance().GetPipesPassed().ToString();



    }
}
