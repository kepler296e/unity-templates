using UnityEngine;
using System.Collections.Generic;

public class MousePickUp : MonoBehaviour
{
    private float maxPickUpDistance = 3f;
    private float scrollSpeed = 10f;
    private float rotationSpeed = 5f;
    private float throwForce = 400f;

    private int layerMask;
    private RaycastHit hit;
    private Vector3 forward;

    private List<GameObject> takenObjects = new List<GameObject>();
    private List<GameObject> savedObject = new List<GameObject>();

    private MouseLook mouseLook;

    private bool taking;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Objects");
        mouseLook = GetComponent<MouseLook>();
    }

    void Update()
    {
        forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, maxPickUpDistance, layerMask))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject obj = hit.transform.gameObject;
                if (!takenObjects.Contains(obj)) Take(obj);
            }
            DrawRay(Color.green);
        }
        else DrawRay(Color.red);

        if (takenObjects.Count > 0)
        {
            foreach (GameObject obj in takenObjects) Freeze(obj);

            if (!taking && Input.GetButtonDown("Fire1")) Release(takenObjects[0]);
            if (Input.GetButtonDown("Fire2")) Throw(takenObjects[0]);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) takenObjects[0].transform.position += forward * scrollSpeed * Time.deltaTime;
            if (scroll < 0f) takenObjects[0].transform.position -= forward * scrollSpeed * Time.deltaTime;

            if (Input.GetButton("Fire3"))
            {
                float x = Input.GetAxis("Mouse X") * rotationSpeed;
                float y = Input.GetAxis("Mouse Y") * rotationSpeed;
                takenObjects[0].transform.localRotation *= Quaternion.Euler(y, x, 0);
                mouseLook.enabled = false;
            }
            else mouseLook.enabled = true;
        }
    }

    void DrawRay(Color color)
    {
        Debug.DrawRay(transform.position, forward * maxPickUpDistance, color);
    }

    void Take(GameObject obj)
    {
        takenObjects.Add(obj);
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.transform.SetParent(transform);
        taking = true;
        Invoke("FinishTaking", 0.1f);
    }

    void FinishTaking() { taking = false; }

    void Release(GameObject obj)
    {
        takenObjects.Remove(obj);
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.transform.SetParent(null);
    }

    void Freeze(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Throw(GameObject obj)
    {
        Release(obj);
        obj.GetComponent<Rigidbody>().AddForce(forward * throwForce);
    }
}