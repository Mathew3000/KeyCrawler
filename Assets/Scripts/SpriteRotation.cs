using KeyCrawler;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    #region EditorSettings
    [Header("Rotation On/ Off")]
    public bool rotationSwitch = true;
    [Header("Rotation")]
    public float rotationSpeed = 0.1f;
    [Header("Flotation On/ Off")]
    public bool flotationSwitch = true;
    [Header("Flotation")]
    public float floatingSpeed = 1;
    public float floatingHeight = 1;
    #endregion

    #region PrivateVariables
    private float scaleValue = 0.0f;
    private float floatingValue = 0.0f;

    private Transform startTransformation;
    private Vector3 startPosition;
    private Vector3 startScale;
    private Player localPlayer;
    #endregion

    void Start()
    {
        startPosition = transform.localPosition;
        startScale = transform.localScale;
        localPlayer = FindObjectOfType<Player>();
        scaleValue = Random.Range(0, Mathf.PI*2);
        floatingValue = Random.Range(0, Mathf.PI);
    }


    void Update()
    {
        // Rotation
        if (rotationSwitch)
        {
            scaleValue = Mathf.MoveTowards(scaleValue, Mathf.PI * 2, rotationSpeed * Time.deltaTime);
            Vector3 newScale = new Vector3(Mathf.Sin(scaleValue), 1, 1);
            transform.localScale = new Vector3(
                startScale.x * newScale.x, 
                startScale.y * newScale.y, 
                startScale.z * newScale.z);

            if (scaleValue >= Mathf.PI * 2.0f)
            {
                scaleValue = 0.2f;
            }
        }

        // Flotation
        if (flotationSwitch)
        {
            Vector3 newPos = startPosition;
            floatingValue = Mathf.MoveTowards(floatingValue, Mathf.PI, floatingSpeed * Time.deltaTime);
            
            newPos.y = startPosition.y + Mathf.Sin(floatingValue) * floatingHeight;
            transform.localPosition = newPos;

            if (floatingValue >= Mathf.PI)
            {
                floatingValue = 0;
            }
        }

    }

    #region EventHandler
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            // If collision with player, then pickup animation
        }
    }
    #endregion
}
