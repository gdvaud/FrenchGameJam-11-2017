using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    public GameManager gm;

    public int health = 50;
    public int maxHealth = 50;

    [Header("Shoot Handling")]
    private bool shoot = false;
    private bool wasLoading = false;
    public float shotSpeed = 0.5f; //Number of shots per second
    private float timeLastShot;
    public GameObject projectile;
    public Transform shootDirection;

    void Update() {
        if (shoot && (timeLastShot + 1f / shotSpeed) < Time.time) {
            GameObject p = GameObject.Instantiate(projectile, shootDirection.position, shootDirection.rotation);
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
