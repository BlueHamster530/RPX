using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    GameObject goTarget;//바라볼 목표 오브젝트
    [SerializeField]
    float fOrthographic= 11;//목표 오브젝트와의 거리


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = fOrthographic;
        SetCameraPosition();
    }
    public void SetCameraOrthographicSize(float Value)
    {
        fOrthographic = Value;
        cam.orthographicSize = fOrthographic;
    }
    private void SetCameraPosition()
    {
        //Vector3 tempPosition = goTarget.transform.position;
        //transform.eulerAngles = new Vector3(0, fYAngle, 0);
        //tempPosition = tempPosition - (transform.forward * fDistance);
        //tempPosition = tempPosition + new Vector3(0, fWeight, 0);
        transform.position = new Vector3(goTarget.transform.position.x, goTarget.transform.position.y+6.5f, transform.position.z);
        //transform.LookAt(goTarget.transform);
    }
    // Update is called once per frame
    void Update()
    {
        SetCameraPosition();
    }
}
