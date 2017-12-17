
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public LayerMask groundlayer;

    [System.Serializable]
    public class PositionSettings
    {
        public bool invertPan = true;
        public float panSmooth = 7;
        public float distanceFromGround = 100;
        public bool allowZoom = true;
        public float zoomSmooth = 5;
        public float zoomStep = 5;
        public float maxZoom = 25;
        public float minZoom = 80;

        [HideInInspector]
        public float newDistance = 40;

        

    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = 50;
        public float yRotation = 0;
        public bool allowYOrbit = true;
        public float yOrbitSmooth = 0.5f;

    }

    [System.Serializable]
    public class InputSettings
    {
        public string PAN = "MousePan";
        public string ORBIT_Y = "MouseTurn";
        public string ZOOM = "Mouse ScrollWheel";

    }


    public float inputX;
    public float inputZ;


    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    Vector3 destination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    Vector3 previousMousePos = Vector3.zero;
    Vector3 currentMousePos = Vector3.zero;
    float panInput, orbitInput, zoomInput;
    int panDirection = 0;

    


    void Start()
    {
        // initialisation code 
        panInput = 0;
        orbitInput = 0;
        zoomInput = 0;
    }


    void getInput()
    {
        // responsible  for setting our input variables
        panInput = Input.GetAxis(input.PAN);
        orbitInput = Input.GetAxis(input.ORBIT_Y);
        zoomInput = Input.GetAxis(input.ZOOM);
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

            previousMousePos = currentMousePos;
        currentMousePos = Input.mousePosition;

    }

    void Update()
    {
        //updating input, zooming, rotating, panning 

        //input
        getInput();

        //Zooming
        if (position.allowZoom)
            Zoom();


        //Rotating
        if (orbit.allowYOrbit)
            Rotate();

        //Panning
        PanWorld();

    }

    void FixedUpdate()
    {
        // handle camera distance -- raycasting goes in fixed update 
        HandleCameraDistance();
    }

    //void PanWorldKeys()
    //{
    //        transform.position += transform.right * inputZ * Time.deltaTime

    //}

    void PanWorld()
    {
        Vector3 targetPos = transform.position;

        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        if (panInput > 0)
        {   //Use Time.deltaTime to make sure if refreshes consistently, regardless of framerate
            targetPos += transform.right * (currentMousePos.x - previousMousePos.x) * position.panSmooth * panDirection * Time.deltaTime;
            //Don't know what our actual forward vector is, so cross the transform.right (my X axis) with the global Up axis for the scene
            targetPos += Vector3.Cross(transform.right, Vector3.up) * (currentMousePos.y - previousMousePos.y) * position.panSmooth * panDirection * Time.deltaTime;

        }

        if (inputX > 0)
        {
            targetPos += transform.right * inputX * Time.deltaTime;
        }

        if (inputZ > 0)
        {
            targetPos += transform.right * inputZ * Time.deltaTime;
        }

        //if (Input.GetKey("Up"))
        //{
        //    targetPos += transform.right * (currentMousePos.x - previousMousePos.x) * position.panSmooth * panDirection * Time.deltaTime;
        //    //Don't know what our actual forward vector is, so cross the transform.right (my X axis) with the global Up axis for the scene
        //    targetPos += Vector3.Cross(transform.right, Vector3.up) * (currentMousePos.y - previousMousePos.y) * position.panSmooth * panDirection * Time.deltaTime;
        //}

        transform.position = targetPos;

    }

    void HandleCameraDistance()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, groundlayer))
        {
            destination = Vector3.Normalize(transform.position - hit.point) * position.distanceFromGround;
            //Adding distance from ground to location hit on ground
            destination += hit.point;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, 0.3f);
        }

    }

    void Zoom()
    {   //New Zoom distance is position of zoomStep variable, multiplied by the negative value of zoomInput (negative zoom out, positive zoom in)
        position.newDistance += position.zoomStep * -zoomInput;

        //Works out how fast to zoom in from the distance from the ground, to the new distance (zoom) set by the user
        position.distanceFromGround = Mathf.Lerp(position.distanceFromGround, position.newDistance, position.zoomSmooth * Time.deltaTime);

        // If the camera distance is less that the maximum zoom position
        if (position.distanceFromGround < position.maxZoom)
        {   //Then set the distance from ground as the maximum zoom value and the new distance as the maximum zoom value
            position.distanceFromGround = position.maxZoom;
            position.newDistance = position.maxZoom;
        }
        // If the camera distance is less that the minimum zoom position
        if (position.distanceFromGround > position.minZoom)
        {//Then set the distance from ground as the minimum zoom value and the new distance as the minimum zoom value
            position.distanceFromGround = position.minZoom;
            position.newDistance = position.minZoom;

        }
    }

    void Rotate()
    {   //If the orbitInput is above 0 (User has press the orbit key)
        if (orbitInput > 0)
        {   //Set the y rotation of the variable to the Current mouse x position, minus the previous mouse x position,
            //multiplied by the yOrbitSmooth value * Time.deltatime (for consistent updating)
            orbit.yRotation += (currentMousePos.x - previousMousePos.x) * orbit.yOrbitSmooth * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0);

    }





}
