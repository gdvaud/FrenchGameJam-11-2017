using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Projectile : MonoBehaviour {

    private float force = 0;

    private Vector3 v;
    private Vector3 accel = new Vector3(0f, -9.81f);

	// Use this for initialization
	void Start () {
        var angle = transform.eulerAngles.z * Math.PI / 180f;
        v = new Vector3(
            force * (float) Math.Cos(angle),
            force * (float) Math.Sin(angle)
        );
	}

	// Update is called once per frame
	void Update () {
        var dt = Time.deltaTime;
        transform.position += accel * (dt * dt) / 2f + v * dt;
        v += accel * dt;
	}

    public void setForce(float force) {
        this.force = force;
    }
}
