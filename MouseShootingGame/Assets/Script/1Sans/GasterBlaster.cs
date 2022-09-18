using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasterBlaster : MonoBehaviour
{
    [SerializeField] GameObject blast;
    public void Shoot()
    {
        blast.SetActive(true);
        StartCoroutine(SelfDestroy());
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
