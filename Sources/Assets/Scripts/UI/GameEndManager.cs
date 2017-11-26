using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour {

    public Text playerWon;
    public Transform statsLocation;
    public GameObject playerStat;

    public float deltaStat = 150;
    private void Start() {
        GameData data = FindObjectOfType<GameData>();
        List<KeyValuePair<int, Player>> stats = new List<KeyValuePair<int, Player>>();
        foreach (KeyValuePair<int, Player> player in data.players) {
            stats.Add(player);
        }

        stats.Sort(delegate (KeyValuePair<int, Player> x, KeyValuePair<int, Player> y) {
            float xStat = x.Value.kill + x.Value.damage / 50 - x.Value.death;
            float yStat = y.Value.kill + y.Value.damage / 50 - y.Value.death;
            if (xStat == yStat) {
                return 0;
            } else {
                return xStat < yStat ? 1 : -1;
            }
        });

        stats.ForEach(delegate (KeyValuePair<int, Player> x) {
            Debug.Log(x + " k:" + x.Value.kill + " d:" + x.Value.death + " dgm:" + x.Value.damage);
            if (x.Value.death < data.maxLives) {
                playerWon.text = "Player " + x.Key + " won !";
            }
            createStat(x.Value, x.Key);
        });
    }

    private void createStat(Player player, int number) {
        GameObject stat = Instantiate(playerStat, statsLocation);
        stat.name = "Player Stats " + number;

        Vector2 position = stat.GetComponent<RectTransform>().anchoredPosition;
        position.y = -deltaStat;
        deltaStat += 110;
        stat.GetComponent<RectTransform>().anchoredPosition = position;

        Text[] objs = stat.GetComponentsInChildren<Text>();
        foreach (var obj in objs) {
            switch (obj.name) {
                case "Name":
                    obj.text = "Player " + number;
                    break;
                case "Kills":
                    obj.text = "" + player.kill;
                    break;
                case "Death":
                    obj.text = "" + player.death;
                    break;
                case "Damages":
                    obj.text = "" + player.damage;
                    break;
            }
        }
    }
}
