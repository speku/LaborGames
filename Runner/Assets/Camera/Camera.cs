using UnityEngine;

public class Camera : MonoBehaviour
{

    public static float distanceTraveled;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool gameRunning = false;

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
    }


    private void GameOver()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameRunning = false;
    }

    void Update()
    {
        if (gameRunning) transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        distanceTraveled = transform.localPosition.x;
    }

}