using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  SoundManager 
{
    public static void playsound(){
        GameObject gameObject=new GameObject("Sound",typeof(AudioSource));
        AudioSource audioSource=gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GameAssets.GetInstance().birdJump);

    }
    public static void playsound2(){
        GameObject gameObject=new GameObject("Sound",typeof(AudioSource));
        AudioSource audioSource=gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GameAssets.GetInstance().lost);

    }

}
