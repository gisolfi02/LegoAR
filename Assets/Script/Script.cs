using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class Script : MonoBehaviour
{

    //ShadowsPlane
    public GameObject shadowPlane; //Prefab del piano utilizzato per l'ombra

    //AR Componentes
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARPlaneManager planeManager;
    
    //Costruzione
    public List<GameObject> passi = new List<GameObject>(); // Prefab dell'oggetto da posizionare
    private bool piazzato = false; 
    private Pose pose; //Posizione del tocco dell'utente
    private int passo = 0;
    private GameObject passoCorrente;
    private int temp;
    //UI
    private Button avanti,indietro,start,home,annulla, conferma;
    private GameObject startPanel, constructionPanel,loadingPanel, confermaPanel;
    private Slider progressBar;

    /// <summary>
    /// This function initializes the AR application and sets up the user interface.
    /// </summary>
    void Awake()
    {
        // Get the ARRaycastManager component
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        // Set up the "Avanti" button
        avanti = GameObject.FindGameObjectWithTag("Avanti").GetComponent<Button>();
        avanti.onClick.AddListener(Avanti);
        avanti.gameObject.SetActive(false);

        // Set up the "Indietro" button
        indietro = GameObject.FindGameObjectWithTag("Indietro").GetComponent<Button>();
        indietro.onClick.AddListener(Indietro);
        indietro.gameObject.SetActive(false);

        // Set up the "Start" button
        start = GameObject.FindGameObjectWithTag("Start").GetComponent<Button>();
        start.onClick.AddListener(StartDetection);

        // Set up the "Home" button
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Button>();
        home.onClick.AddListener(ReturnToHome);

        // Set up the "conferma" button
        conferma = GameObject.FindGameObjectWithTag("Conferma").GetComponent<Button>();
        conferma.onClick.AddListener(Conferma);

        // Set up the "annulla" button
        annulla = GameObject.FindGameObjectWithTag("Annulla").GetComponent<Button>();
        annulla.onClick.AddListener(Annulla);

        // Set up the "progressBar" slider
        progressBar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Slider>();
        progressBar.value = 0f;
        progressBar.gameObject.SetActive(false);

        // Set up the "StartPanel" game object
        startPanel = GameObject.FindGameObjectWithTag("StartPanel");
        startPanel.SetActive(true);

        // Set up the "ConstructionPanel" game object
        constructionPanel = GameObject.FindGameObjectWithTag("ConstructionPanel");
        constructionPanel.SetActive(false);

        // Set up the "LoadingPanel" game object
        loadingPanel = GameObject.FindGameObjectWithTag("LoadingPanel");
        loadingPanel.SetActive(false);

        // Set up the "ConfermaPanel" game object
        confermaPanel = GameObject.FindGameObjectWithTag("ConfermaPanel");
        confermaPanel.SetActive(false);

        // Disable AR plane detection
        planeManager.enabled = false;
    }


    /// <summary>
    /// This function is responsible for handling the game logic during the AR application's update phase.
    /// It checks for user touch input, performs raycasting to detect planes, and updates the UI elements based on the current step.
    /// </summary>
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
                        shadowPlane = Instantiate(shadowPlane, new UnityEngine.Vector3(pose.position.x,pose.position.y,pose.position.z), pose.rotation);
                        avanti.gameObject.SetActive(true);
                        indietro.gameObject.SetActive(true);
                        progressBar.gameObject.SetActive(true);
                        progressBar.value = (float)1/passi.Count;
                    }
                }
            }
        }
        else{
            if(passo == 0 || passo == 58){
                avanti.interactable=false;
            }else{
                avanti.interactable=true;
            }
            if (passo <= 1)
            {
                indietro.interactable = false;
            } else{
                indietro.interactable = true;
            }
        }
    }

    /// <summary>
    /// This function handles the logic for moving to the next step in the construction process.
    /// </summary>
    /// <remarks>
    /// The function checks if the current step object exists, saves its position and rotation, and then destroys it.
    /// It then checks if there is a next step in the list of prefabs. If so, it increments the step counter,
    /// and instantiates the next step object at the same position as the previous one.
    /// </remarks>
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
            progressBar.value += (float)1/passi.Count;
        }
    }

    /// <summary>
    /// This function handles the logic for moving to the previous step in the construction process.
    /// </summary>
    /// <remarks>
    /// The function checks if the current step object exists, saves its position and rotation, and then destroys it.
    /// It then checks if there is a previous step in the list of prefabs. If so, it decrements the step counter,
    /// and instantiates the previous step object at the same position as the current one.
    /// </remarks>
    void Indietro()
    {
        // Check if the current step object exists
        if (passoCorrente != null)
        {
            // Save the position and rotation of the current object
            pose.position = passoCorrente.transform.position;
            pose.rotation = passoCorrente.transform.rotation;

            // Destroy the current object
            Destroy(passoCorrente);
        }

        // Check if there is a previous step
        if (passi[passo-1] !=null){
            // Decrement the step counter and instantiate the previous step object at the same position
            passo--;
            passoCorrente = Instantiate(passi[passo], pose.position, pose.rotation);
            progressBar.value -= (float)1/passi.Count;
        }
    }
    
    /// <summary>
    /// This function is responsible for starting the AR plane detection and loading process.
    /// </summary>
    /// <remarks>
    /// The function disables the "StartPanel" and enables the "LoadingPanel". It then starts the "LoadingCoroutine"
    /// to simulate a loading process. After the loading process is completed, it enables AR plane detection,
    /// and shows the "ConstructionPanel".
    /// </remarks>
    void StartDetection()
    {
        // Disable the "StartPanel" and enable the "LoadingPanel"
        startPanel.SetActive(false);
        loadingPanel.SetActive(true);

        // Start the "LoadingCoroutine" to simulate a loading process
        StartCoroutine(LoadingCoroutine());
    }

    /// <summary>
    /// This coroutine simulates a loading process by waiting for a specified duration.
    /// After the loading process is completed, it disables the "LoadingPanel", enables AR plane detection,
    /// and shows the "ConstructionPanel".
    /// </summary>
    /// <returns>
    /// An IEnumerator that waits for the specified duration before completing.
    /// </returns>
    private IEnumerator LoadingCoroutine()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Disable the loading panel
        loadingPanel.SetActive(false);

        // Enable AR plane detection
        planeManager.enabled = true;

        // Show the construction panel
        constructionPanel.SetActive(true);
    }

    /// <summary>
    /// This function handles the user's confirmation action.
    /// It disables the "Home", "Avanti", and "Indietro" buttons,
    /// stores the current step in a temporary variable, resets the step counter,
    /// and activates the "ConfermaPanel".
    /// </summary>
    void ReturnToHome()
    {
        // Disable the "Home", "Avanti", and "Indietro" buttons
        home.interactable = false;
        avanti.interactable = false;
        indietro.interactable = false;

        // Store the current step in a temporary variable
        temp = passo;

        // Reset the step counter
        passo = 0;

        // Activate the "ConfermaPanel"
        confermaPanel.SetActive(true);
    }


    /// <summary>
    /// This function is responsible for returning the user to the home scene.
    /// It sets the target scene name in PlayerPrefs and then loads the loading scene.
    /// </summary>
    /// <remarks>
    /// This function is called when the "Home" button is clicked. It ensures that the user is returned to the home scene
    /// by setting the target scene name in PlayerPrefs and then loading the loading scene.
    /// </remarks>
    void Conferma()
    {
        // Set the target scene name in PlayerPrefs
        PlayerPrefs.SetString("TargetScene", "SampleScene");

        // Load the loading scene
        SceneManager.LoadScene("LoadScene");   
    }


    /// <summary>
    /// This function handles the user's cancellation action.
    /// It disables the "Home", "Avanti", and "Indietro" buttons,
    /// restores the step counter to its previous value,
    /// and deactivates the "ConfermaPanel".
    /// </summary>
    void Annulla()
    {
        // Enable the "Home", "Avanti", and "Indietro" buttons
        home.interactable = true;
        avanti.interactable = true;
        indietro.interactable = true;

        // Restore the step counter to its previous value
        passo = temp;

        // Reset the temporary variable
        temp = 0;

        // Deactivate the "ConfermaPanel"
        confermaPanel.SetActive(false);
    }

    
}

