using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemoGameController : MonoBehaviour {
    public int winAmt = 50;
    public RocketPlayerController[] playerControllers;
    public TextMeshProUGUI winGui;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            if (playerControllers[i].targetPack.Count >= winAmt)
            {
                //set something to something
                winGui.gameObject.SetActive(true);
                winGui.SetText("PLAYER " + (i+1) + " HAS WON THE GAME");
                Time.timeScale = 0; //stop

            }
        }
    }
}
