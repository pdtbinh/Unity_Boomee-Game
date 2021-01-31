using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CopyScore : MonoBehaviour
{
    public Text mainScore;

    public TextMeshProUGUI endPanelScore;

    private void OnEnable()
    {
        endPanelScore.text = mainScore.text;
    }


}
