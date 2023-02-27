using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField]
    float fDefaultAnlge = 125.0f;
    // Start is called before the first frame update
    Vector3 moveDirection;
    Quaternion qTargetAngle;
    [SerializeField]
    float fMoveSpeed = 600.0f;
    bool bIsLookLeft;

    //OnDash
    bool bIsDash = false;//chech dashing
    bool bCanDash = true;
    [SerializeField]
    float fDashTime = 0.5f;//돌진 시간
    [SerializeField]
    float fDashPower = 10.0f;
    [SerializeField]
    float fDashCooldown = 1.0f;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        moveDirection = Vector3.zero;
        bIsLookLeft = false;
        bIsDash = false;
        bCanDash = true;
        fDashTime = 1.0f;
        transform.rotation = Quaternion.Euler(0, fDefaultAnlge, 0);//OnStart, Setting Player Look Right
    }
    public void HorizontalDash(float _fvalue)
    {
        if (bIsDash == true || bCanDash == false)
            return;

        moveDirection = new Vector3(_fvalue, 0, 0);
        StartCoroutine("Dash");

    }
    private IEnumerator Dash()
    {
        bCanDash = false;
        bIsDash = true;
        rigid.useGravity = false;
        rigid.velocity = moveDirection * fDashPower;
        yield return new WaitForSeconds(fDashTime);
        bIsDash = false;
        rigid.useGravity = true;
        yield return new WaitForSeconds(fDashCooldown);
        bCanDash = true;
    }

    public void HorizontalMove(float _fvalue)
    {
        if (bIsDash == true)//Can't Control on Dashing;
            return;

          if (_fvalue != 0)
          {
              qTargetAngle = Quaternion.Euler(new Vector3(0, 90 * _fvalue, 0));
              if (_fvalue < 0)
                  bIsLookLeft = true;
              else
                  bIsLookLeft = false;
          }
          else
          {
              if (bIsLookLeft == true)
                  qTargetAngle = Quaternion.Euler(new Vector3(0, -fDefaultAnlge, 0));
              if (bIsLookLeft == false)
                  qTargetAngle = Quaternion.Euler(new Vector3(0, fDefaultAnlge, 0)); 
          }
          moveDirection = new Vector3(_fvalue, 0, 0);
    }
    public void OnDash()
    {
        if (bIsDash == true)
            return;

        StartCoroutine("StartDash");

    }
    private IEnumerator StartDash()
    {
        bIsDash = true;
        float _xDir = 0;
        if (bIsLookLeft == true)
            _xDir = -1;
        else
            _xDir = 1;

        qTargetAngle = Quaternion.Euler(new Vector3(0, 90 * _xDir, 0));
        moveDirection = new Vector3(_xDir, 0, 0);
        fDashTime = 1.0f;

        while (fDashTime > 0)
        {
            fDashTime -= Time.deltaTime;
            rigid.velocity = moveDirection * fMoveSpeed * Time.deltaTime;
        }


        if (bIsLookLeft == true)
            qTargetAngle = Quaternion.Euler(new Vector3(0, -fDefaultAnlge, 0));
        if (bIsLookLeft == false)
            qTargetAngle = Quaternion.Euler(new Vector3(0, fDefaultAnlge, 0));
        bIsDash = false;

        yield return 0;
    }
    private void FixedUpdate()
    {
        //Controller.Move(moveDirection);
        if (bIsDash == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, qTargetAngle, Time.deltaTime * 20);
            rigid.velocity = moveDirection * fMoveSpeed * Time.deltaTime;
        }

    }
}
