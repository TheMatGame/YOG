    using UnityEngine;

    public class MovingTower : MonoBehaviour, HitInterface
    {
        float distancePerHit = 20f;
        public float towerHeight = 1f;

        bool moving = false;
        Vector3 endPosition;
        public void Hit(GameObject actor)
        {
            Vector3 direction = (transform.position - actor.transform.position).normalized;

            // Elegimos el eje dominante
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                direction = new Vector3(Mathf.Sign(direction.x), 0f, 0f); // solo en X
            }
            else
            {
                direction = new Vector3(0f, 0f, Mathf.Sign(direction.z)); // solo en Z
            }

            endPosition = transform.position + direction * distancePerHit;

            // Horizontal check for collision
            bool hit = Physics.Linecast(transform.position, endPosition, LayerMask.GetMask("Default"));
            if (hit) return;

            // Vertical check for collision
            bool hit2 = Physics.Linecast(endPosition, endPosition - new Vector3(0f,towerHeight/2 + 1,0f));
            if (hit2) moving = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (moving) {
                float moveSpeed = 5f; // Ajusta la velocidad a tu gusto
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, endPosition) < 0.01f)
                {
                    transform.position = endPosition; // Forzamos el valor exacto al final
                    Reset();
                }

            }
        }

        private void Reset()
        {
            moving = false;
            endPosition = Vector3.zero;
        }
    }
