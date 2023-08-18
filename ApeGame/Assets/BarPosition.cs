using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarPosition : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5.0f; // Smoothing speed
    [SerializeField] public Vector3 scale;
    private bool opacity;
    public float fadeDuration = 2.0f; // Duration of the fade-out effect in seconds
    private CanvasGroup canvasGroup; // Reference to the CanvasGroup
    private float fadeStartTime; // Time when the fade-out effect started


    void FindPlayer() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        FindPlayer();
        canvasGroup = GetComponent<CanvasGroup>();
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) {
            FindPlayer();
        } else {
            Vector3 newPos = player.transform.position + Vector3.up * 10f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        if(GameObject.Find("PowerBar").GetComponent<MeshMaskScriptUI>().active == false) {
            if(!opacity)
                fadeStartTime = Time.time;
            // Calculate the elapsed time since the fade started
            float elapsed = Time.time - fadeStartTime;
            // Calculate the new alpha value
            float alpha = CalculateAlpha(elapsed);
            canvasGroup.alpha = alpha;
            opacity = true;
        } else if(opacity && GameObject.Find("PowerBar").GetComponent<MeshMaskScriptUI>().active) {
            canvasGroup.alpha = 1.0f;
            opacity = false;
        }
    }

    private float CalculateAlpha(float elapsed)
    {
        // Calculate the normalized time
        float normalizedTime = Mathf.Clamp01(elapsed / fadeDuration);
        // Calculate the alpha value based on the normalized time
        float alpha = 1f - normalizedTime;
        return alpha;
    }
}
