using UnityEngine;

public class MainCubeFlash : MonoBehaviour
{
    // use SerializeField to expose private variables in the inspector
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] float rotateSpeed = 10f; //degrees per second
    [SerializeField] SubCubeFlash[] subCubeFlashes;
    [SerializeField] int flashingTime = 12; // total time to flash sub-cubes

    private Renderer mainCubeRenderer;
    private Color mainCubeOriginalColor;

    private bool startToRotate = false;
    
    private Color[] subCubeOriginalColors;

    public bool IsFlashing { get; private set; } // use isFlashing to prevent multiple simultaneous flashes

    void Awake()
    {
        mainCubeRenderer = GetComponent<Renderer>(); //get Renderer that'a attached to main cube
        mainCubeOriginalColor = mainCubeRenderer.material.color; //store original color, so we can revert back to it later
   
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    startToRotate = !startToRotate;
                    if (startToRotate && !IsFlashing)
                    {
                        StartCoroutine(FlashSubCubeColor());
                        Debug.Log("Flashing SubCube");
                    }
                }
            }
        }

        if (startToRotate)
        {
            RotateCube();
        }
    }

    // Detect mouse click on the cube
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(FlashMainCubeColor());
            Debug.Log("Flashing");
        }
    }

    //set the color of the main cube
    public void SetColor(Color color) { 
        mainCubeRenderer.material.color = color;
    }

    //get the color of the main cube
    public Color GetColor() { 
        return mainCubeRenderer.material.color;
    }

    private void RotateCube()
    {
        transform.Rotate(new Vector3(0f,0f,rotateSpeed) * Time.deltaTime);
    }

    private System.Collections.IEnumerator FlashSubCubeColor()
    {
        IsFlashing = true;
        for (int i = 0; i < flashingTime; i++)
        {
            for (int j = 0; j < subCubeFlashes.Length; j++)
            {
                subCubeFlashes[j].SetColor(mainCubeOriginalColor);
            }
            yield return new WaitForSeconds(flashDuration);

            for (int j = 0; j < subCubeFlashes.Length; j++)
            {
                subCubeFlashes[j].SetColor(subCubeOriginalColors[j]);
            }
            yield return new WaitForSeconds(flashDuration);
        }
        IsFlashing = false;
    }

    private System.Collections.IEnumerator FlashMainCubeColor()
    {
        for (int i = 0; i < flashingTime; i++)
        {
            SetColor(Color.red);//change color to red
            yield return new WaitForSeconds(flashDuration); //wait for specified duration

            mainCubeRenderer.material.color = mainCubeOriginalColor; //revert back to original color
            yield return new WaitForSeconds(flashDuration);
        }
    }

    public void FlashMainCubeToColor(Color targetColor, float dutation) 
    { 
        if (!IsFlashing) 
        { 
            StartCoroutine(FlashMainCubeToColorCoroutine(targetColor, dutation));
        }
    }

    private System.Collections.IEnumerator FlashMainCubeToColorCoroutine(Color targetColor, float duration)
    {
        IsFlashing = true;

        for (int i = 0; i < flashingTime; i++)
        {
            mainCubeRenderer.material.color = targetColor;
            yield return new WaitForSeconds(duration);
            mainCubeRenderer.material.color = mainCubeOriginalColor;
            yield return new WaitForSeconds(duration);
        }
        
        IsFlashing = false;
    }
}
