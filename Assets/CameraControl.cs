using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform CameraTransform;
    public GameObject GrassTileMap;
    public float[] yLimits;

    public void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && CameraTransform.position.y < yLimits[0])
            Move(true);
        else if (Input.GetKey(KeyCode.DownArrow) && CameraTransform.position.y > yLimits[1])
            Move(false);
    }

    void Move(bool up)
    {
        if (up)
            CameraTransform.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
        else CameraTransform.position += new Vector3(0f, -2f * Time.deltaTime, 0f);

        if (CameraTransform.position.y >= 0f)
            GrassTileMap.SetActive(true);
        else GrassTileMap.SetActive(false);
    }
}
