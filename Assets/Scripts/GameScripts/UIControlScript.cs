using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlScript : MonoBehaviour
{
    private GameObject field;
    public int rotateAngle;
    public float ScaleFactor;
    private Vector3 scaleFactor;
    private ObjectSpawnerScript objectSpawner;

    void Awake()
    {
        ScaleFactor *= 0.01f;
        scaleFactor = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        field = GameObject.Find("Phase1");
        objectSpawner = FindObjectOfType<ObjectSpawnerScript>();
    }

    public void ResetBtnClick()     //disable placement buttons, show indicator, enable spawner
    {
        Destroy(field);
        GameObject.FindGameObjectWithTag("PlacementPanel").SetActive(false);
        GameObject.Find("PlacementIndicator").SetActive(true);
        objectSpawner.objPlaced = false;
    }

    public void RotateLeftBtnClick()
    {
        field.transform.Rotate(Vector3.up, rotateAngle);
    }

    public void RotateRightBtnClick()
    {
        field.transform.Rotate(Vector3.up, -rotateAngle);
    }

    public void ScaleUpBtnClick()
    {
        field.transform.localScale += scaleFactor;
    }

    public void ScaleDownBtnClick()
    {
        field.transform.localScale -= scaleFactor;
    }

    public void AcceptBtnClick()
    {
        
    }
}
