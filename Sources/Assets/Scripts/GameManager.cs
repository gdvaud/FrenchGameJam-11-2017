using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Managers")]
    public InputManager inputManager;
    public SceneManager sceneManager;
    public UIManager UIManager;
    private Dictionary<int, TankManager> tanks;

    [Header("Game Variable")]
    public int nbPlayer = 2;
    public GameObject tankPrefab;

    private void Awake() {
        tanks = new Dictionary<int, TankManager>();
    }
    // Use this for initialization
    void Start() {

        for (int i = 1; i <= nbPlayer; i++) {
            GameObject tank = GameObject.Instantiate(tankPrefab);

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
}
