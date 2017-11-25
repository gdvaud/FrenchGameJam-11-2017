using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    public GameManager gm;

    public int health = 50;
    public int maxHealth = 50;

    public int Health {
        get { return health; }
        set {
            health = value;
            var ratio = health / ((float) maxHealth);
            healthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = ratio;
        }
    }

    private string playerName;
    public string PlayerName {
        get { return playerName; }
        set {
            playerName = value;
            playerNameLabel.GetComponent<UnityEngine.UI.Text>().text = value;
        }
    }

    public GameObject playerNameLabel;
    public GameObject healthBar;

    [Header("Shoot Handling")]
    private bool shoot = false;
    private bool isLoading = false;
    public float forceFactorBySecond = 10f;
    public float shotSpeed = 0.5f; //Number of shots per second
    private float timeLastShot;
    private float startLoading;
    public GameObject projectile;
    public Transform shootDirection;

    [Header("Rotation Handling")]
    public GameObject canon;
    public float rotationSpeed; //Degrees by second
    public int direction = 1;
    public float maxAngle;
    public float minAngle;

    [Header("Sounds")]
    public AudioSource audioSource;
    public List<AudioClip> sounds;

    void Update() {
        if (!isLoading) {
            float z = canon.transform.eulerAngles.z;
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

            var projManager = p.GetComponent<ProjectileManager>();
            projManager.Emitter = gameObject;
            projManager.gameManager = gm;

            p.GetComponent<Projectile>().setForce((Time.time - startLoading) * forceFactorBySecond);

            audioSource.clip = sounds[Random.Range(0,sounds.Count)];
            audioSource.Play();

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
        if (!isLoading) {
            startLoading = Time.time;
        }
        isLoading = loading;
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        var proj = coll.gameObject.GetComponent<ProjectileManager>();
        if (proj != null) {
            // Ignore self-collision when if happens soon after the shot
            var ignoreCollision = gameObject == proj.Emitter && Time.time - proj.creationTime < ProjectileManager.TOLERANCE_DURATION;
            if (!ignoreCollision) {
                Health -= proj.InstantDamage();
                Debug.LogFormat("{0} dealt {1} damage to {2}", proj.Emitter.name, proj.InstantDamage(), name);
                if (Health <= 0) {
                    Debug.LogFormat("{0} killed {1}", proj.Emitter.gameObject.name, name);
                    Destroy(gameObject);
                }
            }

        }
    }
}
