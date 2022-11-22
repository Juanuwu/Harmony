using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Animator[] Enemys;
    [SerializeField] private Transform cnotasI;
    [SerializeField] private Transform cnotasD;
    [SerializeField] private float vel;
    private float n;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<Enemys.Length; i++){
            Enemys[i].SetInteger("v",Random.Range(0,3));
        }
    }

    // Update is called once per frame
    void Update()
    {
        n += vel * Time.deltaTime;
        cnotasI.localRotation = Quaternion.Euler(0,0,n);
        cnotasD.localRotation = Quaternion.Euler(0,0,-n);
        if(Input.GetKeyDown("space")) CambiarScena();
        if(Input.GetKeyDown(KeyCode.Escape)) Salir();
    }

    private void CambiarScena(){
        //Debug.Log("xD");
        SceneManager.LoadScene("SampleScene");
    }
    private void Salir(){
        //Debug.Log("a mimir");
        Application.Quit();
    }
}
