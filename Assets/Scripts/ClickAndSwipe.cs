using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
/*************************************************************
* This code will ensure that a TrailRenderer and BoxCollider *
* are on the GameObject the script is attached to            *
*************************************************************/

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled =false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true; // set swiping to true when the left mouse button is down
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            // Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

    // Set up the GameObject to move with the mouse position
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
        /*************************************************************************************************
        * ScreenToWorld will convert the screen position of the mouse to a world position. The reason we *
        * use 10.0f on the z axis, is because the camera has the z position of -10.0f                    *
        *************************************************************************************************/
    }

    // Update the TrailRenderer and BoxCollider
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }
}
