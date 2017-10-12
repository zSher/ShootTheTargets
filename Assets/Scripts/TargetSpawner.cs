using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    public Rect spawnArea;
    public GameObject targetObj;
    public bool enableSpawn;
    public float interval = 5;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnTargets());
        spawnArea.center = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnTargets()
    {
        while (enableSpawn)
        {
            GameObject target = Instantiate(targetObj);
            float x = Random.Range(spawnArea.xMin, spawnArea.xMax);
            float y = Random.Range(spawnArea.yMin, spawnArea.yMax);
            target.transform.position = new Vector3(x, y);
            Rigidbody2D rb2d = target.GetComponent<Rigidbody2D>();
            rb2d.AddTorque(Random.Range(-10, 10));

            yield return new WaitForSeconds(interval);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea.center, new Vector3(spawnArea.width, spawnArea.height, 0));
    }
}
