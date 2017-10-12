using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TargetController : MonoBehaviour {

    private static Sprite[] foodSprites;
    private SpriteRenderer spriteRenderer;
    //public bool pickupable;
    public int worth { get; set; }

	// Use this for initialization
	void Start () {
		if (foodSprites == null)
        {
            foodSprites = Resources.LoadAll<Sprite>("Sprites/Food");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = foodSprites[Random.Range(0, foodSprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
