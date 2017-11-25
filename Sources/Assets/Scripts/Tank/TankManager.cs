using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    public GameManager gm;

    public int health = 50;
    public int maxHealth = 50;

    [Header("Shoot Handling")]
    public bool shoot = false;
    public bool wasLoading = false;
    public float shotSpeed = 0.5f; //Number of shots per second
    public float timeLastShot;
    public GameObject projectile;
    public Transform shootDirection;

    void Update() {
        if (shoot && (timeLastShot + 1f / shotSpeed) < Time.time) {
            Debug.Log("shot");
            GameObject p = GameObject.Instantiate(projectile, shootDirection.position, shootDirection.rotation);
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.right* 100);
            timeLastShot = Time.time;
        }
        shoot = false;
    }

    public void setGameManager(GameManager gm) {
        this.gm = gm;
    }

    public void setIsLoading(bool loading) {
        if (wasLoading && !loading) {
            this.shoot = true;
        }
        wasLoading = loading;
    }
}
