using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    public Transform targetPoint;
    public float throwForce = 12f;
    public float upwardForce = 2f;

    private InputAction throwAction;
    private bool hasThrown = false;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
        throwAction = new InputAction("Throw", binding: "<Keyboard>/Space");
        throwAction.performed += ctx => ThrowBall();
    }
    void OnEnable()
    {
        throwAction.Enable();
    }
    void OnDisable()
    {
        throwAction.Disable();
    }
    void OnDestroy()
    {
        throwAction.Dispose();
    }
    private void ThrowBall()
    {
        if (hasThrown) return;
        Vector3 direction;
        if(targetPoint != null) direction = (targetPoint.position - transform.position).normalized;
        else direction = transform.forward;
        Vector3 throwVector = (direction + Vector3.up * .15f).normalized;
        GetComponent<Rigidbody>().AddForce(throwVector * throwForce + Vector3.up * upwardForce, ForceMode.Impulse);
        hasThrown = true;
    }

    private void Update()
    {
        if(Keyboard.current!=null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        hasThrown = false;
    }
}