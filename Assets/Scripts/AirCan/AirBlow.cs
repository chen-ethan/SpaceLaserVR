using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlow : VRTK.VRTK_InteractableObject
{

    protected VRTK.VRTK_ControllerEvents controllerEvents;
    public GameObject nozzle;
    public Rigidbody player;

    public float blowStrength;
    public float capacity;




    public override void StartUsing(VRTK.VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        controllerEvents = currentUsingObject.GetComponent<VRTK.VRTK_ControllerEvents>();
        //gets the cameraRig Object's rigidbody
        //Debug.Log("player - rigidbody = " + currentUsingObject.transform.parent.parent.name);
        player = currentUsingObject.transform.parent.parent.GetComponent<Rigidbody>();
    }

    public override void StopUsing(VRTK.VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        controllerEvents = null;
        player = null;

    }

    protected override void Update()
    {
        if (controllerEvents && player)
        {
            float power = controllerEvents.GetTriggerAxis();
            Blow(power);
            VRTK.VRTK_ControllerHaptics.TriggerHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(controllerEvents.gameObject), power * 0.25f, 0.1f, 0.01f);
        }
        /*else
        {
            Blow(0f);
        }
        */
    }

    protected void Blow(float power){
        Debug.Log("BS:" + blowStrength + "\t Power: " + power);
        player.AddForce(nozzle.transform.up * blowStrength * power,ForceMode.Force);
    }



}

