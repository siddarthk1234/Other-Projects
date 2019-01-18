using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] CharacterController cc;
    [SerializeField] private LayerMask layerMask;
    private Vector3 currentLookTarget = Vector3.zero;
   [SerializeField] private float moveSpeed = 5.0f;
    Vector3 direction;
    private Animator anim;
    private BoxCollider[] swordColliders;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();

        
    }

    // Update is called once per frame
    void Update()
    {


        if (!GameManager.instance.GameOver)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            cc.SimpleMove(direction * moveSpeed);
            if (direction == Vector3.zero)
            {
                anim.SetBool("IsRunning", false);
            }

            else
            {
                anim.SetBool("IsRunning", true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("SpinAttack");
            }
        }
        
    }

    void FixedUpdate()
    {


        if (!GameManager.instance.GameOver)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

            if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != currentLookTarget)
                {
                    currentLookTarget = hit.point;
                }

                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

            }
        }
        
    }


    public void BeginAttack()
    {
        foreach(var weapon in swordColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
        }
    }
}
