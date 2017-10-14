using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSizer : MonoBehaviour {
    public float minTextSize = 36;
    public float maxTextSize = 122;
    public TextMeshProUGUI scoreCounter;
    public int maxScore = 50;
    private float score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScoreText(float scoreAmt)
    {
        score = scoreAmt;
        //calc size, learp between 36 - 122 for font size based on 0 - 50 score
        float fontSize = Mathf.Lerp(minTextSize, maxTextSize, score / maxScore);

        scoreCounter.fontSize = fontSize;
        scoreCounter.SetText(score.ToString());
    }
}
