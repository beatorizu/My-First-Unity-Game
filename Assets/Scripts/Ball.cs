using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody rb;
    private bool isBouncing;
    public float speed = 2;
    public float bouncingForce = 3;
    private float vertical;
    private float horizontal;
    public static bool stop;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb || stop) return;

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (transform.position.y < -10) StartCoroutine(Reset());
    }

    void FixedUpdate()
    {
        if (!rb || stop) return;

        rb.AddForce(0, 0, vertical * speed, ForceMode.Force);
        rb.AddForce(horizontal * speed, 0, 0, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space))
        {
            if (isBouncing) return;
            rb.AddForce(0, bouncingForce, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isBouncing = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isBouncing = true;
    }

    IEnumerator Reset() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForFixedUpdate();

        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }
}
