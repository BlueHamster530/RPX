using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    GameObject goTarget;//�ٶ� ��ǥ ������Ʈ
    [SerializeField]
    float fOrthographic= 11;//��ǥ ������Ʈ���� �Ÿ�


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
