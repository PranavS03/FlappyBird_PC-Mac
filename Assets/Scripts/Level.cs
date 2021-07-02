using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float Camera_size=50f;
    private const float Pipe_Width=8.56f;
    private const float Pipe_head_height=3.75f;
    private const float pipe_speed=30f;
   

    private static Level instance;

    public static Level Getinstance(){
        return instance;
    }
    private List<Pipe> pipes;
    private float pipeSpawn;
    private float pipeSpawnMax;
    private float gapSize;
    private int pipesPassed;
    private State state;

    private enum State{
        Waiting,
        Playing,
        BirdDead,

    }




    private void Awake(){
        instance=this;
        pipes=new List<Pipe>();
        pipeSpawnMax=2f;
        gapSize=22f;
        state=State.Waiting;
    }
    private void Update(){
        if(state==State.Playing){
            handlePipes();
            handleSpawning();
            gapSize=Random.Range(17f,35f);
        }


    }
    private void handleSpawning(){
        pipeSpawn-=Time.deltaTime;
        if(pipeSpawn<0){
            pipeSpawn+=pipeSpawnMax;
            float heightEdgelim=10f;
            float minheight=gapSize*.5f+heightEdgelim;
            float ttlhgt=Camera_size*2f;
            float mxhgt=ttlhgt-gapSize*.5f-heightEdgelim;

            float height=Random.Range(minheight,mxhgt);

            CreateGapPipe(height,gapSize,150f);

        }
    }
    private void handlePipes(){
        for(int i=0; i<pipes.Count;i++){
            Pipe pipe =pipes[i];
            bool isRight=pipe.Getpos()>0;//here zero is position of bird on x axis 

            pipe.move();
            if(isRight && pipe.Getpos()<=0){
                pipesPassed++;

            }
            if(pipe.Getpos() <-120f){
                pipe.Dest();
                pipes.Remove(pipe);
                i--;

            }

        }
    }
    private void Start(){
        Bird.GetInstance().OnDeath+=Bird_OnDied;
        Bird.GetInstance().startedPlaying+=Bird_OnStartedPlaying;


    }
    private void Bird_OnStartedPlaying(object sender,System.EventArgs e){
        state=State.Playing;
    }
    private void Bird_OnDied(object sender,System.  EventArgs e){
         print("DEAD!!!");
         state=State.BirdDead;
        
         
    }
    
    private void CreateGapPipe(float gapY, float gapSize, float xPosition){
        CreatePipe(gapY-gapSize*.5f,xPosition,true);
        CreatePipe(Camera_size*2f-gapY-gapSize*.5f,xPosition,false);



    }
   private void CreatePipe(float height,float xPosition, bool createBottom){
       //pipe head
       Transform pipeHead=Instantiate(GameAssets.GetInstance().pfPipeHead);
       float headYpos;
       if(createBottom){
           headYpos=-Camera_size+height-Pipe_head_height*.5f;
       }
       else
       {
           headYpos=+Camera_size-height+Pipe_head_height*.5f;
       }

       pipeHead.position=new Vector3(xPosition,headYpos );
       

       //pipe body

       Transform pipeBody=Instantiate(GameAssets.GetInstance().pfPipeBody);
       float bodyYpos;
       if(createBottom){
           bodyYpos=-Camera_size;
       }
       else
       {
           bodyYpos=+Camera_size;
           pipeBody.localScale=new Vector3(1,-1,1);
       }
       pipeBody.position=new Vector3(xPosition,bodyYpos);
       


       SpriteRenderer pipeBodySpriterenderer=pipeBody.GetComponent<SpriteRenderer>();
       pipeBodySpriterenderer.size=new Vector2(Pipe_Width,height);

       BoxCollider2D pipeBodyBoxCollider=pipeBody.GetComponent<BoxCollider2D>();
       pipeBodyBoxCollider.size=new Vector2(Pipe_Width,height);
       pipeBodyBoxCollider.offset=new Vector2(0f,height*.5f);

       Pipe pipe=new Pipe(pipeHead,pipeBody);   
       pipes.Add(pipe);
   }

   public int GetPipesPassed(){
       return pipesPassed/2;
   }
   ///Representing single pipe
   private class Pipe{
       private Transform pipeHeadTrans;
       private Transform pipeBodyTrans;
       public Pipe(Transform pipeHeadTrans,Transform pipeBodyTrans){
           this.pipeBodyTrans=pipeHeadTrans;
           this.pipeHeadTrans=pipeBodyTrans;
       }
       public void move(){
           pipeHeadTrans.position+= new Vector3(-1,0,0)*pipe_speed*Time.deltaTime;
           pipeBodyTrans.position+= new Vector3(-1,0,0)*pipe_speed*Time.deltaTime;

       }
       public float Getpos(){
           return pipeHeadTrans.position.x;

       }
       public void Dest(){
           Destroy(pipeHeadTrans.gameObject);
           Destroy(pipeBodyTrans.gameObject);
       }
   }
}
