using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    private List<GameObject> passi = new List<GameObject>(); // Prefab dell'oggetto da posizionare
    private List<GameObject> infoPassi = new List<GameObject>();
    private bool piazzato = false; 
    private Pose pose; //Posizione del tocco dell'utente
    private int passo = 0;
    private GameObject passoCorrente;
    private GameObject infoPassoCorrente;
    private int temp;
    //UI
    private Button avanti,indietro,unicorno,cavalluccio,anatra,home,annulla, conferma,info;
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

        // Set up the "Unicorno" button
        unicorno = GameObject.FindGameObjectWithTag("Unicorno").GetComponent<Button>();
        unicorno.onClick.AddListener(Unicorno);

        // Set up the "Cavalluccio" button
        cavalluccio = GameObject.FindGameObjectWithTag("Cavalluccio").GetComponent<Button>();
        cavalluccio.onClick.AddListener(Cavalluccio);

        // Set up the "Anatra" button
        anatra = GameObject.FindGameObjectWithTag("Anatra").GetComponent<Button>();
        anatra.onClick.AddListener(Anatra);

        // Set up the "Home" button
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Button>();
        home.onClick.AddListener(ReturnToHome);

        // Set up the "conferma" button
        conferma = GameObject.FindGameObjectWithTag("Conferma").GetComponent<Button>();
        conferma.onClick.AddListener(Conferma);

        // Set up the "annulla" button
        annulla = GameObject.FindGameObjectWithTag("Annulla").GetComponent<Button>();
        annulla.onClick.AddListener(Annulla);

        // Set up the "info" button
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<Button>();
        info.onClick.AddListener(Info);
        info.gameObject.SetActive(false);

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
    /// Handles the AR application's game logic during the Update phase.
    /// This function processes user touch input, performs raycasting to detect planes, 
    /// and updates the UI and AR elements depending on the application's state and progress.
    /// </summary>
    void Update()
    {
        // Check if the object has not been placed yet
        if (piazzato == false)
        {
            // Check if there is at least one touch on the screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Perform a raycast when the touch begins
                if (touch.phase == TouchPhase.Began)
                {
                    // Check if the raycast hits a plane within a polygon
                    if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                    {
                        // Retrieve the pose (position and rotation) of the first detected plane
                        pose = hits[0].pose;

                        // Instantiate the current step's AR object at the detected position and rotation
                        passoCorrente = Instantiate(passi[passo], pose.position, pose.rotation);

                        // Mark the object as placed to avoid further raycasts
                        piazzato = true;

                        // Instantiate a shadow plane at the same position and rotation as the detected pose
                        shadowPlane = Instantiate(shadowPlane, new UnityEngine.Vector3(pose.position.x, pose.position.y, pose.position.z), pose.rotation);

                        // Instantiate the information object related to the current step slightly above the detected position
                        infoPassoCorrente = Instantiate(infoPassi[passo], new UnityEngine.Vector3(pose.position.x + 0.2f, pose.position.y + 0.3f, pose.position.z), pose.rotation);

                        // Initially set the information object to inactive
                        infoPassoCorrente.SetActive(false);

                        // Activate navigation and UI elements
                        avanti.gameObject.SetActive(true);
                        indietro.gameObject.SetActive(true);
                        info.gameObject.SetActive(true);
                        progressBar.gameObject.SetActive(true);

                        // Set the progress bar value based on the current step's progress
                        progressBar.value = (float)1 / passi.Count;

                        // Increment the step for the next instantiation
                        passo++;
                    }
                }
            }
        }
        else
        {
            // Update UI element interactability based on the current step
            if (passo == 0 || passo == passi.Count - 1)
            {
                avanti.interactable = false; // Disable forward navigation at the beginning or end
            }
            else
            {
                avanti.interactable = true; // Enable forward navigation otherwise
            }

            if (passo <= 1)
            {
                indietro.interactable = false; // Disable backward navigation at the first step
            }
            else
            {
                indietro.interactable = true; // Enable backward navigation otherwise
            }
        }
    }

    /// <summary>
    /// Handles the game logic for advancing to the next step in the AR construction process.
    /// </summary>
    /// <remarks>
    /// This function manages the transition from the current step to the next step in the sequence:
    /// - It checks if the current step object (`passoCorrente`) exists.
    /// - Saves the current object's position and rotation before destroying it.
    /// - Checks if there is a subsequent step in the list of prefabs (`passi`).
    /// - If a next step exists, increments the step counter (`passo`), instantiates the next step object
    ///   at the same position and rotation as the previous one, and updates the progress bar accordingly.
    /// </remarks>
    void Avanti()
    {
        // If there is a current step object, handle its cleanup
        if (passoCorrente != null)
        {
            // Save the position and rotation of the current step object
            pose.position = passoCorrente.transform.position;
            pose.rotation = passoCorrente.transform.rotation;

            // Destroy the current step object
            Destroy(passoCorrente);

            // Destroy the related information object
            Destroy(infoPassoCorrente);
        }

        // Check if there is a next step in the sequence
        if (passi[passo + 1] != null)
        {
            // Increment the step counter
            passo++;

            // Instantiate the next step object at the saved position and rotation
            passoCorrente = Instantiate(passi[passo], pose.position, pose.rotation);

            // Instantiate the information object for the next step
            infoPassoCorrente = Instantiate(infoPassi[passo], new UnityEngine.Vector3(
                pose.position.x + 0.2f, 
                pose.position.y + 0.3f, 
                pose.position.z), 
                pose.rotation);

            // Update the progress bar to reflect the new step
            progressBar.value += (float)1 / passi.Count;
        }
    }



    /// <summary>
    /// Handles the game logic for moving to the previous step in the AR construction process.
    /// </summary>
    /// <remarks>
    /// This function manages the transition from the current step to the previous step in the sequence:
    /// - It checks if the current step object (`passoCorrente`) exists.
    /// - Saves the current object's position and rotation before destroying it.
    /// - Checks if there is a previous step in the list of prefabs (`passi`).
    /// - If a previous step exists, decrements the step counter (`passo`), instantiates the previous step object
    ///   at the same position and rotation as the current one, and updates the progress bar accordingly.
    /// </remarks>
    void Indietro()
    {
        // Check if the current step object exists
        if (passoCorrente != null)
        {
            // Save the position and rotation of the current step object
            pose.position = passoCorrente.transform.position;
            pose.rotation = passoCorrente.transform.rotation;

            // Destroy the current step object
            Destroy(passoCorrente);

            // Destroy the related information object
            Destroy(infoPassoCorrente);
        }

        // Check if there is a previous step in the sequence
        if (passi[passo - 1] != null)
        {
            // Decrement the step counter
            passo--;

            // Instantiate the previous step object at the saved position and rotation
            passoCorrente = Instantiate(passi[passo], pose.position, pose.rotation);

            // Instantiate the information object for the previous step
            infoPassoCorrente = Instantiate(infoPassi[passo], new UnityEngine.Vector3(
                pose.position.x + 0.2f,
                pose.position.y + 0.3f,
                pose.position.z),
                pose.rotation);

            // Update the progress bar to reflect the new step
            progressBar.value -= (float)1 / passi.Count;
        }
    }

    
    
    /// <summary>
    /// This function loads all the "Unicorno Passi" game objects from the "Resources" folder,
    /// adds them to the "passi" list, disables the "StartPanel", and enables the "LoadingPanel".
    /// It then starts the "LoadingCoroutine" to simulate a loading process.
    /// </summary>
    void Unicorno()
    {
        // Load all "Unicorno Passi" game objects from the "Resources" folder
        GameObject[] passiUnicorno = Resources.LoadAll<GameObject>("Unicorno Passi");

        // Ordina l'array per nome
        passiUnicorno = passiUnicorno
            .OrderBy(prefab => ExtractNumber(prefab.name))
            .ToArray();

        // Add the loaded game objects to the "passi" list
        passi.AddRange(passiUnicorno);

        // Disable the "StartPanel" and enable the "LoadingPanel"
        startPanel.SetActive(false);
        loadingPanel.SetActive(true);

        // Start the "LoadingCoroutine" to simulate a loading process
        StartCoroutine(LoadingCoroutine());
    }

    
    
    /// <summary>
    /// Loads all "Cavalluccio Passi" and "Cavalluccio Info" game objects from the "Resources" folder.
    /// Adds the loaded objects to their respective lists, disables the "StartPanel," and enables the "LoadingPanel."
    /// Finally, initiates a coroutine to simulate the loading process.
    /// </summary>
    void Cavalluccio()
    {
        // Load all "Cavalluccio Passi" game objects from the "Resources" folder
        GameObject[] passiCavalluccio = Resources.LoadAll<GameObject>("Cavalluccio Passi");

        // Order the array by numeric value extracted from their names
        passiCavalluccio = passiCavalluccio
            .OrderBy(prefab => ExtractNumber(prefab.name))
            .ToArray();

        // Add the ordered "Cavalluccio Passi" game objects to the "passi" list
        passi.AddRange(passiCavalluccio);

        // Load all "Cavalluccio Info" game objects from the "Resources" folder
        GameObject[] infoPassiCavalluccio = Resources.LoadAll<GameObject>("Cavalluccio Info");

        // Order the array by numeric value extracted from their names
        infoPassiCavalluccio = infoPassiCavalluccio
            .OrderBy(prefab => ExtractNumber(prefab.name))
            .ToArray();

        // Add the ordered "Cavalluccio Info" game objects to the "infoPassi" list
        infoPassi.AddRange(infoPassiCavalluccio);

        // Disable the "StartPanel" to hide the starting UI
        startPanel.SetActive(false);

        // Enable the "LoadingPanel" to show a loading process
        loadingPanel.SetActive(true);

        // Start the loading coroutine to simulate the loading process
        StartCoroutine(LoadingCoroutine());
    }



    /// <summary>
    /// This function loads all the "Anatra Passi" game objects from the "Resources" folder,
    /// adds them to the "passi" list, disables the "StartPanel", and enables the "LoadingPanel".
    /// It then starts the "LoadingCoroutine" to simulate a loading process.
    /// </summary>
    void Anatra()
    {
        // Load all "Anatra Passi" game objects from the "Resources" folder
        GameObject[] passiAnatra = Resources.LoadAll<GameObject>("Anatra Passi");

        // Ordina l'array per nome
        passiAnatra = passiAnatra
            .OrderBy(prefab => ExtractNumber(prefab.name))
            .ToArray();

        // Add the loaded game objects to the "passi" list
        passi.AddRange(passiAnatra);

        // Disable the "StartPanel" and enable the "LoadingPanel"
        startPanel.SetActive(false);
        loadingPanel.SetActive(true);

        // Start the "LoadingCoroutine" to simulate a loading process
        StartCoroutine(LoadingCoroutine());
    }

     
    /// <summary>
    /// This function extracts the first number found in a given string using regular expressions.
    /// </summary>
    /// <param name="name">The string from which to extract the number.</param>
    /// <returns>The first number found in the string, or 0 if no number is found.</returns>
    private int ExtractNumber(string name)
    {
        // Use regular expressions to find the first number in the string
        Match match = Regex.Match(name, @"\d+");

        // If a number is found, parse it and return it
        // Otherwise, return 0
        return match.Success ? int.Parse(match.Value) : 0;
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

    /// <summary>
    /// Toggles the display of information related to the current step.
    /// </summary>
    /// <remarks>
    /// The function checks the current state of the <see cref="infoPassoCorrente"/> game object:
    /// - If the object is inactive, it activates it to display the associated information.
    /// - If the object is active, it deactivates it to hide the information.
    /// </remarks>
    void Info()
    {
        // Check if the current information object is inactive
        if (infoPassoCorrente.activeSelf == false)
        {
            // Activate the information object to display its contents
            infoPassoCorrente.SetActive(true);
        }
        else
        {
            // Deactivate the information object to hide its contents
            infoPassoCorrente.SetActive(false);
        }
    }

}

