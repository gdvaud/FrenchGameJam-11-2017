using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public const float TOLERANCE_DURATION = 1f;
    public float creationTime;
    // Tank GameObject. Must be set by emitter.
    public GameObject Emitter { get; set; }
    public int baseDamage = 20;

    public int InstantDamage() {
        var physics = GetComponent<Projectile>();

        // Divide by default forceFactorBySecond
        var velocityFactor = physics.v.magnitude / 10f;
        return Mathf.RoundToInt(baseDamage * velocityFactor);
    }

	// Use this for initialization
	void Start () {
        Debug.Assert(Emitter != null);
        creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D coll) {
        // Ignore self-collision when if happens soon after the shot
        if (coll.gameObject == Emitter && Time.time - creationTime < TOLERANCE_DURATION) {
            Debug.Log("Self-collision (ignored)", this);
        } else {
            Debug.Log("Collision " + coll.gameObject);
            Destroy(gameObject);
        }
    }

}
