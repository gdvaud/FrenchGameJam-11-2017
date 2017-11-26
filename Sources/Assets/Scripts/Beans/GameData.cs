using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public int numberPlayer { get; set; }
    public int maxLives;
    public Dictionary<int, KeyCode> playerKeys { get; set; }

    public Dictionary<int, Player> players { get; set; }

    private void Awake() {
        playerKeys = new Dictionary<int, KeyCode>();
        players = new Dictionary<int, Player>();
    }
    private void Start() {
        DontDestroyOnLoad(this);
    }

    public bool addKey(int player, KeyCode key) {
        if (!playerKeys.ContainsValue(key)) {
            playerKeys.Add(player, key);
            players.Add(player, new Player());
            return true;
        }
        return false;
    }
}
