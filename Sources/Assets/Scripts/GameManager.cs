﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    private GameData gameData;
    public GameObject bottomBound;
    public GameObject leftBound;
    public GameObject rightBound;

    [Header("Managers")]
    public InputManager inputManager;
    public ScenesManager sceneManager;
    public SpawnManager spawnManager;
    private Dictionary<int, TankManager> tanks;

    [Header("Tank Generation")]
    public int maxPlayer = 8;
    public List<GameObject> persos;
    public Transform minLocation;
    public Transform maxLocation;
    private float spawnWidth;
    public GameObject playerScore;
    public GameObject scores;

    private void Awake() {
        tanks = new Dictionary<int, TankManager>();
    }
    // Use this for initialization
    void Start() {
        gameData = FindObjectOfType<GameData>();

        shufflePersos();

        float scoreWidthPerPlayer = this.scores.GetComponent<RectTransform>().rect.width / gameData.numberPlayer;
        float playerWidth = this.playerScore.GetComponent<RectTransform>().rect.width;
        for (int i = 1; i <= gameData.numberPlayer; i++) {
            GameObject tank = GameObject.Instantiate(persos[i % persos.Count]);
            GameObject score = Instantiate(this.playerScore, this.scores.transform);

            Vector2 scorePosition = score.GetComponent<RectTransform>().anchoredPosition;
            scorePosition.x = scoreWidthPerPlayer * (-0.5f + i);
            score.GetComponent<RectTransform>().anchoredPosition = scorePosition;

            TankManager tankManager = tank.GetComponent<TankManager>();
            tankManager.setGameManager(this);
            tankManager.PlayerNumber = i;
            tankManager.Score = score;
            tankManager.lives.text = "Lives: " + gameData.maxLives;

            resetTank(tankManager);

            Player player;
            gameData.players.TryGetValue(i, out player);
            player.tank = tankManager;
            //gameData.players.Add(i, player);

            tanks.Add(i, tankManager);
        }

        inputManager.setKeys(gameData.playerKeys);
    }

    private void resetTank(TankManager tank) {
        spawnManager.freeSpawnPoint(tank.PlayerNumber);
        tank.transform.position = spawnManager.allocateSpawnPoint(tank.PlayerNumber);
        tank.Health = tank.maxHealth;
        tank.setIsLoading(false);
    }

    public void updateKeyState(int player, bool state) {
        TankManager tank;
        tanks.TryGetValue(player, out tank);
        tank.setIsLoading(state);
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

    public void OnPlayerKill(int killerId, int victimId) {
        Player killer = gameData.players[killerId];
        killer.kill += 1;

        Player victim = gameData.players[victimId];
        victim.death += 1;

        TankManager killers, victims;
        tanks.TryGetValue(killerId, out killers);
        tanks.TryGetValue(victimId, out victims);
        killers.kills.text = "Kills: " + killer.kill;
        victims.lives.text = "Lives: " + (gameData.maxLives - victim.death);

        spawnManager.freeSpawnPoint(victimId);
        TankManager victimTank = tanks[victimId];
        if (victim.death < gameData.maxLives) {
            resetTank(victimTank);
        } else {
            victimTank.gameObject.SetActive(false);

            var nbAlive = gameData.players.Values.Where(p => p.death < gameData.maxLives).Count();
            if (nbAlive < 2) {
                sceneManager.loadGameEnd();
            }
        }
    }

    public void registerPlayerDamage(int attackerId, int damages) {
        gameData.players[attackerId].damage += damages;
    }
}
