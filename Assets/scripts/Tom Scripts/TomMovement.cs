using UnityEngine;

namespace Tom_Scripts
{
    public class TomMovement : MonoBehaviour
    {
        private Rigidbody2D myBody;
        public Transform target;
        public float speed = 3f;
        private bool isJumping = false;
        private Collider2D unitCollider;
        
        void Awake(){
            myBody = GetComponent<Rigidbody2D> ();
            unitCollider = GetComponent<Collider2D>();
            IgnoreCollisionWithTreeCircles();
            
        }
        
        void Update(){
            Walk();
            Jump();
        }
        
        
        void Walk()
        {
            Vector2 currentPosition = transform.position;
            
            Vector2 targetPosition = new Vector2(target.position.x, currentPosition.y);
            
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            
            transform.position = new Vector2(newPosition.x, newPosition.y);
            if (targetPosition.x > newPosition.x)
            {
                ChangeDirection(1);
            }
            else
            {
                ChangeDirection(-1);
            }
        }

        void Jump()
        {
            if ((transform.position.x is > 6 and < 7 
                 || transform.position.x is > 12 and < 13) 
                && !isJumping)
            {
                myBody.AddForce(new Vector2(0, 7f), ForceMode2D.Impulse);
                isJumping = true;
            }
            if (transform.position.x is > 33.5f and < 49f && !isJumping)
            {
                myBody.AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        
        void ChangeDirection(int direction){
            Vector2 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }
        
        void IgnoreCollisionWithTreeCircles()
        {
            GameObject[] treeCircles = GameObject.FindGameObjectsWithTag("Tree-circle");
            foreach (GameObject treeCircle in treeCircles)
            {
                Collider2D treeCircleCollider = treeCircle.GetComponent<Collider2D>();
                if (treeCircleCollider != null)
                {
                    Physics2D.IgnoreCollision(unitCollider, treeCircleCollider);
                }
            }
        }

    }
}