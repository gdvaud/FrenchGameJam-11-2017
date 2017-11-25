using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Projectile : MonoBehaviour {

    private float force = 0;

    private Vector3 v;
    private Vector3 accel = new Vector3(0f, -9.81f);
    private const float NOCLIP_DURATION = 1f;
    private float creationTime;
    private Collider2D collider;

	// Use this for initialization
	void Start () {
        creationTime = Time.time;
        collider = GetComponent<Collider2D>();
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

        // Enable collider after tolerance duration exceeded
        if (!collider.enabled && Time.time - creationTime > NOCLIP_DURATION) {
            collider.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Collision", this);
        Destroy(gameObject);
    }

    public void setForce(float force) {
        this.force = force;
    }
}
