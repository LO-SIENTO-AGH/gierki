 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JerryMovement : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody2D myBody;
    private Animator animator;

    public Transform groundCheckPosition;

    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;
    private bool isOnFence;
    private bool isOnTree;
    private float jumpPower = 5f;
   
    void Awake(){
        myBody = GetComponent<Rigidbody2D> ();
        animator  = GetComponent<Animator> ();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        CheckIfGrounded();
        PlayerJump();
        CheckJumpFromFence();
    }

    void FixedUpdate(){
        PlayerWalk();
    }

    void PlayerWalk(){
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if(h < 0){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else{
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction){
        Vector2 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded(){
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if(isGrounded){
            if(jumped){
                jumped = false;
                animator.SetBool("Jump", false);
            }
        }
    }

   void PlayerJump(){
        if(isGrounded){
            if(Input.GetKey(KeyCode.Space)){
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                animator.SetBool("Jump", true);
            }
        }
   }
   
   void CheckJumpFromFence()
   {
       if (isOnFence && Input.GetKeyDown(KeyCode.DownArrow))
       {
           isGrounded = false;
           isOnFence = false;
           myBody.gravityScale = 1;
       }
   }

   void JumpFromFence()
   {
       myBody.velocity = new Vector2(speed, jumpPower);
       isOnFence = false;
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
           Vector2 triggerPosition = transform.position;

           // Get the position of the other collider (collider entering the trigger)
           Vector2 otherColliderPosition = other.transform.position;

           // Calculate the vertical (Y-axis) difference between the two positions
           float verticalDifference = otherColliderPosition.y - triggerPosition.y;
           Debug.Log(otherColliderPosition.y - triggerPosition.y);
           if (verticalDifference < -0.5)
           {
               isOnFence = true;
               isGrounded = true;
               myBody.gravityScale = 0;
               myBody.velocity = new Vector2(speed, 0);
           }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
       myBody.gravityScale = 1;
       myBody.velocity = new Vector2(speed, 0);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
       Debug.Log(other.gameObject.name);
       if (other.gameObject.CompareTag("Finish"))
       {
           GameCompleted();
       }
       if (other.gameObject.CompareTag("Tree-circle"))
       {
           OnTree();
       }
       if (other.gameObject.CompareTag("Fence"))
           Debug.Log("Fence");
       {
           isOnFence = true;
           myBody.velocity = new Vector2(0f, 0f); 
                   
           if (Input.GetKeyDown(KeyCode.DownArrow))
           {
               other.otherCollider.enabled = false;
               JumpFromFence();
           }
       }
   }

   void GameCompleted()
   {
       SceneManager.LoadScene("EndScene");
   }

   void OnTree()
   {
       var position = transform.position;
       position = new Vector2(position.x, position.y + 2);
       transform.position = position;
       myBody.gravityScale = 0;
       isOnTree = true;
   }
}
