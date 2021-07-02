using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;






public class GameOverWindow : MonoBehaviour
{
    private Text sc;
    private Text hsc;

    private void Awake(){
        sc=transform.Find("sc").GetComponent<Text>();
        hsc=transform.Find("hsc").GetComponent<Text>();

        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); };
        transform.Find("Menu").GetComponent<Button_UI>().ClickFunc = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); };
        
        }
    private void Start(){
        Bird.GetInstance().OnDeath += Bird_OnDeath;
        hide();
        }
    private void Bird_OnDeath(object sender, System.EventArgs e){
        sc.text=Level.Getinstance().GetPipesPassed().ToString();
        hsc.text="HighScore: "+ Score.GetHighscore();
        Show();
        
        }
    public void Show(){
        gameObject.SetActive(true);

    }

    public void hide(){
        gameObject.SetActive(false);
    } 
   
    
}
