using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{
    const float SPEED = 6f;
    [SerializeField]
    Transform SectionInfo;
    Vector3 desiredScale = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
    }

    public void openInfo()
    {
        desiredScale.x = 0.3f;
        desiredScale.y = 0.3f;
        desiredScale.z = 0.3f;
    }

    public void closeInfo()
    {
        desiredScale = Vector3.zero;
    }
}
