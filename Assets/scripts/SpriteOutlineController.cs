using UnityEngine;

public class SpriteOutlineController : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.Q;
    public GameObject targetObject;
    public float outlineThickness = 2f;
    public float outlineDuration = 0.3f;

    private Material spriteMat;

    void Start()
    {
        spriteMat = targetObject.GetComponent<SpriteRenderer>().material;
        spriteMat.SetFloat("_OutlineThickness", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            spriteMat.SetFloat("_OutlineThickness", outlineThickness);
            CancelInvoke(nameof(HideOutline));
            Invoke(nameof(HideOutline), outlineDuration);
        }
    }

    void HideOutline()
    {
        spriteMat.SetFloat("_OutlineThickness", 0);
    }
}
