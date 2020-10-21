 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public float recoilSpeed = 20f;
    float sideRecoil;
    float upRecoil;
    float maxSidePos;
    float maxSideNeg;
    float checkValue;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = sideRecoil + Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = upRecoil + Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        if (sideRecoil > 0)
        {
            sideRecoil -= recoilSpeed * Time.deltaTime;
        }
        else if(sideRecoil < 0)
        {
            sideRecoil += recoilSpeed * Time.deltaTime;
        }
        upRecoil -= recoilSpeed * Time.deltaTime;
        if ((checkValue < 0 && sideRecoil > 0) || (checkValue >0 && sideRecoil < 0))
        {
            sideRecoil = 0;
            checkValue = 0;
        }
        if(upRecoil < 0)
        {
            upRecoil = 0;
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
        
    }
    public void AddRecoil(float up, float side, float maxNeg, float maxPos)
    {
        sideRecoil += side;
        upRecoil += up;
        maxSideNeg = maxNeg;
        maxSidePos = maxPos;
        checkValue += sideRecoil;
    }
}
