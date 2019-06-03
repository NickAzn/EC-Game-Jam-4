using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Back collided with " + other.tag);
        if (other.tag == "Block") {
            LevelManager.instance.PlayerDataHit(10);
        }
    }
}
