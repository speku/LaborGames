using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public float swipeThreshold;

    public enum Swipes
    {
        Up,
        Down
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    public bool WasTouch()
    {
        foreach (var t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    public bool WasSwipe(Swipes swipe)
    {

        foreach (Touch touch in Input.touches)
        {
            switch (swipe)
            {
                case Swipes.Down:
                    if (touch.deltaPosition.y <= swipeThreshold)
                    {
                        return true;
                    }
                    break;
                case Swipes.Up:
                    if (touch.deltaPosition.x >= swipeThreshold)
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }
    
}
