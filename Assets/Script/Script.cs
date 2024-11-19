using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Script : MonoBehaviour
{
    public List<GameObject> passi = new List<GameObject>(); // Prefab dell'oggetto da posizionare
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool piazzato = false; 
    private Pose pose;
    private int passo = 0;

    private GameObject passoCorrente;
    private Button avanti;

    void Start()
    {
        // Ottieni il componente ARRaycastManager
        raycastManager = GetComponent<ARRaycastManager>();
        
        
        avanti = GameObject.FindGameObjectWithTag("Button").GetComponent<Button>();
        avanti.onClick.AddListener(Avanti);
        avanti.gameObject.SetActive(false);
    }

    void Update()
    {
        if(piazzato == false){
        // Controlla se l'utente ha toccato lo schermo
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Esegui un raycast solo quando l'utente tocca lo schermo
                if (touch.phase == TouchPhase.Began)
                {
                    // Lancia un raycast per trovare un piano
                    if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                    {
                        // Ottieni la posizione del primo piano rilevato
                        pose = hits[0].pose;

                        passoCorrente = Instantiate(passi[passo],pose.position,pose.rotation);
                        passo++; // Incrementa il passo per la prossima istanza di oggetto da posizionare
                        piazzato = true; // Imposta la variabile per evitare ulteriori raycasts

                        avanti.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void Avanti()
    {
        if (passoCorrente != null)
        {
            // Salva la posizione e la rotazione dell'oggetto corrente
            pose.position = passoCorrente.transform.position;
            pose.rotation = passoCorrente.transform.rotation;

            // Distruggi l'oggetto corrente
            Destroy(passoCorrente);
        }

        // Controlla se esiste un altro passo
        if (passi[passo+1] !=null){
            // Incrementa il passo e posiziona il nuovo oggetto nella stessa posizione
            passo++;
            passoCorrente = Instantiate(passi[passo], pose.position, pose.rotation);
        }
    }
}

