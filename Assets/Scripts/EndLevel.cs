using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{

    [SerializeField]
    private string nextLevel;

    public GameObject blackOutSquare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            StartCoroutine(FadeBlackOutSquare());
            StartCoroutine(LoadLevelAfterDelay(1.5f));
        }
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 3) {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack) {
            //yield return new WaitForSeconds(1f);
            while (blackOutSquare.GetComponent<Image>().color.a < 1) {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent <Image>().color = objectColor;
                yield return null;
            }
        } else {
            while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent <Image>().color = objectColor;
                yield return null;
            }
        }
    }

    IEnumerator LoadLevelAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}
