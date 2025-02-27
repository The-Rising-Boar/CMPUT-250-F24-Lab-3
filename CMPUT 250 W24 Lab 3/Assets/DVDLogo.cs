﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDLogo : MonoBehaviour
{
    //Speed it moves at
    public float speed = 7;

    //Bounds of the screen (could get these with camera bounds but we can do this since it's a fixed camera)
    public float X_Max = 5, Y_Max = 4;

    //Current direction
    private Vector3 direction;

    // Rotate Ball
    public Transform Ball;
    public float turnDegree, degreeIncrement;

    //Animation
    public Animator anim;

    // Bounce Sound
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly initialize direction
        direction = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f));
        direction.Normalize();

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void FlipDirectionX(){
        audioSource.Play();
        
        direction.x*=-1;
        direction.x+= Random.Range(-0.1f,0.1f);
        direction.y+= Random.Range(-0.1f,0.1f);
        direction.Normalize();

        degreeIncrement = 0-degreeIncrement;
        bounceCount.instance.Addpoint();

        anim.ResetTrigger("BallNone");
        anim.SetTrigger("BallBounce");
        

        if (speed < 60){speed += .5f;}
        if (degreeIncrement < 20){
            if (degreeIncrement >= 0) {
                degreeIncrement += .4f;
            } else {
                degreeIncrement -= .4f;
            }
        }
    }

    private void FlipDirectionY(){
        audioSource.Play();

        direction.y*=-1;
        direction.x+= Random.Range(-0.1f,0.1f);
        direction.y+= Random.Range(-0.1f,0.1f);
        direction.Normalize();

        degreeIncrement = 0-degreeIncrement;
        bounceCount.instance.Addpoint();
        
        anim.ResetTrigger("BallNone");
        anim.SetTrigger("BallBounce");


        if (speed < 60){speed += .5f;}
        if (degreeIncrement < 20){
            if (degreeIncrement >= 0) {
                degreeIncrement += .4f;
            } else {
                degreeIncrement -= .4f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move in direction unless we'd go out of bounds, if so bounce with some randomness

        Vector3 newPosition = transform.position + direction*Time.deltaTime*speed;

        Ball.transform.rotation = Quaternion.Euler(Vector3.forward * turnDegree);
        turnDegree += degreeIncrement;

        //See if a bounce needs to happen before moving
        if (newPosition.x>X_Max){
            FlipDirectionX();
        }
        else if (newPosition.x<-1*X_Max){
            FlipDirectionX();
        }

        if (newPosition.y>Y_Max){
            FlipDirectionY();
        }
        else if (newPosition.y<-1*Y_Max){
            FlipDirectionY();
        }
        else {
            // anim.ResetTrigger("Ball Squash");
            anim.SetTrigger("BallNone");
        }

        transform.position += direction*Time.deltaTime*speed;
    }
}
