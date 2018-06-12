using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Velocidade do Personagem
    public float speed = 6f;

    //Vetor responsavel pelo movimento
    public Vector3 movement;

    //Responsavel pela transição da animação
    public Animator anim;

    //Responsavel pela fisica do objeto
    Rigidbody playerRigidbody;

    //Mascara de chão
    int floorMask;

    //Informações para raycast
    float camRayLenght = 100f;

    void Awake()
    {
        // Atribuir a mascara do chão
        floorMask = LayerMask.GetMask("Floor");

        // Atribuir as referencias
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);


    }


    //Metodo de movimentar o player
    void Move( float h, float v)
    {
        //Determina os movimentos
        movement.Set(h, 0f, v);

        //Normalização do movimento
        movement = movement.normalized * speed * Time.deltaTime;

        //Efetuar movimento
        playerRigidbody.MovePosition(transform.position + movement);

    }

    //Metodo para girar o jogador
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);


        }
    }

    void Animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f);

        anim.SetBool("IsWalking", walking);

    }
}
