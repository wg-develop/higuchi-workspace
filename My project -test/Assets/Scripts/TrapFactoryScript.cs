using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapFactoryScript : MonoBehaviour
{
    public GameObject[] trapGameObjects;
    private GameObject trapGameObject;
    private Vector3 mousePosition;
    public GameObject mainCameraGameObject;
    public Button[] buttonGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = -1 * mainCameraGameObject.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (trapGameObject)
        {
            trapGameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
        }
        if (trapGameObject && Input.GetMouseButtonDown(0))
        {
            CommonScript.trapGameObjects.Add(trapGameObject);
            trapGameObject = null;
            Invoke(nameof(SetInteractableButton), 1.0f);
        }
        if (trapGameObject && Input.GetMouseButtonDown(1))
        {
            Destroy(trapGameObject);
            Invoke(nameof(SetInteractableButton), 1.0f);
        }
    }
    public void CreateTrap(int num)
    {
        switch (num)
        {
            case 1:
                trapGameObject = Instantiate(trapGameObjects[0], mousePosition, Quaternion.identity);
                break;
            case 2:
                trapGameObject = Instantiate(trapGameObjects[1], mousePosition, Quaternion.identity);
                break;
            case 3:
                trapGameObject = Instantiate(trapGameObjects[2], mousePosition, Quaternion.identity);
                break;
        }
        SetInteractableButton(false);
    }
    public void SetInteractableButton()
    {
        SetInteractableButton(true);
    }
    public void SetInteractableButton(bool flag)
    {
        foreach (Button buttonGameObject in buttonGameObjects)
        {
            buttonGameObject.interactable = flag;
        }
    }

}
