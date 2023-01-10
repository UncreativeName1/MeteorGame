using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckValid : MonoBehaviour
{
    List<(bool isInt, float min, float max)> bounds = new List<(bool isInt, float min, float max)> {
        (true, 1f, 999999), // max health (0)
        (true, 0f, 999999), // max shield health (1)
        (true, 0f, 999999), // starting shield health (2)
        (true, 0f, 999999), // base regen amount (3)
        (false, 0.001f, 999999f), // regen interval (4)
        (true, 0f, 999999), // movement speed (5)
        (true, 1f, 999999), // fire speed (6)
        (true, 0f, 999999), // bullet dmg (7)
        (false, 0f, 999999), // meteor spawn frequency (8)
        (false, 0.001f, 999999f), // meteor guarantee (9)
        (true, 1f, 999999), // prime bounces (10)
        (true, 0f, 999999), // meteor time limit (11)
        (false, 0f, 999999f), // powerup spawn frequency (12)
        (false, 0.001f, 999999f) // powerup guarantee (13)
    };

    public void Check(int index) {
        bool isInt = bounds[index].isInt;
        float min = bounds[index].min;
        float max = bounds[index].max;

        // alr 0 here, even though float gives 0.001f
        //Debug.Log(min);
        //Debug.Log(max);

        String str = gameObject.GetComponent<TMP_InputField>().text;
        if (isInt) {
            int iNum;
            bool validInput = Int32.TryParse(str, out iNum);
            if (!validInput) {
                //Debug.Log("Invalid Format.");
                gameObject.GetComponent<Image>().color = new Color32(255, 210, 210, 255);
            } else if ( ((int)min > iNum) || ((int)max < iNum) ) {
                //Debug.Log("Number must be between " + (int)min + " and " + (int)max + " inclusive.");
                gameObject.GetComponent<Image>().color = new Color32(255, 210, 210, 255);
            } else {
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        } else {
            float fNum;
            bool validInput = Single.TryParse(str, out fNum);
            if (!validInput) {
                //Debug.Log("Invalid Format.");
                gameObject.GetComponent<Image>().color = new Color32(255, 210, 210, 255);
            } else if ((min > fNum) || (max < fNum)) {
                //Debug.Log(min);
                //Debug.Log(max);
                //Debug.Log(fNum);
                //Debug.Log("Number must be between " + min + " and " + max + " inclusive.");
                gameObject.GetComponent<Image>().color = new Color32(255, 210, 210, 255);
            } else {
                //Debug.Log(min);
                //Debug.Log(max);
                //Debug.Log(fNum);
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
