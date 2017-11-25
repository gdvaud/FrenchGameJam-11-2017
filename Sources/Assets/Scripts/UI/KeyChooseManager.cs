using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyChooseManager : MonoBehaviour {

    public ScenesManager scenesManager;
    private GameData gameData;
    private int actualPlayer = 1;

    public Text playerEnter;
    public Text error;

    private void Start() {
        gameData = FindObjectOfType<GameData>();
        playerEnter.text = "Enter player " + actualPlayer + " key !";
    }

    private void Update() {
        if (actualPlayer <= gameData.numberPlayer) {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(kcode)) {
                    Debug.Log(actualPlayer + " " + kcode);
                    error.enabled = false;
                    if (gameData.addKey(actualPlayer, kcode)) {
                        actualPlayer++;
                        break;
                    } else {
                        error.enabled = true;
                    }
                }
            }
            playerEnter.text = "Enter player " + actualPlayer + " key !";
        } else {
            scenesManager.loadGame();
        }
    }
}
