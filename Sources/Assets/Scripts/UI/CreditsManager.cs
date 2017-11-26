using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour {

    public ScenesManager scenesManager;
    public List<GameObject> pages;
    private Stack<GameObject> pageStack;

	// Use this for initialization
	void Start () {
        pageStack = new Stack<GameObject>(pages);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick() {
        if (pageStack.Count > 1) {
            pageStack.Pop().SetActive(false);
        } else {
            scenesManager.loadMenu();
        }
    }
}
