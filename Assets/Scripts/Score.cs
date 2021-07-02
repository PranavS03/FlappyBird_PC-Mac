using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {

    public static void Start() {
        //ResetHighscore();
        Bird.GetInstance().OnDeath += Bird_OnDied;
    }

    private static void Bird_OnDied(object sender, System.EventArgs e) {
        TrySetNewHighscore(Level.Getinstance().GetPipesPassed());
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("HighScore");
    }

    public static bool TrySetNewHighscore(int Score) {
        int currentHighscore = GetHighscore();
        if (Score > currentHighscore) {
            // New Highscore
            PlayerPrefs.SetInt("HighScore", Score);
            PlayerPrefs.Save();
            return true;
        } else {
            return false;
        }
    }

    public static void ResetHighscore() {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
    }
}
