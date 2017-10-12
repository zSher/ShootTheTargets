using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreTracker : MonoBehaviour {

    private TextMeshProUGUI textMesh;
    public RocketPlayerController playerController;
    
	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        if (textMesh.text != playerController.targetPack.Count.ToString())
        {
            textMesh.SetText(playerController.targetPack.Count.ToString());
        }
    }
}
