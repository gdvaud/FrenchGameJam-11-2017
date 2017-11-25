using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameData gameData;
    public GameObject bottomBound;
    public GameObject leftBound;
    public GameObject rightBound;

    [Header("Managers")]
    public InputManager inputManager;
    public ScenesManager sceneManager;
    private Dictionary<int, TankManager> tanks;

    [Header("Tank Generation")]
    public int maxPlayer = 8;
    private List<int> unusedAreas;
    public List<GameObject> persos;
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
        gameData = FindObjectOfType<GameData>();
        spawnWidth = maxLocation.position.x - minLocation.position.x;

        shufflePersos();

        for (int i = 1; i <= gameData.numberPlayer; i++) {
            GameObject tank = GameObject.Instantiate(persos[i % persos.Count]);

            Vector2 tankPosition = tank.transform.position;
            tankPosition.x = generatePosition() + minLocation.position.x;
            tank.transform.position = tankPosition;

            TankManager tankManager = tank.GetComponent<TankManager>();
            tankManager.setGameManager(this);
            tankManager.PlayerName = "P" + i;

            tanks.Add(i, tankManager);
        }

        inputManager.setKeys(gameData.playerKeys);
    }

    public void updateKeyState(int player, bool state) {
        TankManager tank;
        tanks.TryGetValue(player, out tank);
        tank.setIsLoading(state);
    }

    private float generatePosition() {
        int area = Random.Range(0, unusedAreas.Count);
        return (spawnWidth / maxPlayer * area) + (spawnWidth / maxPlayer / 2);
    }

    private void shufflePersos() {
        List<int> indexes = new List<int>(persos.Count);
        List<GameObject> newPersos = new List<GameObject>(persos.Count);

        for (int i = 0; i < persos.Count; i++) {
            indexes.Add(i);
        }

        int size = indexes.Count;
        for (int i = 0; i < size; i++) {
            int index = Random.Range(0, indexes.Count);
            newPersos.Add(persos[indexes[index]]);
            indexes.RemoveAt(index);
        }
        persos = newPersos;
    }
}
