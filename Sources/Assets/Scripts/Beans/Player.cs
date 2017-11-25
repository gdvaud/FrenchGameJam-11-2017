using System.Collections;
using System.Collections.Generic;

public class Player {

    public int kill { get; set; }
    public int death { get; set; }
    public int damage { get; set; }
    public int lifes { get; set; }

    public Player(int lifes) {
        kill = 0;
        death = 0;
        damage = 0;
        this.lifes = lifes;
    }
}
