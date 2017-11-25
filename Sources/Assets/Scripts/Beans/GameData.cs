using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public int numberPlayer { get; set; }
    public Dictionary<int, KeyCode> playerKeys { get; set; }
    private void Awake() {
        playerKeys = new Dictionary<int, KeyCode>();

        // --- Used during Tests
        numberPlayer = 3;
        playerKeys.Add(1, KeyCode.Alpha1);
        playerKeys.Add(2, KeyCode.Alpha2);
        playerKeys.Add(3, KeyCode.Alpha3);
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
