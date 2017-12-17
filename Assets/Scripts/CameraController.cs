
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 75f;
    public float minY = 20f;
    public float maxY = 200f;

    public Vector2 panLimit;
	
	// Update is called once per frame
	void Update () {

        //Save camera location
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.GetKey("up")  || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {   //Move in the specified direction by the amount specified in the variable. Time.deltatime to ensure updating is consistent
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.GetKey("down") || Input.mousePosition.y <= panBorderThickness)
        {   //Move in the specified direction by the amount specified in the variable. Time.deltatime to ensure updating is consistent
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.GetKey("right") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {   //Move in the specified direction by the amount specified in the variable. Time.deltatime to ensure updating is consistent
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.GetKey("left") || Input.mousePosition.x <= panBorderThickness)
        {   //Move in the specified direction by the amount specified in the variable. Time.deltatime to ensure updating is consistent
            pos.z -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);


        //Set current position = new position (pos is new position)
        transform.position = pos;

	}
}
