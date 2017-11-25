using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Managers")]
    public InputManager inputManager;
    public SceneManager sceneManager;
    public UIManager UIManager;
    private Dictionary<int, TankManager> tanks;

    [Header("Tank Generation")]
    public int nbPlayer = 2;
    public int maxPlayer = 8;
    private List<int> unusedAreas;
    public GameObject tankPrefab;
    public Transform minLocation;
    public Transform maxLocation;
    private float spawnWidth;

    private void Awake() {
        tanks = new Dictionary<int, TankManager>();
        unusedAreas = new List<int>();
        for (int i = 0; i < maxPlayer; i++) {
            unusedAreas.Add(i);
        }
    }
    // Use this for initialization
    void Start() {
        spawnWidth = maxLocation.position.x - minLocation.position.x;

        for (int i = 1; i <= nbPlayer; i++) {
            GameObject tank = GameObject.Instantiate(tankPrefab);

            Vector2 tankPosition = tank.transform.position;
            tankPosition.x = generatePosition() + minLocation.position.x;
            Debug.Log(tankPosition);
            tank.transform.position = tankPosition;

            TankManager tankManager = tank.GetComponent<TankManager>();
            tankManager.setGameManager(this);

            tanks.Add(i, tankManager);
        }

        inputManager.addKey(1, KeyCode.A);
        inputManager.addKey(2, KeyCode.B);
    }

    public void updateKeyState(int player, bool state) {
        TankManager tank;
        tanks.TryGetValue(player, out tank);
        tank.setIsLoading(state);
    }

    private float generatePosition() {
        int area = Mathf.RoundToInt(Random.Range(0, unusedAreas.Count));
        return (spawnWidth / maxPlayer * area) + (spawnWidth / maxPlayer / 2);
    }
}
