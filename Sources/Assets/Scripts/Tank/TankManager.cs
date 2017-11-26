using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour {

    public GameManager gm;

    [Header("Score")]
    private GameObject score;
    public GameObject Score {
        get { return score; }
        set {
            Text[] objs = value.GetComponentsInChildren<Text>();
            foreach (var obj in objs) {
                switch(obj.name) {
                    case "Player":
                        obj.text = "Player " + playerNumber;
                        break;
                    case "Lives":
                        lives = obj;
                        break;
                    case "Death":
                        death = obj;
                        break;
                    case "Kills":
                        kills = obj;
                        break;
                }
            }
        }
    }
    public Text lives { get; set; }
    public Text kills { get; set; }
    public Text death { get; set; }
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

    private int playerNumber;
    public int PlayerNumber {
        get { return playerNumber; }
        set {
            playerNumber = value;
            playerNameLabel.GetComponent<UnityEngine.UI.Text>().text = "P" + value;
        }
    }

    public GameObject playerNameLabel;
    public GameObject healthBar;
    public GameObject playerArm;

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

            playerArm.SetActive(false);
            Invoke("enableArm", 1f / shotSpeed);

            audioSource.clip = sounds[Random.Range(0,sounds.Count)];
            audioSource.Play();

            timeLastShot = Time.time;
        }
        shoot = false;
    }

    private void enableArm() {
        playerArm.SetActive(true);
        startLoading = Time.time;
        var canonAngle = Random.Range(minAngle, maxAngle);
        canon.transform.rotation = Quaternion.AngleAxis(canonAngle, Vector3.forward);
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
                    var killerTankManager = proj.Emitter.GetComponent<TankManager>();
                    gm.OnPlayerKill(killerTankManager.PlayerNumber, PlayerNumber);
                }
            }

        }
    }
}
