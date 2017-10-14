using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class RocketPlayerCollider : MonoBehaviour {

    private Rigidbody2D parentrb2D;
    private Transform parentTransform;
    private RocketPlayerController parentController;

    // Use this for initialization
    void Start () {
        parentTransform = transform.parent.transform;
        parentrb2D = transform.parent.GetComponent<Rigidbody2D>();
        parentController = GetComponentInParent<RocketPlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!parentController.isDead)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.Equals(transform.parent.gameObject))
                {
                    //Break if we are colliding against ourselves
                    return;
                }
                //TODO: check teams blah blah blah

                //Check where we collided with other player, choose which dies based on direction and velocity

                //get the vector of velocity, check it that vector is pointing towards the collision point
                //rb2D.velocity

                GameObject enemyPlayer = collision.gameObject;
                RocketPlayerController rpController = collision.gameObject.GetComponentInParent<RocketPlayerController>();
                if (rpController.isInvulnerable || rpController.isDead)
                {
                    return;
                }

                Vector3 direction = enemyPlayer.transform.position - parentTransform.position;
                float angleOfCollision = Vector3.Angle(direction.normalized, parentrb2D.velocity.normalized);

                if (angleOfCollision < 90 && parentrb2D.velocity != Vector2.zero)
                {
                    rpController.CrashExplode(parentrb2D.velocity.normalized);
                }
            }
            else if (collision.gameObject.tag == "Target")
            {
                //get target's score amount, add to this objects score
                this.parentController.targetPack.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.tag == "DeadOnTouch")
            {
                if (!parentController.isInvulnerable)
                {
                    parentController.CrashExplode(parentrb2D.velocity * -1);
                }
            }
        }
       
    }
}
