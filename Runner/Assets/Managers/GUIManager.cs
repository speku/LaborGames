using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    private InputManager inputManager;
    public GUIText gameOverText, instructionsText, runnerText;

	// Use this for initialization
	void Start () {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameOverText.enabled = false;
        inputManager = FindObjectOfType<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") || inputManager.WasTouch())
        {
            GameEventManager.TriggerGameStart();
        }
	}

    private void GameStart()
    {
        gameOverText.enabled = false;
        instructionsText.enabled = false;
        runnerText.enabled = false;
        enabled = false;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        instructionsText.enabled = true;
        enabled = true;
    }
}
