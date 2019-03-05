namespace VRTK.GrabAttachMechanics { 

using System;
    using UnityEngine;

public class Asteroid_ClimbableGrabAttatch : VRTK_BaseGrabAttach
{
    [Header("Climbable Settings", order = 2)]

    [Tooltip("Will respect the grabbed climbing object's rotation if it changes dynamically")]
    public bool useObjectRotation = false;

    protected override void Initialise()
    {
        tracked = false;
        climbable = true;
        kinematic = true;
    }

        public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
        {
            if (base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint))
            {
                SnapObjectToGrabToController(givenGrabbedObject);
                grabbedObjectScript.isKinematic = true;
                return true;
            }
            return false;
        }


        /// <summary>
        /// The StopGrab method ends the grab of the current Interactable Object and cleans up the state.
        /// </summary>
        /// <param name="applyGrabbingObjectVelocity">If `true` will apply the current velocity of the grabbing object to the grabbed object on release.</param>
        public override void StopGrab(bool applyGrabbingObjectVelocity)
        {
            ReleaseObject(applyGrabbingObjectVelocity);
            base.StopGrab(applyGrabbingObjectVelocity);
        }

        protected virtual void SetSnappedObjectPosition(GameObject obj)
        {
            if (grabbedSnapHandle == null)
            {
                obj.transform.position = controllerAttachPoint.transform.position;
            }
            else
            {
                obj.transform.rotation = controllerAttachPoint.transform.rotation * Quaternion.Inverse(grabbedSnapHandle.transform.localRotation);
                obj.transform.position = controllerAttachPoint.transform.position - (grabbedSnapHandle.transform.position - obj.transform.position);
            }
        }

        protected virtual void SnapObjectToGrabToController(GameObject obj)
        {
            if (!precisionGrab)
            {
                SetSnappedObjectPosition(obj);
            }
            obj.transform.SetParent(controllerAttachPoint.transform);
        }

    }

}

