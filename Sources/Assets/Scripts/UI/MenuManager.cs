using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public ScenesManager scenesManager;
    private GameData gameData;

    public Text numberPlayer;
    public Slider numberPlayerSlider;

    private void Start() {
        gameData = FindObjectOfType<GameData>();
    }

    public void onStart() {
        gameData.numberPlayer = (int) numberPlayerSlider.value;
        scenesManager.loadKeyChoose();
    }

    public void onQuit() {
        scenesManager.quit();
    }

    public void onNumberPlayerChange() {
        numberPlayer.text = "Nombre joueurs: " + numberPlayerSlider.value;
    }
    
}
