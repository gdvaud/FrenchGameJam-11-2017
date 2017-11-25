using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    private const float NOCLIP_DURATION = 1f;
    private float creationTime;

	// Use this for initialization
	void Start () {
        creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll) {
        // Enable collider after tolerance duration exceeded
        if (Time.time - creationTime < NOCLIP_DURATION) {
            Debug.Log("Collision (ignored)", this);
        } else {
            Debug.Log("Collision", this);
            Destroy(gameObject);
        }
    }

}
