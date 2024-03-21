using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchView : MonoBehaviour
{

    public GameObject BrainObject;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    private int activeView = 1;
    public string hexColor = "#9FBAA7";
    private List<Vector3[]> viewPositions = new List<Vector3[]>();
    private List<Quaternion[]> viewRotations = new List<Quaternion[]>();

    void Start()
    {
        viewPositions.Add(new Vector3[] {
            new Vector3(0.13f, 1.77f, -0.157f),
            new Vector3(0.15f, 1.77f, -0.157f),
	    new Vector3(-0.18f, 1.47f, -0.35f),
            new Vector3(0.2f, 1.4f, -0.37f),
	    new Vector3(-0.22f, 2.084f, -0.25534f),
            new Vector3(0.22f, 2.0954f, -0.242f),
	    new Vector3(-0.3f, 1.67f, -0.3927f),
            new Vector3(0.3f, 1.67f, -0.364f),
	    new Vector3(-0.22f, 2.084f, -0.2553f),
            new Vector3(0.22f, 2.0954f, -0.242f),
	    new Vector3(-0.3f, 1.67f, -0.3927f),
            new Vector3(0.3f, 1.67f, -0.3639f)
	});
	viewRotations.Add(new Quaternion[] {
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0)
	});

        viewPositions.Add(new Vector3[] {
            new Vector3(0.14f, 1.56f, -0.156f),
            new Vector3(0.346f, 1.56f, -0.157f),
	    new Vector3(-0.18f, 1.037f, -0.895f),
            new Vector3(0.419f, 1.011f, -0.879f),
	    new Vector3(-0.929f, 2.084f, -0.2553f),
            new Vector3(0.9256f, 2.0954f, -0.242f),
	    new Vector3(-1f, 1.213f, -0.393f),
            new Vector3(1.07f, 1.25f, -0.364f),
	    new Vector3(-0.929f, 2.084f, -0.2553f),
            new Vector3(0.9256f, 2.0954f, -0.242f),
	    new Vector3(-1f, 1.213f, -0.393f),
            new Vector3(1.07f, 1.25f, -0.364f)
	});
	viewRotations.Add(new Quaternion[] {
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0)
	});

        viewPositions.Add(new Vector3[] {
            new Vector3(-0.15f, 1.77f, -0.175f),
            new Vector3(0.15f, 1.77f, -0.157f),
	    new Vector3(-0.2f, 1.4f, -0.37f),
            new Vector3(0.2f, 1.4f, -0.37f),
	    new Vector3(-0.15f, 1.77f, -0.175f),
            new Vector3(0.22f, 2.0954f, -0.242f),
	    new Vector3(-0.3f, 1.67f, -0.3927f),
            new Vector3(0.3f, 1.67f, -0.364f),
	    new Vector3(-0.22f, 2.084f, -0.2553f),
            new Vector3(0.22f, 2.0954f, -0.242f),
	    new Vector3(-0.3f, 1.67f, -0.3927f),
            new Vector3(0.3f, 1.67f, -0.3639f)
	});
	viewRotations.Add(new Quaternion[] {
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0),
            Quaternion.Euler(-90, 0, 0)
	});

        SetChildTransforms();
    }

    public void SwitchViewFn(int viewIndex)
    {
	activeView = viewIndex;
        if (activeView == 1)
        {
            SetChildTransforms();
            Button1.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
            Button2.image.color = Color.white;
            Button3.image.color = Color.white;
        }
        else if (activeView == 2)
        {
            SetChildTransforms();
            Button1.image.color = Color.white;
            Button2.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
            Button3.image.color = Color.white;
        }
        else
        {
            SetChildTransforms();
            Button1.image.color = Color.white;
            Button2.image.color = Color.white;
            Button3.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
        }
    }

    private void SetChildTransforms()
    {
        Vector3[] positions = viewPositions[activeView - 1];
        Quaternion[] rotations = viewRotations[activeView - 1];

        int childCount = Mathf.Min(BrainObject.transform.childCount, positions.Length);
        for (int i = 0; i < childCount; i++)
        {
	    Transform child = BrainObject.transform.GetChild(i);
	    
            if (activeView != 3)
            {
		if (child.name.Contains("m_R"))
		{
		    child.gameObject.SetActive(false);
		}
		else
		{
		    child.gameObject.SetActive(true);
		}
            }
            else if (child.name.Contains("L"))
            {
                child.gameObject.SetActive(false);
                continue;
            }
            else if (child.name.Contains("m_R"))
            {
                child.gameObject.SetActive(true);
            }

            if (i < positions.Length)
            {
                child.localPosition = positions[i];
                child.localRotation = rotations[i];
            }
        }
    }
}
