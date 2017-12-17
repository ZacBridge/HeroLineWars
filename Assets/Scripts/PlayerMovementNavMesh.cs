using System.Collections; 
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class PlayerMovementNavMesh : MonoBehaviour {

    private Vector3 targetPosition; // Where the character wants to move to 

   // const int RIGHT_MOUSE_BUTTON = 0;

    UnityEngine.AI.NavMeshAgent agent;

    // Cache all components
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Use this for initialization
    void Start() {
        // Sets desired position to the current position (transform.position is current position)
        targetPosition = transform.position;

    }

    // Update is called once per frame
    void Update() {
        // Checks to see if the player is holding the right mouse button down
        if (Input.GetMouseButton(1))
            //Runs the SetTargetPosition method
            SetTargetPosition();
        // Runs the MovePlayer method to actually move the character
        MovePlayer();

    }


    void SetTargetPosition()
    {   //Instantiates a new instance of a plane on the y axis of the current position of the object
        Plane plane = new Plane(Vector3.up, transform.position);
        //Instantiates a new instance of a raycast that points from the center of the camera to the current mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
            targetPosition = ray.GetPoint(point);
    }

    // Moves the player to the target location and rotates them to look at the target position and stops them from moving when they get to the target position
    void MovePlayer()
    {
        agent.SetDestination(targetPosition);

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }


}

