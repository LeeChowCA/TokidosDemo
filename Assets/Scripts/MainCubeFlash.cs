using UnityEngine;

public class MainCubeFlash : MonoBehaviour
{
    // use SerializeField to expose private variables in the inspector
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] float rotateSpeed = 10f; //degrees per second
    private Renderer cubeRenderer;
    private Color originalColor;

    private bool startToRotate = false;

    [SerializeField] SubCubeFlash[] subCubeFlashes;
    private Color[] subCubeOriginalColors;

    private bool isFlashing = false;

    void Awake()
    {
        cubeRenderer = GetComponent<Renderer>(); //get Renderer that'a attached to main cube
        
        originalColor = cubeRenderer.material.color; //store original color, so we can revert back to it later
        
        subCubeOriginalColors = new Color[subCubeFlashes.Length];
    }

    void Start()
    {
        for (int i = 0; i < subCubeFlashes.Length; i++)
        {
            subCubeOriginalColors[i] = subCubeFlashes[i].GetColor();
        } //store the original color of the sub-cube
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startToRotate = !startToRotate;
            if (startToRotate && !isFlashing)
            {
                StartCoroutine(FlashSubCubeColor());
                Debug.Log("Flashing SubCube");
            }
        }


        if (startToRotate)
        {
            RotateCube();
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(FlashColor());
            Debug.Log("Flashing");
        }
    }

    //set the color of the main cube
    public void SetColor(Color color) { 
        cubeRenderer.material.color = color;
    }

    //get the color of the main cube
    public Color GetColor() { 
        return cubeRenderer.material.color;
    }

    private void RotateCube()
    {
        transform.Rotate(new Vector3(0f,0f,rotateSpeed) * Time.deltaTime);
        Debug.Log("Rotating" + startToRotate);
    }

    private System.Collections.IEnumerator FlashSubCubeColor()
    {
        isFlashing = true;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < subCubeFlashes.Length; j++)
            {
                subCubeFlashes[j].SetColor(originalColor);
            }
            yield return new WaitForSeconds(flashDuration);

            for (int j = 0; j < subCubeFlashes.Length; j++)
            {
                subCubeFlashes[j].SetColor(subCubeOriginalColors[j]);
            }
            yield return new WaitForSeconds(flashDuration);
        }
        isFlashing = false;
    }



    private System.Collections.IEnumerator FlashColor()
    {
        for (int i = 0; i < 10; i++)
        {
            SetColor(Color.red);//change color to red
            yield return new WaitForSeconds(flashDuration); //wait for specified duration

            cubeRenderer.material.color = originalColor; //revert back to original color
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
