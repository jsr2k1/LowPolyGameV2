using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVRTeleporterController : MonoBehaviour
{
    public VRTeleporter teleporter;
    bool aiming = false;

	void Update ()
    {
        if(!aiming) {
            if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0) {
                aiming = true;
                teleporter.ToggleDisplay(true);
            }
        }
        else {
            if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0) {
                aiming = false;
                teleporter.Teleport();
                teleporter.ToggleDisplay(false);
            }
        }
	}
}
