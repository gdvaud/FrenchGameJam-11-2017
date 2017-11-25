using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    public AudioSource sound;
    private float initialVolume;
    private float timeStart;
    public float fadeDuration;

    private void Start() {
        timeStart = Time.time;
        initialVolume = sound.volume;
    }

    // Update is called once per frame
    private void Update() {
        sound.volume = Mathf.Lerp(0, initialVolume, (Time.time - timeStart) / fadeDuration);

        if (sound.volume >= initialVolume) {
            Destroy(this);
        }
    }
}
