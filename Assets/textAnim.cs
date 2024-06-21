using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAnim : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public float fadeDuration = 0.5f; // Duration of the fade in/out
    public float displayDuration = 0.5f; // Duration to display the text before fading out

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Start the continuous fade in and out coroutine
        StartCoroutine(FadeInAndOutLoop());
    }

    private void Update() {
        if(Input.anyKey){
            SceneManager.LoadScene("GameScene");
        }
    }

    private IEnumerator FadeInAndOutLoop()
    {
        while (true)
        {
            // Ensure the text is fully transparent initially
            SetTextAlpha(0f);

            // Fade in
            yield return FadeTextToAlpha(1f);

            // Wait for the display duration
            yield return new WaitForSeconds(displayDuration);

            // Fade out
            yield return FadeTextToAlpha(0f);

            // Wait for the display duration before fading in again
            yield return new WaitForSeconds(displayDuration);
        }
    }

    private IEnumerator FadeTextToAlpha(float targetAlpha)
    {
        Color originalColor = textMeshPro.color;
        float startAlpha = textMeshPro.color.a;
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            SetTextAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the target alpha is set at the end
        SetTextAlpha(targetAlpha);
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = textMeshPro.color;
        color.a = alpha;
        textMeshPro.color = color;
    }
}

