using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image upperImage;
    [SerializeField] private Image lowerImage;
    [SerializeField] private float fadingSpeed;
    [SerializeField] private bool fadeIn;

    public IEnumerator FadeNow()
    {
        this.gameObject.SetActive(true);
        if(fadeIn)
        {
            float startingFillAmount = 0.5f;
            float currentFillAmount = startingFillAmount;
            while(currentFillAmount > 0f)
            {
                currentFillAmount = Mathf.MoveTowards(currentFillAmount, 0f, fadingSpeed * Time.deltaTime);
                upperImage.fillAmount = currentFillAmount;
                lowerImage.fillAmount = currentFillAmount;
                yield return null;
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            float startingFillAmount = 0f;
            float currentFillAmount = startingFillAmount;
            while(currentFillAmount < 0.5f)
            {
                currentFillAmount = Mathf.MoveTowards(currentFillAmount, 0.5f, fadingSpeed * Time.deltaTime);
                upperImage.fillAmount = currentFillAmount;
                lowerImage.fillAmount = currentFillAmount;
                yield return null;
            }
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
    }
}
