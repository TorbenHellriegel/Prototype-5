using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollow : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Enables the trail as long as the mouse button is pressed
        if(Input.GetMouseButtonDown(0))
        {
            trail.enabled = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            trail.Clear();
            trail.enabled = false;
        }

        // Set the trail position to the mouse position
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }
}
