using UnityEngine;

public class SubCubeFlash : MonoBehaviour
{
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] MainCubeFlash mainCubeFlash;
    private Color mainCubeOriginalColor;

    private Color subCubeOriginalColor;
    private Renderer subCubeRenderer;

    private Renderer cubeRenderer;

    private void Awake()
    {
        subCubeRenderer = GetComponent<Renderer>();
        Debug.Assert(subCubeRenderer != null, "Renderer component not found on SubCubeFlash GameObject.");
        //cubeRenderer = GetComponent<Renderer>(); //get Renderer that'a attached to main cube
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subCubeOriginalColor = subCubeRenderer.material.color;
        mainCubeOriginalColor = mainCubeFlash.GetColor(); // store the main cube's original color
    }

    private void OnMouseDown()
    {
        //for left mouse button 
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FlashMainCubeColor());
        }
    }

    //set the color of the main cube
    public void SetColor(Color color)
    {
        subCubeRenderer.material.color = color;
    }

    //get the color of the main cube
    public Color GetColor()
    {
        return subCubeRenderer.material.color;
    }

    private System.Collections.IEnumerator FlashMainCubeColor()
    {
        if (mainCubeFlash != null)
        {
            for (int i = 0; i < 10; i++) {
                mainCubeFlash.SetColor(subCubeOriginalColor); // flash the main cube to the sub-cube's original color
                yield return new WaitForSeconds(flashDuration);

                mainCubeFlash.SetColor(mainCubeOriginalColor); // revert back to original color
                yield return new WaitForSeconds(flashDuration);
            }
        }
        else
        {
            Debug.LogWarning("MainCubeFlash reference is not set.");
        }
    }
}
