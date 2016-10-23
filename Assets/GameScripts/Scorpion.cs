using UnityEngine;
using System.Collections;

public class Scorpion : MonoBehaviour {
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;
    float myHeight;

	// Use this for initialization
	void Start () {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
	}
	
	void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth;
        Vector2 lineCastPosY = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPosY - myTrans.right.toVector2() * 0.05f, enemyMask);
        //Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);

        if(!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }
}
