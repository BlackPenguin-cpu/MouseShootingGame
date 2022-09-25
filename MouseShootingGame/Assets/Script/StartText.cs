using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartText : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.localPosition += new Vector3(0, Mathf.Sin(Time.time) * 20, 0) * Time.deltaTime;
    }
}
