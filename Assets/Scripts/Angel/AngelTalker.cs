using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelTalker : MonoBehaviour
{
    public static AngelTalker Instance;

    public Canvas AngelTalkerCanvas;

    public Image AngelTalkerTextbox;
    public Sprite HappyCloudSprite;
    public Sprite AngryCloudSprite;

    public Text AngelTalkerText;

    public Image BlackScreenCover;

    public Animator CloudAnimator;

    public AudioClip[] VoiceSoundList;

    public bool CurrentlyTalking = false;

    public AudioSource MusicTrack;
    public AudioSource AudioSourceComp;

    public void Start()
    {
        Instance = this;
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

    public IEnumerator SayLine(string line, float LingerTime = 3, float TalkSpeed = 0.05f, string textColor = "default")
    {
        AngelTalkerText.enabled = true;
        switch(textColor)
        {
            case "default":
                if (!ProgressionManager.IntroFinished)
                    AngelTalkerText.color = Color.black;
                else
                    AngelTalkerText.color = Color.white;
                break;
            case "red":
                AngelTalkerText.color = Color.red;
                break;
            default:
                AngelTalkerText.color = Color.black;
                break;
        }
        AngelTalkerText.text = "";
        yield return new WaitForSeconds(TalkSpeed);
        for (int i = 0; i < line.Length; i++)
        {
            AngelTalkerText.text += line[i];
            yield return new WaitForSeconds(TalkSpeed);
        }
        yield return new WaitForSeconds(LingerTime);
    }

    public void DoAngelLineTrigger(int lineToSay)
    {
        StopAllCoroutines();
        if(CurrentlyTalking)
            AngelTalkerText.text += "-";
        switch (lineToSay)
        {
            case 0:
                StartCoroutine(Line0());
                break;
            case 1:
                StartCoroutine(Line1());
                break;
            case 2:
                StartCoroutine(Line2());
                break;
            case 3:
                StartCoroutine(Line3());
                break;
            case 4:
                StartCoroutine(Line4());
                break;
            default:
                StartCoroutine(LineNonExistant());
                break;
        }
    }

    public IEnumerator DarkenSky(float speed)
    {
        Color startColor = Camera.main.backgroundColor;
        Color endColor = new Color(0.3f, 0.3f, 0.3f);
        float tick = 0f;
        while (Camera.main.backgroundColor != endColor)
        {
            tick += Time.deltaTime * speed;
            Camera.main.backgroundColor = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }
    }

    public IEnumerator Line0()
    {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(RemoveBlackscreen());
        CurrentlyTalking = true;
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("Ah, greetings mortal. I've been expecting you."));
        yield return StartCoroutine(SayLine("You must be the builder that the others spoke of."));
        yield return StartCoroutine(SayLine("Well, worry not. I know you have arrived in order to construct a luxurious temple to your 'Sun God'..."));
        yield return StartCoroutine(SayLine("But I have gone and constructed something far greater here instead."));
        yield return StartCoroutine(SayLine("Behold, a mystical desert oasis."));
        yield return StartCoroutine(SayLine("Equipped with imported melon trees and the finest(and only) drinking water one could find in this barren desert."));
        yield return StartCoroutine(SayLine("I invite you to join me in enjoying this great gift I have constructed."));
        yield return StartCoroutine(SayLine("And by join me, I mean you enjoy it while I watch."));
        yield return StartCoroutine(SayLine("This way, you can relax, and not have to do the manual labor for which you were sent."));
        yield return StartCoroutine(SayLine("Horrible manual labor, such as mining that rock by the road with your pickaxe to obtain building stones..."));
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }

    public IEnumerator Line1()
    {
        yield return new WaitForSeconds(1f);
        CurrentlyTalking = true;
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("Oh. I see you've decided to mine that rock for stones anyway."));
        yield return StartCoroutine(SayLine("Understandable. You did lug that pickaxe all the way out here. And that rock was an eyesore."));
        yield return StartCoroutine(SayLine("I sure do prefer a perfectly flat and empty desert with no stones in it myself."));
        yield return StartCoroutine(SayLine("So long as it contains a tried and true 'sight for sore eyes' oasis like the one we have here."));
        yield return StartCoroutine(SayLine("A gift that will continue to give, so long as nobody builds something over it."));
        yield return StartCoroutine(SayLine("Specifically, by deposting building rocks by the sign right by my oasis by pressing (E)..."));
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }

    public IEnumerator Line2()
    {
        yield return new WaitForSeconds(1f);
        CurrentlyTalking = true;
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("Ah, I uh... I see you've gone and moved those buildings stones to that sign I warned you about."));
        yield return StartCoroutine(SayLine("OK, fine. I get that nobody wants to carry 100 stones (your max capacity) around in their pockets."));
        yield return StartCoroutine(SayLine("But you could've dumped them off somewhere else instead of that sign I told you not to interact with."));
        yield return StartCoroutine(SayLine("Alright, no big deal I guess. After all, idle stones cause no harm."));
        yield return StartCoroutine(SayLine("The REAL harm would happen if someone were to hold (E) on that sign now fully loaded with rocks."));
        yield return StartCoroutine(SayLine("That would, of course, complete the building process, which could lead to undesired consequences."));
        yield return StartCoroutine(SayLine("My wings shudder to think of it."));
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }

    public IEnumerator Line3()
    {
        yield return new WaitForSeconds(1f);
        CurrentlyTalking = true;
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("OK wait a second", 1));
        yield return StartCoroutine(SayLine("That is something you DON'T want to do"));
        yield return StartCoroutine(SayLine("Have you even been listening to me this whole time?"));
        yield return StartCoroutine(SayLine("We have a nice thing going here, why are you so dead-set on ruining this?"));
        yield return StartCoroutine(SayLine("I swear, if you finish building that structure..."));
        yield return StartCoroutine(SayLine("You won't live that long to regret your decisions.", 5, 0.1f, "red"));
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }

    public IEnumerator Line4()
    {
        yield return new WaitForSeconds(1f);
        CurrentlyTalking = true;
        AngelTalkerTextbox.sprite = AngryCloudSprite;
        ProgressionManager.IntroFinished = true;
        yield return StartCoroutine(OpenTextBox());
        StartCoroutine(DarkenSky(0.5f));
        yield return StartCoroutine(SayLine("...", 5, 1f));
        yield return StartCoroutine(SayLine("What have you done?"));
        yield return StartCoroutine(SayLine("My oasis, my beautiful oasis..."));
        CloudAnimator.Play("Move In");
        yield return StartCoroutine(SayLine("Gone..."));
        MusicTrack.Play();
        yield return StartCoroutine(SayLine("YOU WILL PAY FOR WHAT YOU HAVE DONE", 3, 0.1f, "red"));
        AngelEventManager.Instance.AddEvent("lightning");
        AngelEventManager.Instance.TimeForNextEvent = Time.time + 5f;
        AngelEventManager.Instance.PauseEventTimer = false;
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }

    public IEnumerator Line5()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("What hideous walls you've built."));
        yield return StartCoroutine(SayLine("Probably structurally unsound, too."));
        yield return StartCoroutine(SayLine("But you don't seem to care for my words, do you?"));
        yield return StartCoroutine(SayLine("Perhaps we should get some of your peers to review your work instead..."));
        AngelEventManager.Instance.AddEvent("zombies");
        CurrentlyTalking = false;
    }

    public IEnumerator LineNonExistant()
    {
        CurrentlyTalking = true;
        yield return StartCoroutine(OpenTextBox());
        yield return StartCoroutine(SayLine("The line I was requested to say here doesn't exist."));
        yield return StartCoroutine(SayLine("That's a bit of a lark... Hopefully you're not actually playing this and see this one."));
        yield return StartCoroutine(CloseTextBox());
        CurrentlyTalking = false;
    }
}
