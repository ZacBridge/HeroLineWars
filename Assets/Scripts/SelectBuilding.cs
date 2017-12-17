using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectBuilding : MonoBehaviour {

    private Renderer rend;

    public Canvas BuildingCanvas;
    public Canvas CharcterCanvas;


    private void Awake()
    {
        //mat = Resources.Load("mobspawnermat", typeof(Material)) as Material;
        rend = GetComponent<Renderer>();
        BuildingCanvas.enabled = false;
        CharcterCanvas.enabled = true;
    }

    void Update()
    {
        BuildingSelected();
    }

    void BuildingSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (!EventSystem.current.IsPointerOverGameObject()) // if anything OTHER than canvas is clicked, then do this
            {
                if (hit)
                {
                    if (hitInfo.transform.gameObject.tag == "Team1Building")
                    {
                        Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                        BuildingCanvas.enabled = true;
                        CharcterCanvas.enabled = false;
                    }
                    else
                    {
                        BuildingCanvas.enabled = false;
                        CharcterCanvas.enabled = true;
                        Debug.Log("I have hit, just not a building with the right tag");
                    }
                }
                else
                {
                    Debug.Log("No hit");
                    CharcterCanvas.enabled = true;
                    BuildingCanvas.enabled = false;
                }
                Debug.Log("Mouse is down");
            }
        }
    }


    void SetColor()
    {
        rend.material.SetColor("mobspawnermat", Color.red);
    }

}


