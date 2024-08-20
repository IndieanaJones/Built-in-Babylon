using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image BlackScreenImage;
    public AudioSource AudioSourceComp;

    public GameObject TopText1;
    public GameObject ImageObject1;
    public GameObject BottomText1;

    public GameObject TopText2;
    public GameObject ImageObject2;
    public GameObject BottomText2;

    public GameObject TopText3;
    public GameObject ImageObject3;
    public GameObject BottomText3;

    public IEnumerator DoBlackscreen()
    {
        while (BlackScreenImage.color.a < 1)
        {
            Color tempColor = BlackScreenImage.color;
            tempColor.a += 0.01f;
            BlackScreenImage.color = tempColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator RemoveBlackscreen()
    {
        while (BlackScreenImage.color.a > 0)
        {
            Color tempColor = BlackScreenImage.color;
            tempColor.a -= 0.01f;
            BlackScreenImage.color = tempColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Start()
    {
        StartCoroutine(DoIntro());
    }

    public IEnumerator DoIntro()
    {
        yield return new WaitForSeconds(0.75f);
        TopText1.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        ImageObject1.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        BottomText1.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(3f);
        TopText1.SetActive(false);
        ImageObject1.SetActive(false);
        BottomText1.SetActive(false);
        yield return new WaitForSeconds(0.75f);

        TopText3.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        ImageObject3.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        BottomText3.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(3f);
        TopText3.SetActive(false);
        ImageObject3.SetActive(false);
        BottomText3.SetActive(false);
        yield return new WaitForSeconds(0.75f);

        TopText2.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        ImageObject2.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(0.75f);
        BottomText2.SetActive(true);
        AudioSourceComp.Play();
        yield return new WaitForSeconds(2f);
        TopText2.SetActive(false);
        ImageObject2.SetActive(false);
        BottomText2.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("MainMenu");
    }
}
