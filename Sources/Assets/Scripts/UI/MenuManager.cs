using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public ScenesManager scenesManager;
    public GameObject beans;
    private GameData gameData;

    public Text numberPlayer;
    public Slider numberPlayerSlider;
    public Text numberLives;
    public Slider numberLivesSlider;

    private void Start() {
        gameData = FindObjectOfType<GameData>();
        if (gameData == null) {
            Instantiate(beans);
        }
    }

    public void onStart() {
        gameData.numberPlayer = (int) numberPlayerSlider.value;
        gameData.maxLives = (int) numberLivesSlider.value;
        scenesManager.loadKeyChoose();
    }

    public void onQuit() {
        scenesManager.quit();
    }

    public void onNumberPlayerChange() {
        numberPlayer.text = "Nombre de joueurs: " + numberPlayerSlider.value;
    }

    public void onNumberLivesChange() {
        numberLives.text = "Nombre de vies: " + numberLivesSlider.value;
    }
    
}
