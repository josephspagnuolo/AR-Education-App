using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public GameObject objectToHighlight;
    private Renderer objectRenderer;
    private Color originalColor;
    private Color referenceColor = new Color(255, 219, 0f);
    private bool isHighlighted = false;
    private bool flashingIn = true;
    private bool startedFlashing = false;

    private void Start()
    {
        objectRenderer = objectToHighlight.GetComponent < Renderer>();
        originalColor = objectRenderer.material.color;
        
    }

    public void ChangeObjectMaterial()
    {
        isHighlighted = !isHighlighted;
        if (isHighlighted)
        {
            startedFlashing = !startedFlashing;
            startedFlashing = true;
            StartCoroutine(HighlightFlash());
            Debug.Log("isHighlighted: " + isHighlighted);
            Debug.Log("Original Red: " + originalColor.r * 255);
        }
        else
        {
            startedFlashing = false;
            Debug.Log("isHighlighted: " + isHighlighted);
        }
    }

    private IEnumerator HighlightFlash()
    {
        objectRenderer.material.color = referenceColor;
        while (startedFlashing)
        {
            yield return new WaitForSeconds(0.08f);

            if (flashingIn)
            {
                if (objectRenderer.material.color.r * 255 <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    objectRenderer.material.color = new Color32(
                        (byte)(objectRenderer.material.color.r * 255 - 25),
                        (byte)(objectRenderer.material.color.g * 255 - 25),
                        (byte)(objectRenderer.material.color.b),255);
                }
            }
            else
            {
                if (objectRenderer.material.color.r * 255 >= 150)
                {
                    flashingIn = true;
                }
                else
                {
                    objectRenderer.material.color = new Color32(
                        (byte)(objectRenderer.material.color.r * 255 + 25),
                        (byte)(objectRenderer.material.color.g * 255 + 25),
                        (byte)(objectRenderer.material.color.b), 255);
                }
            }
        }
        objectRenderer.material.color = originalColor;
    }
}
