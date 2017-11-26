using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    private AsyncOperation loading = null;

    public void loadMenu() {
        StartCoroutine(loadLevelAsync("Menu"));
    }

    public void loadKeyChoose() {
        StartCoroutine(loadLevelAsync("KeyChoose"));
    }

    public void loadGame() {
        StartCoroutine(loadLevelAsync("Level 1"));
    }

    public void loadGameEnd() {
        StartCoroutine(loadLevelAsync("GameEnd"));
    }

    public void loadCredits() {
        StartCoroutine(loadLevelAsync("Credits"));
    }


    public void quit() {
        Application.Quit();
    }

    private IEnumerator loadLevelAsync(string level) {
        if (loading == null) {
            loading = SceneManager.LoadSceneAsync(level);

            while (!loading.isDone) {
                yield return new WaitForEndOfFrame();
            }

            loading = null;
        }
    }
}
