using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    public Camera playerCamera; // C�mara del jugador
    public LayerMask placementLayer; // Capa 
    public float rotationSpeed = 45f; // Velocidad de rotaci�n 

    private TrapInventory inventory;
    private GameObject currentPreview; 
    private Quaternion currentRotation = Quaternion.identity; 

    void Start()
    {
        inventory = GetComponent<TrapInventory>();
    }

    void Update()
    {
        RotatePreview(); 
        PlaceTrapPreview();

        if (Input.GetMouseButtonDown(0)) // Bot�n izquierdo del rat�n
        {
            PlaceTrap();
        }
    }

    void RotatePreview()
    {
        // Ajustar la rotaci�n con Q y E o la rueda del rat�n
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rotationInput = -rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) rotationInput = rotationSpeed * Time.deltaTime;

        // Alternativa: Usar la rueda del rat�n para rotar
        rotationInput += Input.mouseScrollDelta.y * rotationSpeed;

        if (rotationInput != 0)
        {
            currentRotation *= Quaternion.Euler(0, 0, rotationInput); 
            if (currentPreview != null)
            {
                currentPreview.transform.rotation = currentRotation;
            }
        }
    }

    void PlaceTrapPreview()
    {
        
        GameObject selectedTrap = inventory.GetSelectedTrap();
        GameObject previewPrefab = GetCurrentPreviewPrefab();

      
        if (selectedTrap != null && previewPrefab != null)
        {
            
            if (currentPreview == null || currentPreview.name != previewPrefab.name)
            {
                if (currentPreview != null)
                    Destroy(currentPreview);

                currentPreview = Instantiate(previewPrefab);
                currentPreview.name = previewPrefab.name; 
                currentPreview.transform.rotation = currentRotation; 
            }


            Vector2 worldPos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            float trapRadius = 0.2f;
            if (!Physics2D.OverlapCircle(worldPos, trapRadius))
            {
                currentPreview.SetActive(true);
                currentPreview.transform.position = worldPos;
            }
            else
            {
                currentPreview.SetActive(false);
            }
        }
        else if (currentPreview != null)
        {
            Destroy(currentPreview); 
        }
    }

    void PlaceTrap()
    {
        GameObject selectedTrap = inventory.GetSelectedTrap();
        if (selectedTrap != null)
        {
            Vector2 worldPos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            float trapRadius = 0.2f;
            if (!Physics2D.OverlapCircle(worldPos, trapRadius)) 
            {
                Instantiate(selectedTrap, worldPos, currentRotation);
                inventory.UseTrap();
                Debug.Log("Trampa colocada.");
            }
            else
            {
                Debug.Log("No se puede colocar la trampa aqu�.");
            }
        }
    }

    GameObject GetCurrentPreviewPrefab()
    {
        int selectedTrapIndex = GetSelectedTrapIndex();
        if (selectedTrapIndex >= 0 && selectedTrapIndex < inventory.traps.Length)
        {
            GameObject preview = inventory.traps[selectedTrapIndex].previewPrefab;
            if (preview == null)
            {
                Debug.LogError($"El prefab de previsualizaci�n para la trampa en el �ndice {selectedTrapIndex} no est� asignado.");
            }
            return preview;
        }
        return null;
    }

    int GetSelectedTrapIndex()
    {
        GameObject selectedTrap = inventory.GetSelectedTrap();
        if (selectedTrap != null)
        {
            for (int i = 0; i < inventory.traps.Length; i++)
            {
                if (inventory.traps[i].trapPrefab == selectedTrap)
                {
                    return i;
                }
            }
        }
        return -1; 
    }
}
