using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject cubePrefab; // assign your Cube prefab in Inspector

    [Header("Spawn Settings")]
    public Vector3 mainCubePosition = new Vector3(0, 0, 0);
    public Vector3[] subCubePositions = {
        new Vector3(2, 0, 0),
        new Vector3(-2, 0, 0),
        new Vector3(0, 2, 0),
        new Vector3(0, -2, 0)
    };

    private void Start()
    {
        // 1. Spawn main cube
        GameObject mainCube = SpawnMainCube();

        // 2. Spawn sub cubes and link them to main cube
        SpawnSubCubes(mainCube.GetComponent<MainCubeFlash>());
    }

    private GameObject SpawnMainCube()
    {
        GameObject mainCube = Instantiate(cubePrefab, mainCubePosition, Quaternion.identity);

        // set tag
        mainCube.tag = "MainCube";

        // set color
        mainCube.GetComponent<Renderer>().material.color = Color.red;

        // attach MainCubeFlash script if not already present
        if (mainCube.GetComponent<MainCubeFlash>() == null)
            mainCube.AddComponent<MainCubeFlash>();

        return mainCube;
    }

    private void SpawnSubCubes(MainCubeFlash mainFlash)
    {
        // pick 4 different colors
        Color[] colors = { Color.blue, Color.green, Color.yellow, Color.magenta };

        for (int i = 0; i < subCubePositions.Length; i++)
        {
            GameObject subCube = Instantiate(cubePrefab, subCubePositions[i], Quaternion.identity);

            // set tag
            subCube.tag = "SubCube";

            // assign unique color
            subCube.GetComponent<Renderer>().material.color = colors[i];

            // attach SubCubeFlash script if not already present
            SubCubeFlash subFlash = subCube.GetComponent<SubCubeFlash>();
            if (subFlash == null)
                subFlash = subCube.AddComponent<SubCubeFlash>();

            // link to main cube flash script
            subFlash.GetType().GetField("mainCubeFlash",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(subFlash, mainFlash);
        }
    }
}
