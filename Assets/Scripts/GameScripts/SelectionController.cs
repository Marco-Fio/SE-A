using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{

    public List<PlacementObject> placedObjects;
    public Camera arCamera;
    private ObjectSpawnerScript objectSpawnerScript; 
    public Text textObject;
    private GameObject lastSelected;
    private Vector3 lastPos;

    private void Start()
    {
        objectSpawnerScript = FindObjectOfType<ObjectSpawnerScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!objectSpawnerScript.objPlaced)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                ChangeSelectedObject(GetTouchedObject());
            }
        }
    }

    public GameObject GetTouchedObject()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject))
        {
            return hitObject.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void ChangeSelectedObject(GameObject selected)
    {
        if(lastSelected != null)
        {
            lastSelected.transform.position = lastPos;
        }
        if(selected != null)
        {
            lastPos = selected.transform.position;
            selected.transform.position += new Vector3(0, 0.5f, 0);
            lastSelected = selected;
        }
    }
}
