using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    private string UpKey, DownKey, LeftKey, RightKey, Dash;
    [SerializeField]
    private Vector3 NW, NE, SW, SE;
    [SerializeField]
    private float PlayerSpeed, DashSpeed;
    [SerializeField]
    private GameObject PlayerAvatar;
    [SerializeField]
    private string FacingWhat; // Unused currently
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    float DashCooldown;
    [SerializeField]
    float DashTimer;
    [SerializeField]
    GameObject DashSphere;
    [SerializeField]
    private bool IsDashing = false;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = gameObject.GetComponent<Rigidbody>();
        NW = new Vector3(-1, 0, 1);
        NE = new Vector3(1, 0, 1);
        SW = new Vector3(-1, 0, -1);
        SE = new Vector3(1, 0, -1);
    }


    void FixedUpdate()
    {

        if(IsDashing)
        {
            DashTimer++;
            if(DashTimer >= DashCooldown)
            {
                DashTimer = 0;
                IsDashing = false;
                DashSphere.SetActive(false);
            }
        }

        if(Input.GetKey(UpKey) && Input.GetKey(LeftKey))
        {
            rb.MovePosition(transform.position + NW * Time.deltaTime * PlayerSpeed * .7f);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, -45, 0);
            FacingWhat = "ul";
        }

        else if (Input.GetKey(UpKey) && Input.GetKey(RightKey))
        {
            rb.MovePosition(transform.position + NE * Time.deltaTime * PlayerSpeed * .7f);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 45, 0);
            FacingWhat = "ur";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(LeftKey))
        {
            rb.MovePosition(transform.position + SW * Time.deltaTime * PlayerSpeed * .7f);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, -135, 0);
            FacingWhat = "dl";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(RightKey))
        {
            rb.MovePosition(transform.position + SE * Time.deltaTime * PlayerSpeed * .7f);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 135, 0);
            FacingWhat = "dr";
        }

        else if (Input.GetKey(UpKey))
        {

            rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 0, 0);
            FacingWhat = "u";
        }

        else if (Input.GetKey(DownKey))
        {
            rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 180, 0);
            FacingWhat = "d";
        }

        else if (Input.GetKey(LeftKey))
        {
            rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 270, 0);
            FacingWhat = "l";
        }
        else if (Input.GetKey(RightKey))
        {
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 90, 0);
            FacingWhat = "r";
        }

        if(Input.GetKeyDown(Dash) && (!IsDashing))
        {
            Vector3 dash = DashDirection(FacingWhat);
            if (dash == NW || dash == NE || dash == SW || dash == SE)
            {
                rb.AddForce(dash * DashSpeed * .7f);
                IsDashing = true;
                DashSphere.SetActive(true);
            }

            else
            {
                rb.AddForce(dash * DashSpeed);
                IsDashing = true;
                DashSphere.SetActive(true);
            }
        }

    }

    private Vector3 DashDirection(string direction)
    {
        switch (direction)

        {
            case "ul":
                return NW;

            case "ur":
                return NE;

            case "dl":
                return SW;

            case "dr":
                return SE;

            case "u":
                return Vector3.forward;

            case "d":
                return Vector3.back;

            case "l":
                return Vector3.left;

            case "r":
                return Vector3.right;
        }
        
        return new Vector3(0, 0, 0);
    }

}