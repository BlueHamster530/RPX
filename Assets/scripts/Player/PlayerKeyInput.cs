using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInput : MonoBehaviour
{
    CharController charController;
    PlayerAnimationController AnimController;
    // Start is called before the first frame update
   // int nNowWeapon = 0;

    [SerializeField]
    GameObject[] goWeapons;

    const float fDashCheckTime = 0.3f;
    float fDashCurrentTime = 0.0f;
    float fDashxDirBefore = 0.0f;

    void Start()
    {
        charController = GetComponent<CharController>();
        AnimController = GetComponent<PlayerAnimationController>();
        Init(); 
    }
    private void Init()
    {
       // nNowWeapon = 0;
        fDashCurrentTime = 0.0f;
        fDashxDirBefore = 0.0f;
    }
    private void KeyInputEvent()
    {
        float fx = Input.GetAxisRaw("Horizontal");
        float fz = Input.GetAxisRaw("Vertical");
        if (fDashCurrentTime > 0)
            fDashCurrentTime -= Time.deltaTime;

        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (fDashCurrentTime <= 0)
            {
                fDashCurrentTime = fDashCheckTime;
                fDashxDirBefore = fx;
            }
            else
            {
                if (fDashxDirBefore == fx)
                {
                    charController.HorizontalDash(fx);
                }
                else
                {
                    fDashxDirBefore = fx;
                    fDashCurrentTime = 0;
                }
            }
        }
        charController.HorizontalMove(fx);


    }
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    Vector2 input = context.ReadValue<Vector2>();
    //    if (input != null)
    //    {
    //       // Debug.Log(input);
    //        charController.MoveCharacter(input);
    //    }
    //}
    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        Debug.Log("Attack");
    //        AnimController.GetKeyDonw();
    //        goWeapons[nNowWeapon].SetActive(false);
    //        nNowWeapon++;
    //        if (nNowWeapon >= 3)
    //            nNowWeapon = 0;
    //        goWeapons[nNowWeapon].SetActive(true);

    //    }
    //}
    //public void OnDashMove(InputAction.CallbackContext context)
    //{
    //    float input = context.ReadValue<float>();
    //    if (context.performed)
    //    {
    //        charController.OnDash();
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        KeyInputEvent();
    }
}
