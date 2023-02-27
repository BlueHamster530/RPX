using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponType
{
    None = 0, ShortRange, LongRange
}
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
    bool bIsGrounded;


    #region OnDashVariable
    bool bIsDash = false;//chech dashing
    bool bCanDash = true;
    [SerializeField]
    float fDashTime = 0.5f;//돌진 시간
    [SerializeField]
    float fDashPower = 10.0f;
    [SerializeField]
    float fDashCooldown = 1.0f;
    #endregion

    #region JumpVariable
    bool bCanJump = true;
    bool bIsJump = false;
    [SerializeField]
    float fJumpPower = 10.0f;
    #endregion

    #region WeaponVariable
    PlayerWeaponType nNowWeaponType = PlayerWeaponType.None;
    #endregion

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Init();
    }
    private void Init()
    {
        moveDirection = Vector3.zero;
        bIsLookLeft = false;
        bIsDash = false;
        bCanDash = true;
        bCanJump = true;
        bIsJump = false;
        bIsGrounded = false;
        transform.rotation = Quaternion.Euler(0, fDefaultAnlge, 0);//OnStart, Setting Player Look Right
        nNowWeaponType = PlayerWeaponType.None;
       
    }

    #region Function_HorizonMoveAndDash
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
          moveDirection = new Vector3(_fvalue, rigid.velocity.y, 0);
    }
    public void OnDash()
    {
        if (bIsDash == true)
            return;

        StartCoroutine("StartDash");

    }
    #endregion

    #region Function_Jump
    public void OnJump()
    {
        if (bCanJump == false)
            return;

        if (bIsJump == false)
        {
            bIsJump = true;
            bCanJump = false;
            rigid.velocity = new Vector3(rigid.velocity.x, fJumpPower, 0);
        }

    }
    private void CheckGround()
    {
        if (rigid.velocity.y<0)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(rigid.position, Vector3.down, out rayHit, 1.2f, LayerMask.GetMask("Platform")))
            {
                    Debug.DrawRay(rigid.position, Vector3.down* 1.2f, Color.red);
                if (bCanJump == false)
                {
                    bCanJump = true;
                    bIsJump = false;
                }
            }
            else
            {
                Debug.DrawRay(rigid.position, Vector3.down * 1.2f, Color.green);
                if (bCanJump == true) bCanJump = false;
            }
        }
    }
    #endregion

    private void FixedUpdate()
    {
        CheckGround();
        //Controller.Move(moveDirection);
        if (bIsDash == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, qTargetAngle, Time.deltaTime * 20);
            rigid.velocity = new Vector3(moveDirection.x * fMoveSpeed * Time.deltaTime,rigid.velocity.y,0);
        }
    }
}
