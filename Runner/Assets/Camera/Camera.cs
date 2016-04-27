using UnityEngine;

public class Camera : MonoBehaviour
{

    public static float distanceTraveled;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool gameRunning = false;

    public float speedIncrement = 0.001f;
    public float speed;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private static int boosts;

    private void GameStart()
    {
        distanceTraveled = 0f;
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        gameRunning = true;
       GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * speed, ForceMode.VelocityChange);
    }


    private void GameOver()
    {
        //GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * speed, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameRunning = false;
    }

    void Update()
    {
        if (gameRunning) GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * speedIncrement, ForceMode.VelocityChange);
        //transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        distanceTraveled = transform.localPosition.x;
    }

}