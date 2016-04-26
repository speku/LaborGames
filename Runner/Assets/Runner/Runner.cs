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
    }

    void FixedUpdate()
    {
        if (touchingPlatform)
        {
            GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
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