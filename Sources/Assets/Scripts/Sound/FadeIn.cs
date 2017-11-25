using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    public AudioSource sound;
    private float timeStart;
    public float fadeDuration;

    private void Start() {
        timeStart = Time.time;
    }

    // Update is called once per frame
    private void Update() {
        if (sound.volume == 1) {
            Destroy(this);
        }
        Debug.Log("123");
        sound.volume = Mathf.Lerp(0, 1, (Time.time - timeStart) / fadeDuration);
    }
}
