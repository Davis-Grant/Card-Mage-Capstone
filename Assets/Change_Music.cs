//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Change_Music : MonoBehaviour
{

    public AudioClip BackgroundMusic;
    // Start is called before the first frame update
    void Start()    //changes music based on the scene
    {
        if(GameObject.Find("Audio"))
        {
            AudioSource audio = GameObject.Find("Audio").GetComponent<AudioSource>();
            audio.clip = BackgroundMusic;
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
