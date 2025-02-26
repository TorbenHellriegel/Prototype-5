using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle1;
    public ParticleSystem explosionParticle2;
    public int pointValue = 0;

    private GameManager gameManager;
    private Rigidbody targetRb;
    private float minSpeed = 10;
    private float maxSpeed = 15;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    private float aimBotDestructionTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        
        // Starts the object of with a random starting position and torque
        transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        aimBotDestructionTimer -= Time.deltaTime;

        // Destroys the good food after a certain time when aimbot is on
        if(aimBotDestructionTimer < 2 && gameManager.aimBotOn && gameObject.CompareTag("Good"))
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle1, transform.position, explosionParticle1.transform.rotation);
            Instantiate(explosionParticle2, transform.position, explosionParticle2.transform.rotation);
            Destroy(gameObject);
        }
    }

    // Destroy game object when the player clicks it
    // Also increase/decrease score when the player clicks good/bad food
    private void OnMouseEnter()
    {
        if(gameManager.isGameActive && !gameManager.timePaused && Input.GetMouseButton(0))
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle1, transform.position, explosionParticle1.transform.rotation);
            Instantiate(explosionParticle2, transform.position, explosionParticle2.transform.rotation);
            Destroy(gameObject);
        }
    }

    // Destroy game object when it collides with the sensor
    // Also decrease lives when the player missed good food
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("Good") && gameManager.isGameActive)
        {
            gameManager.UpdateScore(-pointValue);
            gameManager.UpdateLives(1);
        }
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