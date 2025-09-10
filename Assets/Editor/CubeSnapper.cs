using UnityEngine;
using UnityEditor;

public class CubeSnapper : EditorWindow
{
    private Vector3 mainCubeOriginalPosition;
    private Vector3[] subCubeOriginalPositions;

    private Color[] subCubeColors;

    [MenuItem("Tools/CubeSnapper")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CubeSnapper));
    }

    private void OnGUI()
    {
        GUILayout.Label("Snap Main and Sub Cubes!", EditorStyles.boldLabel);

        if (GUILayout.Button("Snap Cubes"))
        {
            SnapCubes();
            SetToMainCubeColor();
        }

        if (GUILayout.Button("UnSnap Cubes"))
        {
            UnSnapCubes();
            UnSetMainCubeColor();
        }
    }

    private void SnapCubes()
    {
        GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
        GameObject[] subCubes = GameObject.FindGameObjectsWithTag("SubCube");

        if (mainCube == null || subCubes.Length == 0)
        {
            Debug.LogWarning("MainCube or SubCubes not found in the scene.");
            return;
        }

        // Store original positions
        mainCubeOriginalPosition = mainCube.transform.position;
        subCubeOriginalPositions = new Vector3[subCubes.Length];
        for (int i = 0; i < subCubes.Length; i++)
        {
            subCubeOriginalPositions[i] = subCubes[i].transform.position;
        }

        // Snap main cube to origin
        mainCube.transform.position = Vector3.zero;

        // Snap sub-cubes in a row next to main cube
        for (int i = 0; i < subCubes.Length; i++)
        {
            subCubes[i].transform.position = new Vector3(i + 1, 0, 0); // e.g., (1,0,0), (2,0,0), ...
        }
    }

    private void UnSnapCubes()
    {
        GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
        GameObject[] subCubes = GameObject.FindGameObjectsWithTag("SubCube");

        if (mainCube == null || subCubes.Length == 0 || subCubeOriginalPositions == null)
        {
            Debug.LogWarning("Cannot unsnap: missing references or original positions.");
            return;
        }

        // Restore main cube position
        mainCube.transform.position = mainCubeOriginalPosition;

        // Restore sub-cube positions
        for (int i = 0; i < subCubes.Length && i < subCubeOriginalPositions.Length; i++)
        {
            subCubes[i].transform.position = subCubeOriginalPositions[i];
        }

        // Clear stored positions
        subCubeOriginalPositions = null;
    }

    private void SetToMainCubeColor()
    {
        GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
        GameObject[] subCubes = GameObject.FindGameObjectsWithTag("SubCube");

        if (mainCube == null || subCubes.Length == 0)
        {
            Debug.LogWarning("MainCube or SubCubes not found in the scene.");
            return;
        }

        Color mainCubeColor = mainCube.GetComponent<Renderer>().material.color;

        // Store original colors of sub-cubes
        subCubeColors = new Color[subCubes.Length];
        for (int i = 0; i < subCubes.Length; i++)
        {
            subCubeColors[i] = subCubes[i].GetComponent<Renderer>().material.color;
        }


        foreach (GameObject subCube in subCubes)
        {
            subCube.GetComponent<Renderer>().material.color = mainCubeColor;
        }
    }

    private void UnSetMainCubeColor()
    { 
        GameObject[] subCubes = GameObject.FindGameObjectsWithTag("SubCube");
        if (subCubes.Length == 0 || subCubeColors == null)
        {
            Debug.LogWarning("SubCubes not found or original colors not stored.");
            return;
        }
        for (int i = 0; i < subCubes.Length && i < subCubeColors.Length; i++)
        {
            subCubes[i].GetComponent<Renderer>().material.color = subCubeColors[i];
        }
        // Clear stored colors
        subCubeColors = null;
    }

    // This function will disalbe mouse click on the cubes when snapped
    //so that user cannot change color or rotate the cubes
    private void DisableMouseClick()
    {
        GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
        GameObject[] subCubes = GameObject.FindGameObjectsWithTag("SubCube");

        if (mainCube == null || subCubes.Length == 0)
        {
            Debug.LogWarning("MainCube or SubCubes not found in the scene.");
            return;
        }
        mainCube.GetComponent<Collider>().enabled = false;
        foreach (GameObject subCube in subCubes)
        {
            subCube.GetComponent<Collider>().enabled = false;
        }
    }
}
