using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirBlow : VRTK.VRTK_InteractableObject
{

    protected VRTK.VRTK_ControllerEvents controllerEvents;
    public GameObject nozzle;
    public Rigidbody player;

    protected GameObject smoke;
    protected ParticleSystem particles;

    public float maxSprayPower = .5f;
    public float minPower = .5f;
    public float blowStrength;
    public float capacity;


    
    
    public override void StartUsing(VRTK.VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        controllerEvents = currentUsingObject.GetComponent<VRTK.VRTK_ControllerEvents>();
        //gets the cameraRig Object's rigidbody
        //Debug.Log("player - rigidbody = " + currentUsingObject.transform.parent.parent.name);
        player = currentUsingObject.transform.parent.parent.GetComponent<Rigidbody>();
        smoke = transform.Find("Smoke").gameObject;
        particles = smoke.GetComponent<ParticleSystem>();
        particles.Play();
        //particles.Stop();
    }

    public override void StopUsing(VRTK.VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        controllerEvents = null;
        player = null;
        particles.Stop();


    }

    protected override void Update()
    {
        if (controllerEvents && player)
        {
            float power = controllerEvents.GetTriggerAxis();
            Blow(power);
            VRTK.VRTK_ControllerHaptics.TriggerHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(controllerEvents.gameObject), power * 0.25f, 0.1f, 0.01f);
        }
        else
        {
            Blow(0f);
        }

    }

    protected void Blow(float power){
        //Debug.Log("BS:" + blowStrength + "\t Power: " + power);
        if (power > 0)
        {
            player.AddForce(-smoke.transform.forward * blowStrength * power, ForceMode.Force);
            if (particles.isPaused || particles.isStopped)
            {
                particles.Play();
            }
            ParticleSystem.MainModule mainModule = particles.main;
            mainModule.startSpeedMultiplier = Math.Max(maxSprayPower * power,minPower);
        } /*else if(player) 
        {
            particles.Stop();

        }*/

    }



}

