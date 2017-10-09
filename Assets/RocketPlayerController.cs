using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class RocketPlayerController : MonoBehaviour {

    private Rigidbody2D rb2D;
    private bool grounded; //Variable to say when we can "shoot"
    private bool canFlick;

    public float launchForceStrength = 50;

    public bool isControllerEnabled = false;
    public int controllerPlayerNumber = 0;

    public List<GameObject> targetPack;

    public GameObject[] explosionGO;

    // Use this for initialization
    void Start () {
        targetPack = new List<GameObject>();
        rb2D = GetComponent<Rigidbody2D>();
        grounded = true;
        canFlick = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            rb2D.isKinematic = true;

            if (!isControllerEnabled)
            {

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
                if (Input.GetButtonDown("Fire1"))
                {
                    Launch();
                }
            } else
            {
                Vector3 aimDir = new Vector3(XCI.GetAxis(XboxAxis.RightStickX, (XboxController)controllerPlayerNumber), XCI.GetAxis(XboxAxis.RightStickY, (XboxController)controllerPlayerNumber), 0);
                if (aimDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, aimDir);

                }

                if (XCI.GetButtonDown(XboxButton.RightBumper, (XboxController)controllerPlayerNumber))
                {
                    Launch();
                }
            }
        } else
        {

            if (rb2D.isKinematic)
            {
                rb2D.isKinematic = false;
            }
        }
    }

    private void Launch()
    {
        rb2D.isKinematic = false;
        grounded = false;
        rb2D.AddForce(new Vector2(transform.up.x * launchForceStrength, transform.up.y * launchForceStrength));
        Debug.Log("Launch");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Groundable")
        {
            if (!grounded)
            {
                if (!isControllerEnabled)
                {
                    if (Input.GetAxis("Fire1") > 0.0f && canFlick)
                    {
                        //flick off this surface without stopping
                        canFlick = false; //works once though
                    } else
                    {
                        ReGround();
                    }
                } else
                {
                    if (XCI.GetButton(XboxButton.RightBumper, (XboxController)controllerPlayerNumber) && canFlick)
                    {
                        //flick off this surface without stopping
                        canFlick = false; //works once though
                    } else
                    {
                        ReGround();
                    }
                }
            }
        }

    }

    private void ReGround()
    {
        grounded = true;
        canFlick = true;
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0.0f;
        rb2D.isKinematic = true;
    }

    public void CrashExplode(Vector2 dirOfExplosion)
    {
        //GameObject explode = GameObject.Instantiate()
        int index = Random.Range(0, explosionGO.Length);

        float spinTime = 0;
        
        while (targetPack.Count > 0)
        {
            GameObject item = targetPack[0];
            item.SetActive(true);

            float x = .25f * Mathf.Cos(spinTime);
            float y = .25f * Mathf.Sin(spinTime);
            spinTime += Time.fixedTime;

            item.transform.position = transform.position + new Vector3(x, y);
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.AddExplosionForce(100f, transform.position, 1f);
            targetPack.RemoveAt(0);
        }

        Quaternion rot = Quaternion.LookRotation(new Vector3(dirOfExplosion.x, dirOfExplosion.y, 0), Vector3.up);
        GameObject newExplosion = Instantiate(explosionGO[index]);
        newExplosion.transform.position = transform.position;
        newExplosion.transform.rotation = Quaternion.LookRotation(Vector3.forward, dirOfExplosion);

        //stop
        rb2D.isKinematic = true;
        grounded = true;
        rb2D.velocity = Vector2.zero;
        rb2D.rotation = 0;

        transform.position = new Vector3(Random.Range(-2.08f, 2.08f), Random.Range(-1.4f, 1.4f), 0);

    }

    public void SetPlayerController(int playerNumber)
    {
        this.isControllerEnabled = true;
        this.controllerPlayerNumber = playerNumber;
    }

}
