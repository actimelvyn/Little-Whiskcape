using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDoor2 : MonoBehaviour
{
    public GameObject intText; // Texte d'interaction
    public bool interactable2; // Si le joueur peut interagir avec la porte
    public Animator doorAnim; // Animation de la porte (si utilisée)

    // Référence au script de la clé
    public Script_PickUP_Key scriptPickUpkey;

    private void Start()
    {
        // Désactiver le texte d'interaction au départ
        intText.SetActive(false);
        interactable2 = false;

        // Trouver automatiquement Script_PickUP_Key s'il n'est pas assigné
        if (scriptPickUpkey == null)
        {
            scriptPickUpkey = FindObjectOfType<Script_PickUP_Key>();
            if (scriptPickUpkey == null)
            {
                Debug.LogError("Script_PickUP_Key non trouvé dans la scène !");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans le trigger et possède la clé
        if (other.CompareTag("Player") && scriptPickUpkey != null && scriptPickUpkey.canOpenDoor)
        {
            intText.SetActive(true); // Affiche le texte d'interaction
            interactable2 = true;   // Permet l'interaction
            Debug.Log("Le joueur peut ouvrir la porte.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cache le texte lorsque le joueur quitte le trigger
        if (other.CompareTag("Player"))
        {
            intText.SetActive(false);
            interactable2 = false;
        }
    }

    private void Update()
    {
        // Vérifie l'interaction du joueur avec la porte
        if (interactable2 && Input.GetKeyUp(KeyCode.E) && scriptPickUpkey != null && scriptPickUpkey.canOpenDoor)
        {
            // Jouer l'animation si définie
            if (doorAnim != null)
            {
                doorAnim.SetTrigger("open");
            }

            // Désactiver la porte ou effectuer une autre action
            this.gameObject.SetActive(false);
            Destroy(intText); // Supprime le texte d'interaction
            Debug.Log("La porte s'ouvre !");
        }
    }
}
