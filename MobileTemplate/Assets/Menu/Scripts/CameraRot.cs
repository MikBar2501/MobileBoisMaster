using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    public static CameraRot main;
    public float rotSpeed = 1.0f;

    Vector3 startRot;
    public Vector3 rotateTo;

    bool rotated = false;

    private void Awake()
    {
        startRot = transform.eulerAngles;
        rotated = false;
        main = this;
    }

    public void Rotate()
    {
        if (rotated)
            Rotate(startRot);
        else
            Rotate(rotateTo);

        rotated = !rotated;
    }

    public void Rotate(Vector3 targetRot)
    {
        StopAllCoroutines();
        StartCoroutine(RotateCo(targetRot));
    }

    IEnumerator RotateCo(Vector3 targetRot)
    {
        while(true)
        {
            transform.eulerAngles = LerpV3(transform.eulerAngles, targetRot, rotSpeed);
            yield return null;
        }
    }

    Vector3 LerpV3(Vector3 from, Vector3 to, float speed)
    {
        return new Vector3(
            Mathf.LerpAngle(from.x, to.x, speed * Time.deltaTime),
            Mathf.LerpAngle(from.y, to.y, speed * Time.deltaTime),
            Mathf.LerpAngle(from.z, to.z, speed * Time.deltaTime)
            );
    }

}
