using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour


{
    private CharacterController cc;
    private Animator anim;

    [SerializeField] private Camera mainCamera;
    private Vector3 camF;
    private Vector3 camR;

    [SerializeField] private float speed;
    [SerializeField] private float horizontalMov;
    [SerializeField] private float verticalMov;
    private Vector3 playerMov;
    private Vector3 playerDirecction;
    

    private void Awake(){
        cc= GetComponent<CharacterController>();
        anim= transform.GetChild(0).GetComponent<Animator>();
    }
    // Start is called before the first frame update
    FMODUnity.StudioEventEmitter emisorCaminata;
    void Start()
    {
    
        emisorCaminata = GameObject.Find("pasos").GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMov= Input.GetAxis("Horizontal");
        verticalMov= Input.GetAxis("Vertical");
        playerMov= new Vector3(horizontalMov, 0, verticalMov);
        playerMov= Vector3.ClampMagnitude(playerMov, 1);

        //sonido pasos : https://www.youtube.com/watch?v=Uy1dX7bOPG4&t=947s
        if (horizontalMov != 0)
        {
            emisorCaminata.Play();
        }
        else
        {
            emisorCaminata.Stop();
        }
        emisorCaminata.Play();
        //Obtener dirección de la cámara
        camF= mainCamera.transform.forward;
        camR= mainCamera.transform.right;
        camF.y= 0;
        camR.y= 0;
        camF= camF.normalized;
        camR= camR.normalized;
        playerDirecction= playerMov.x * camR + playerMov.z * camF;
    
    
        //
        anim.SetFloat("v",cc.velocity.magnitude);
        cc.transform.LookAt(cc.transform.position + playerDirecction);
        cc.Move(playerDirecction * speed * Time.deltaTime);
    }
}
