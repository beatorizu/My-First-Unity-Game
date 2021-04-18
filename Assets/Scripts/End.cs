using UnityEngine;

public class End : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Ball ball);
        if (!ball) return;

        other.TryGetComponent(out Rigidbody rb);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Ball.stop = true;

        GUI.Insance.EndGame();
    }
}
