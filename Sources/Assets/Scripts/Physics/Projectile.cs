using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Projectile : MonoBehaviour {

    private float force = 0;

    private Vector3 v;
    private Vector3 accel = new Vector3(0f, -9.81f);
    private float NOCLIP_DURATION = 0.5f;

	// Use this for initialization
	void Start () {
        print(transform.eulerAngles.z);
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

    void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log("Collision " + this);
        Destroy(gameObject);
    }

    public void setForce(float force) {
        this.force = force;
    }
}
