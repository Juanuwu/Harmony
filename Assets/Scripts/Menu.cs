using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject creditos;

    // Start is called before the first frame update
    void Start()
    {
        offCredit();  
    }

    // Update is called once per frame
    //void Update(){}

    public void onCredit(){
        creditos.SetActive(true);
    }
    public void offCredit(){
        creditos.SetActive(false);
    }
    public void quitGame(){
        Application.Quit();
    }
}
