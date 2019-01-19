using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : VRTK.VRTK_InteractableObject
{

    private float timer = 0.0f;

    public Transform laser;

    public float fireDelay;
    protected VRTK.VRTK_ControllerEvents controllerEvents;
    public GameObject muzzle;

    // Use this for initialization

    public override void StartUsing(VRTK.VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        controllerEvents = currentUsingObject.GetComponent<VRTK.VRTK_ControllerEvents>();
    }

    public override void StopUsing(VRTK.VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        controllerEvents = null;
    }

    // Update is called once per frame
    protected override void Update () {
        timer += Time.deltaTime;
        if (controllerEvents.triggerPressed && timer >= fireDelay) {
            Shoot();
        }
	}

    void Shoot() {
        Debug.Log("Shooting");
        Instantiate(laser,muzzle.transform.position,muzzle.transform.rotation);
        timer = 0.0f;
    }
}
