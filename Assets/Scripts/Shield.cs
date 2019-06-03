using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Shield : MonoBehaviour {

    public GameObject destroyParticles;
    public bool leftHand;

    public SteamVR_Action_Vibration hapticAction;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Shield collided with " + other.tag);
        if (other.tag == "Block") {
            GameObject particles = Instantiate(destroyParticles);
            particles.transform.position = other.transform.position;
            Destroy(other.gameObject);
            if (leftHand) {
                hapticAction.Execute(0, 0.2f, 100, 12, SteamVR_Input_Sources.LeftHand);
            } else {
                hapticAction.Execute(0, 0.2f, 100, 12, SteamVR_Input_Sources.RightHand);
            }
        }
    }
}
