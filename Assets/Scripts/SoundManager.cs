using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioSource music;
    public AudioSource sfx;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySfx(AudioClip clip, bool randomizePitch = true) {
        if (randomizePitch) {
            sfx.pitch = Random.Range(0.95f, 1.05f);
        }
        sfx.PlayOneShot(clip);
    }
}
