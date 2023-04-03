using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text angleText;
    public TMP_Text IterationText;
    public void ChangeAngleText(float Value)
    {
        angleText.text = "Angle:" + Value.ToString() ;
    }
    public void ChangeIterationText(float Value)
    {
        IterationText.text = "Iteration:" + Value.ToString();
    }
}
