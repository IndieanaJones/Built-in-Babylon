using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelTalker : MonoBehaviour
{
    public Canvas AngelTalkerCanvas;
    public Image AngelTalkerTextbox;
    public Text AngelTalkerText;
    public Image BlackScreenCover;

    public AudioClip[] VoiceSoundList;

    public bool CurrentlyTalking = false;
    public float TimeSinceLastLetter = -1;
    public float TalkSpeed = 0.25f;

    public void Start()
    {
        AngelTalkerCanvas.gameObject.SetActive(true);
        StartCoroutine(Line0());
    }

    public IEnumerator RemoveBlackscreen()
    {
        while (BlackScreenCover.color.a > 0)
        {
            Color tempColor = BlackScreenCover.color;
            tempColor.a -= 0.01f;
            BlackScreenCover.color = tempColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator OpenTextBox()
    {
        while(AngelTalkerTextbox.color.a < 1)
        {
            Color tempColor = AngelTalkerTextbox.color;
            tempColor.a += 0.02f;
            AngelTalkerTextbox.color = tempColor;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public IEnumerator CloseTextBox()
    {
        while (AngelTalkerTextbox.color.a > 0)
        {
            Color tempColor = AngelTalkerTextbox.color;
            tempColor.a -= 0.02f;
            Color tempColorText = AngelTalkerText.color;
            tempColorText.a -= 0.02f;
            AngelTalkerTextbox.color = tempColor;
            AngelTalkerText.color = tempColorText;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public IEnumerator SayLine(string line)
    {
        AngelTalkerText.enabled = true;
        AngelTalkerText.color = Color.black;
        AngelTalkerText.text = "";
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < line.Length; i++)
        {
            AngelTalkerText.text += line[i];
            yield return new WaitForSeconds(TalkSpeed);
        }
        yield return new WaitForSeconds(4f);
    }

    public IEnumerator Line0()
    {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(RemoveBlackscreen());
        yield return new WaitForSeconds(12f);
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("I fucking love science"));
        yield return StartCoroutine(SayLine("And I love YOU, player"));
        yield return StartCoroutine(SayLine("I also harbor a great love of oasises and a genuine pureblood hatred of towers."));
        yield return StartCoroutine(SayLine("9/11 (nine eleven) was genuinely deserved."));
        yield return StartCoroutine(CloseTextBox());
    }
}
