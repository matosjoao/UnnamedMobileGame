using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    private int _maximum;
    private int _current;

    private void GetCurrentFill()
    {
        float fillAmount = (float)_current / (float)_maximum;
        mask.fillAmount = fillAmount;
    }
}
