using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    public GameManager gm;

    public int health = 50;
    public int maxHealth = 50;

    [Header("Shoot Handling")]
    private bool shoot = false;
    private bool isLoading = false;
    public float shotSpeed = 0.5f; //Number of shots per second
    private float timeLastShot;
    public GameObject projectile;
    public Transform shootDirection;

    [Header("Rotation Handling")]
    public GameObject canon;
    public float rotationSpeed; //Degrees by second
    public int direction = 1;
    public float maxAngle;
    public float minAngle;


    void Update() {
        if (!isLoading) {
            float z = canon.transform.rotation.eulerAngles.z;
            z += direction * rotationSpeed * Time.deltaTime;
            if (z > maxAngle) {
                z = maxAngle;
                direction *= -1;
            }
            if (z < minAngle) {
                z = minAngle;
                direction *= -1;
            }
            canon.transform.rotation = Quaternion.AngleAxis(z, Vector3.forward);
        } 

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
        if (isLoading && !loading) {
            this.shoot = true;
        }
        isLoading = loading;
    }
}
