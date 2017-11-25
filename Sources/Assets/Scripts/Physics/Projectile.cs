using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Projectile : MonoBehaviour {

    public Vector3 v;
    private Vector3 a = new Vector3(0f, -9.81f);

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {
        var dt = Time.deltaTime;
        transform.position += a * (dt * dt) / 2f + v * dt;
        v += a * dt;
	}

    void OnCollisionEnter2D(Collision2D coll) {
        print("collision");
        gameObject.SetActive(false);
    }
}
