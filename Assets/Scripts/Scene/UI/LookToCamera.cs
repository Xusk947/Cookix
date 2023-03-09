using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        if (Camera.main == null) return;
        transform.LookAt(Camera.main.transform);
    }
}
