using UnityEngine;

namespace Tom_Scripts
{
    public class TomMovement : MonoBehaviour
    {
        private Rigidbody2D myBody;
        public Transform target;
        public float speed = 3f;
        
        void Awake(){
            myBody = GetComponent<Rigidbody2D> ();
        }
        
        void Update(){
            Walk();
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
        
        void ChangeDirection(int direction){
            Vector2 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }

    }
}