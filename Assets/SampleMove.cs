using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SampleMove : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    [SerializeField] private float speed;
    

    // Start is called before the first frame update
    void Start() {
        TryGetComponent(out rb);
        TryGetComponent(out anim);
    }

    // Update is called once per frame
    void Update() {
        Move();
    }


    void Move() {
        var z = Input.GetAxis("Vertical");

        if (z != 0) {
            rb.velocity = new(rb.velocity.x, rb.velocity.y, z * speed);
            anim.SetFloat("Run", 0.5f);
        }
        else {
            rb.velocity = Vector3.zero;
            anim.SetFloat("Run", 0);
        }
    }
}
