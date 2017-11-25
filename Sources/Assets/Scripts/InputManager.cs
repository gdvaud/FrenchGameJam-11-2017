using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameManager gm;
    private Dictionary<int, KeyCode> keys;

    void Awake() {
        keys = new Dictionary<int, KeyCode>();
    }

    // Update is called once per frame
    void Update() {
        foreach (KeyValuePair<int, KeyCode> key in keys) {
            gm.updateKeyState(key.Key, Input.GetKey(key.Value));
        }
    }

    public void setKeys(Dictionary<int, KeyCode> keys) {
        this.keys = keys;
    }
}
