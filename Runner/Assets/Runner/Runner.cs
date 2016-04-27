using UnityEngine;

public class Runner : MonoBehaviour
{

    private Vector3 startPosition;
    public float gameOverY;
    public float cameraOffset;

    private InputManager inputManager;

    public float acceleration;
    public Vector3 jumpVelocity, boostVelocity;

    private bool touchingPlatform;

    public float speedIncrement = 0.001f;
    public float speed;
    private bool gameRunning = false;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        enabled = false;
        
    }

    private static int boosts;

    private void GameStart()
    {
        boosts = 0;
        transform.localPosition = startPosition;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        enabled = true;
        gameRunning = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * speed, ForceMode.VelocityChange);
    }

    public static void AddBoost()
    {
        boosts += 1;
    }

    private void GameOver()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        enabled = false;
        gameRunning = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || inputManager.WasTouch())
        {
            if (touchingPlatform)
            {
                GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
                touchingPlatform = false;
            }
            else if (boosts > 0)
            {
                GetComponent<Rigidbody>().AddForce(boostVelocity, ForceMode.VelocityChange);
                boosts -= 1;
            }
        }


        if (transform.localPosition.y < gameOverY || transform.localPosition.x + cameraOffset < Camera.distanceTraveled )
        {
            GameEventManager.TriggerGameOver();
        }
        if (gameRunning) GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * speedIncrement, ForceMode.VelocityChange);

        if (transform.localPosition.x > Camera.distanceTraveled + cameraOffset) transform.localPosition = new Vector3(Camera.distanceTraveled + cameraOffset, transform.localPosition.y, transform.localPosition.z);
    }

    void FixedUpdate()
    {
        //if (touchingPlatform)
        //{
        //    GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        //}
    }

    void OnCollisionEnter()
    {
        touchingPlatform = true;
    }

    void OnCollisionExit()
    {
        touchingPlatform = false;
    }
}