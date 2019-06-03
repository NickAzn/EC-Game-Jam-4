using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public AudioClip electricHitSound;
    public AudioClip routerPassSound;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Head collided with " + other.tag);
        if (other.tag == "Router") {
            Router r = other.GetComponent<Router>();
            if (transform.position.x <= 0) {
                if (r.leftDistance > 0) {
                    LevelManager.instance.RouterSelected(r.leftDistance);
                } else {
                    LevelManager.instance.WinGame();
                }
            } else {
                if (r.rightDistance > 0) {
                    LevelManager.instance.RouterSelected(r.rightDistance);
                } else {
                    LevelManager.instance.WinGame();
                }
            }
            SoundManager.instance.PlaySfx(routerPassSound);
        } else if (other.tag == "Dodge") {
            LevelManager.instance.PlayerElectricHit(15);
            SoundManager.instance.PlaySfx(electricHitSound);
        }
    }
}
