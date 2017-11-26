using System.Collections;
using System.Collections.Generic;

public class Player {

    public int kill { get; set; }
    public int death { get; set; }
    public int damage { get; set; }

    public Player() {
        kill = 0;
        death = 0;
        damage = 0;
    }
}
