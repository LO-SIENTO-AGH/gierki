 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryMovement : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody2D myBody;
    private Animator animator;
   
    void Awake(){
        myBody = GetComponent<Rigidbody2D> ();
        animator  = GetComponent<Animator> ();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        PlayerWalk();
    }

    void PlayerWalk(){
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
        else if(h < 0){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        }
        else{
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }
}
