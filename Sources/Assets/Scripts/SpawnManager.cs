using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour {
    public List<GameObject> spawnPoints;
    private Dictionary<int, GameObject> playerSpawns;

    public Vector3 allocateSpawnPoint(int playerId) {
        Debug.Assert(!playerSpawns.ContainsKey(playerId));
        var freeSpawnPoints = spawnPoints.Except(playerSpawns.Values).ToList();
        var spawnPoint = freeSpawnPoints[Random.Range(0, freeSpawnPoints.Count)];
        playerSpawns[playerId] = spawnPoint;
        return spawnPoint.transform.position;
    }

    public void freeSpawnPoint(int playerId) {
        playerSpawns.Remove(playerId);
    }

	// Use this for initialization
	void Awake () {
        playerSpawns = new Dictionary<int, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
