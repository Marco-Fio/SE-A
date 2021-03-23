using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawnerScript : MonoBehaviour
{
    public GameObject objectToSpawn;

    private SelectionController selectionController;
    private PlacementIndicatorScript placementIndicator;
    public Text textObject;
    public bool objPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicatorScript>();
        selectionController = FindObjectOfType<SelectionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!objPlaced)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
                selectionController.placedObjects.Add(objectToSpawn.GetComponent<PlacementObject>());
                objPlaced = true;
                GameObject.FindGameObjectWithTag("PlacementPanel").SetActive(true);
                GameObject.Find("PlacementIndicator").SetActive(false);
            }
        }
    }
}
