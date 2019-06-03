using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Router : MonoBehaviour {

    public TextMeshPro leftText;
    public TextMeshPro rightText;

    public int leftDistance;
    public int rightDistance;

    private void Start() {
        leftText.text = leftDistance.ToString();
        rightText.text = rightDistance.ToString();
    }
}
