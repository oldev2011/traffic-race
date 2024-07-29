using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyRotator : MonoBehaviour
{
    [SerializeField]private float _rotateSpeed;
    private void Start()
    {
        StartCoroutine(RotateSky());
    } 
    private IEnumerator RotateSky() 
    {
        yield return new WaitForSeconds(0.1f);
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        StartCoroutine(RotateSky());
    }
}
