using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    public int pts;
    //public Text input;
    public TextMeshProUGUI text;

    void Start()
    {
        pts = PlayerPrefs.GetInt("pts",0);
        text.text = pts.ToString() + "pts";
    }

    public void OnAddPoints(int ptsAdd)
    {
        /*int ptsAdd = int.Parse(input.text);*/
        StartCoroutine(AddPoints(ptsAdd));
    }

    IEnumerator AddPoints(int pointsToAdd)
    {
        int i = 0;
        while (i < pointsToAdd)
        {
            i++;
            text.text = (pts + i).ToString() + "pts";
            yield return null;
        }
        pts = pts + i;
        PlayerPrefs.SetInt("pts", pts);
    }
}
