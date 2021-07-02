using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bird : MonoBehaviour
{
    private const float JUMP = 75f;

    public event EventHandler OnDeath;
    public event EventHandler startedPlaying;

    private static Bird instance;
    public static Bird GetInstance(){
        return instance;
    }
    private State state;
    private enum State{
        Waiting,
        Playing,
        Dead
    }
    private Rigidbody2D birdrigidbody2D;
    private void Awake()
    {
        
        instance=this;
        birdrigidbody2D = GetComponent<Rigidbody2D>();
        state=State.Waiting;
        birdrigidbody2D.bodyType=RigidbodyType2D.Static;

        
    }
    private void Update()
    {
        switch(state){
            default:
            case State.Waiting:
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)){
                state=State.Playing;
                birdrigidbody2D.bodyType=RigidbodyType2D.Dynamic;
                Jump();
                if(startedPlaying!=null) startedPlaying(this,EventArgs.Empty);      

                }
            break;
            case State.Playing:
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
            {
                Jump();
            }
            transform.eulerAngles=new Vector3(0,0,birdrigidbody2D.velocity.y *.2f);
            break;
            case State.Dead:
            break;
        }
        
        

        
    }
    private void Jump()
    {
        birdrigidbody2D.velocity = Vector2.up * JUMP;
        SoundManager.playsound();

    }
    private void OnTriggerEnter2D(Collider2D collider){
       
        birdrigidbody2D.bodyType=RigidbodyType2D.Static;
        if (OnDeath!=null) OnDeath(this,EventArgs.Empty);
        SoundManager.playsound2();


        

    }
    
}
