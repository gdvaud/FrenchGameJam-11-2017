using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    public ScenesManager scenesManager;
    public AudioSource jingle;
    public SpriteRenderer startScreen;

	// Use this for initialization
	void Start () {
        jingle.Play();
	}
	
	// Update is called once per frame
	void Update () {
        var color = startScreen.color;
        color.a += 0.015f;
        startScreen.color = color;
        if (!jingle.isPlaying) {
            scenesManager.loadMenu();
        }
	}
}
