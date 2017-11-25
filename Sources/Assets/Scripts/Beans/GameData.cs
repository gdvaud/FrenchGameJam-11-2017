using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public int numberPlayer { get; set; }
    public Dictionary<int, KeyCode> playerKeys { get; set; }

    public Dictionary<int, Player> players { get; set; }

    private void Awake() {
        playerKeys = new Dictionary<int, KeyCode>();
        players = new Dictionary<int, Player>();

        // --- Used during Tests
        numberPlayer = 3;
        playerKeys.Add(1, KeyCode.Alpha1);
        playerKeys.Add(2, KeyCode.Alpha2);
        playerKeys.Add(3, KeyCode.Alpha3);
        players.Add(1, new Player(5));
        players.Add(2, new Player(5));
        players.Add(3, new Player(5));
        // ---
    }
    private void Start() {
        DontDestroyOnLoad(this);
    }

    public bool addKey(int player, KeyCode key) {
        if (!playerKeys.ContainsValue(key)) {
            playerKeys.Add(player, key);
            return true;
        }
        return false;
    }
}
