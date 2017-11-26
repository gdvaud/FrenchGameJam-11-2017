using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int kill { get; set; }
    public int death { get; set; }
    public int damage { get; set; }

    public TankManager tank { get; set; }

    public Player() {
        kill = 0;
        death = 0;
        damage = 0;
    }
}
