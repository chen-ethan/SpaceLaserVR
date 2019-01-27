using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : VRTK.VRTK_InteractableObject {

    private float timer = 0.0f;


    //public Transform laser;

    public float fireDelay;
    public float range;
    public float dmg;
    public float laserDuration;
    protected VRTK.VRTK_ControllerEvents controllerEvents;
    public GameObject muzzle;

    public ParticleSystem muzzleFlash;

    public LineRenderer laserLine;


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

    protected override void Update () {
        timer += Time.deltaTime;
        if (controllerEvents && controllerEvents.triggerPressed && timer >= fireDelay) {
            Shoot();
        }
	}

    void Shoot() {
        Debug.Log("Shooting");
        muzzleFlash.Play();
        laserLine.SetPosition(0, muzzle.transform.position);
        //Instantiate(laser,muzzle.transform.position,muzzle.transform.rotation);
        StopCoroutine(ShotEffect());
        StartCoroutine(ShotEffect());
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target t = hit.transform.GetComponent<Target>();
            if (t)
            {
                t.TakeDamage(dmg);
            }
            //ParticleSystem flash = Instantiate(muzzleFlash, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(flash,1f);
            laserLine.SetPosition(1, hit.point);
            Debug.Log("Hit something");


        }
        else
        {
            laserLine.SetPosition(1, muzzle.transform.position + (muzzle.transform.forward * range));
            Debug.Log("Missed");

        }
        timer = 0.0f;
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        Debug.Log("Destroy laser");
        //Destroy(laserLine);
        laserLine.enabled = false;
    }
}
