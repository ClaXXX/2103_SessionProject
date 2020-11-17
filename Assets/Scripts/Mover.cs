using UnityEngine;

namespace DefaultNamespace {
    public class Mover : MonoBehaviour {
        // TODO : Mettre la code en charge du déplacement ici
        public void shoot(Rigidbody rb) {
            rb.AddForce(Vector3.left * 1000);
        }
    }
}