using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mov : MonoBehaviour
{
    private enum States{ passive, aggressive, dead};

    [Header("Datos enemigo")]
    [SerializeField] private States state; //Estado 
    [SerializeField] private float speed;
    [SerializeField] private float run;
    [SerializeField][Tooltip("¿A quien sigo?")] private LayerMask follow;
    [SerializeField][Tooltip("Puntos de patrullaje")] private Transform[] Points;

    [Header("Datos pasivo")]
    [SerializeField][Tooltip("Comportamiento aleatorio")] private bool RandomMov;
    [SerializeField][Tooltip("intervalos de tiempo para el comportamiento")] private float timeStop;
    [SerializeField][Tooltip("¿Se detendrá en sus puntos de vigilancia?")] private bool stopMove;

    [Header("Datos agresivo")]
    [SerializeField][Tooltip("El mob ¿Puede ser agresivo?")] private bool aggressive;
    [SerializeField][Range(1f,40f)][Tooltip("Rango de persecución")] private float RangeChasing;
    [SerializeField][Range(1f,40f)][Tooltip("Rango de visión")] private float RangeVision;
    [SerializeField][Tooltip("¿Esta persiguiendo a su objetivo?")] private bool chasing;

    //[Header("Datos Muerte")]
    //[SerializeField][Tooltip("¿Esta muerto el mob?")] private bool die;
    
    //Variables auxiliares
    private int iPoint;
    private float time;
    private int rutina;
    private Quaternion angulo;  
    private Transform Player;

    private void Awake(){
        Player= GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //die= false;
        iPoint= 0;
        state= States.passive;
        if(RangeVision > RangeChasing){ 
            RangeVision= RangeChasing;
            RangeChasing += 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        float range= (chasing)? RangeChasing :RangeVision;
        chasing= Physics.CheckSphere(transform.position, range, follow);
        switch (state){
            case States.passive:
                if(chasing && aggressive){
                    state= States.aggressive;
                    break;
                }
                if(RandomMov){    
                    time -= Time.deltaTime;
                    if(time <= 0){
                        rutina = Random.Range(0,4);
                        time= timeStop;
                    }
                    switch(rutina){
                        case 0: //idle
                            break;
                        case 1: //gira
                            angulo= Quaternion.Euler(0, Random.Range(0,360), 0);
                            rutina++;
                            break;
                        case 2: //mov
                            transform.rotation= Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                            break;
                        default:
                            rutina= 0;
                            break;
                    }
                }else{
                    if(Points.Length > 1){
                        transform.LookAt(new Vector3(Points[iPoint].position.x,
                                                        transform.position.y,
                                                        Points[iPoint].position.z));
                        transform.position= 
                                Vector3.MoveTowards(transform.position, Points[iPoint].position, 
                                                    speed *Time.deltaTime);
                        if(Vector3.Distance(transform.position, Points[iPoint].position)<0.2f){
                            if(time <= 0 || stopMove == false){
                                time= timeStop;
                                if(iPoint < Points.Length-1) iPoint++;
                                else iPoint= 0;
                            }else time -= Time.deltaTime;
                        }
                    }
                }
                break;
            case States.aggressive:
                if(chasing && aggressive){
                    Vector3 player= new Vector3(Player.position.x, transform.position.y, Player.position.x);
                    transform.LookAt(player);    
                    transform.position= Vector3.MoveTowards(transform.position, player, 
                                                            run * Time.deltaTime);
                
                }else state= States.passive;
                break;
            case States.dead:
                //falta hacer xd
                break;
            default:
                Debug.Log("Erron inesperado, comportarmiendo del enemigo");
                state= States.passive;
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color= Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangeVision);
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeChasing);   
    }
}
