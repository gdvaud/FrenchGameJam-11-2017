using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public const float TOLERANCE_DURATION = 1f;
    public float creationTime;
    // Tank GameObject. Must be set by emitter.
    public GameObject Emitter { get; set; }
    public GameManager gameManager { get; set; }
    public AudioSource audioSource;
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
        Debug.Assert(gameManager != null);
        creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (
            transform.position.x < gameManager.leftBound.transform.position.x ||
            transform.position.x > gameManager.rightBound.transform.position.x ||
            transform.position.y < gameManager.bottomBound.transform.position.y
        ) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D coll) {
        // Ignore self-collision when if happens soon after the shot
        if (coll.gameObject == Emitter && Time.time - creationTime < TOLERANCE_DURATION) {
        } else {
            audioSource.Play();
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            Destroy(gameObject, 1f);
        }
    }

}
