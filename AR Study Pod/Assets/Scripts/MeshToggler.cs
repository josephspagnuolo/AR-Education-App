using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshToggler : MonoBehaviour
{
    public GameObject AnimalCellObject;
    private bool isActive = true;

    public void Toggle()
    {
        if (isActive)
        {
            AnimalCellObject.SetActive(false);
            isActive = false;
        }
        else
        {
            AnimalCellObject.SetActive(true);
            isActive = true;
        }
    }
}
