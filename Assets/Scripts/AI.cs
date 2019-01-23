using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public float movement = 6.0f;
    public float viewRadius = 8.0f;
    public float viewAngleStep = 30;

    private Rigidbody AIrigidbody;
	// Use this for initialization
	void Start () {
        AIrigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, 100, LayerMask.GetMask("Ground"));
        Debug.Log("hitt", hitt.transform);
        if (hitt.transform != null)
        {
            transform.LookAt(new Vector3(hitt.point.x, transform.position.y, hitt.point.z));
        }
        AIrigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movement;
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        // Get the left-most ray
        Vector3 forward_left = Quaternion.Euler(0, -45, 0) * transform.forward * viewRadius;
        // loop each ray
        for (int i = 0; i <= viewAngleStep; i++)
        {
            Vector3 v = Quaternion.Euler(0, (90.0f / viewAngleStep) * i, 0) * forward_left; ;

            // Create a ray
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();
            // The ray only collide with two layers
            int mask = LayerMask.GetMask("Obstacle", "Enemy");
            Physics.Raycast(ray, out hitt, viewRadius, mask);

            Vector3 pos = transform.position + v;
            if (hitt.transform != null)
            {
                // If the ray hits something, it will be the end point
                pos = hitt.point;
            }
            Debug.DrawLine(transform.position, pos, Color.red);
            if (hitt.transform != null && hitt.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                OnEnemySpotted(hitt.transform.gameObject);
            }
        }
    }

    void OnEnemySpotted(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().spottedFrame = Time.frameCount;
    }
}
