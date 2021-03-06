using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionCtrl : MonoBehaviour
{
    public GameObject objectReticle;
    public XRController rightControllerRay;
    public InputHelpers.Button teleportActionButton;
    public float activationThreshold = 0.1f;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(rightControllerRay) {
            rightControllerRay.gameObject.SetActive(CheckActivated(rightControllerRay));
            objectReticle.SetActive(CheckActivated(rightControllerRay));
        }

        if(InputHelpers.IsPressed(rightControllerRay.inputDevice, InputHelpers.Button.SecondaryButton, out bool isActivated, 0f)) {

        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool CheckActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActionButton, out bool isActivated, activationThreshold);
        Debug.Log(isActivated);
        return isActivated;
    }
}
