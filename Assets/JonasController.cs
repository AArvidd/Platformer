using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JonasController : MonoBehaviour
{
    [SerializeField]
    int speed = 5;

    [SerializeField]
    int jump;

    [SerializeField]
    LayerMask groundLayer;

    Rigidbody2D rBody;
    bool jumped = true;

    float groundRadius = 0.1f;

    [SerializeField]
    Transform feet;

    Vector2 bottomColideorSise = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        bottomColideorSise.y = 0.1f;
        bottomColideorSise.x = GetComponent<Collider2D>().bounds.size.x * 0.9f;
    }


    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed);

        //bool isGrounded = Physics2D.OverlapCircle(GetFoot(), groundRadius, groundLayer);
        bool isGrounded = Physics2D.OverlapBox(GetFoot(), GetFootSise(), 0, groundLayer);

        if (Input.GetAxisRaw("Jump") > 0 && jumped && isGrounded)
        {
            rBody.AddForce(Vector2.up * jump);
            jumped = false;
        }

        if (Input.GetAxisRaw("Jump") == 0)
        {
            jumped = true;
        }

    }

    private Vector2 GetFoot()
    {
        float hight = GetComponent<Collider2D>().bounds.size.y;
        return transform.position + Vector3.down * hight/2;

    }

    private Vector2 GetFootSise()
    {
        return new Vector2(GetComponent<Collider2D>().bounds.size.x * 0.9f , 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireSphere(GetFoot(), groundRadius);

        Gizmos.DrawCube(GetFoot(), bottomColideorSise);
        
    }

}
