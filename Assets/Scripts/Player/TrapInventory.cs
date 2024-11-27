using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInventory : MonoBehaviour
{
    [System.Serializable]
    public class TrapSlot
    {
        public GameObject trapPrefab; // Prefab de la trampa
        public GameObject previewPrefab; // Prefab de previsualización
        public int quantity;          // Cantidad disponible
    }

    public TrapSlot[] traps; 
    private int selectedTrapIndex = 0; 

    void Update()
    {
       
        for (int i = 0; i < traps.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) 
            {
                selectedTrapIndex = i;
                Debug.Log($"Trampa seleccionada: {traps[selectedTrapIndex].trapPrefab.name}");
            }
        }
    }

    public GameObject GetSelectedTrap()
    {
        if (traps[selectedTrapIndex].quantity > 0)
        {
            return traps[selectedTrapIndex].trapPrefab;
        }
        else
        {
            Debug.Log("No hay más trampas de este tipo.");
            return null;
        }
    }

    public void UseTrap()
    {
        if (traps[selectedTrapIndex].quantity > 0)
        {
            traps[selectedTrapIndex].quantity--;
        }
    }
}
