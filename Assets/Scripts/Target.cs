using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 10;
    private float maxSpeed = 15;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        
        transform.position = RandomSpawnPos();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy game object when the player clicks it
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    // Destroy game object when it collides with the sensor
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    // Returns a random force
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Returns a random torque
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Returns a random spawn position
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }
}