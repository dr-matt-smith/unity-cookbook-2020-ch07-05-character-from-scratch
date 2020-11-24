


using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float speed = 3;
    public float rotateSpeed = 1;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Rotate(0, h * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * v;
        controller.SimpleMove(forward * curSpeed);
        
        // tell Animator current speed
        _animator.SetFloat ("Speed", v);

    }
}