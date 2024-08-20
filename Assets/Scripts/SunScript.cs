using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    public static float EndTime;
    public bool StartMoving = false;
    public GameObject BackLayerObj;
    public GameObject MidLayerObj;

    public void FixedUpdate()
    {
        if (!StartMoving)
            return;
        BackLayerObj.transform.localRotation = Quaternion.Euler(BackLayerObj.transform.localRotation.x, BackLayerObj.transform.localRotation.y +  5, BackLayerObj.transform.localRotation.z);
        MidLayerObj.transform.localRotation = Quaternion.Euler(MidLayerObj.transform.localRotation.x, MidLayerObj.transform.localRotation.y - 5, MidLayerObj.transform.localRotation.z);
    }

    public void EndGame()
    {
        EndTime = Time.time - AngelTalker.Instance.TimeGameStarted;
        StartMoving = true;
        StartCoroutine(SunRiseUp(0.05f));
    }

    public IEnumerator SunRiseUp(float speed)
    {
        Color startColor = Camera.main.backgroundColor;
        Color endColor = new Color(1.0f, 0.73f, 0.0f);
        float tick = 0f;
        while (Camera.main.backgroundColor != endColor)
        {
            tick += Time.deltaTime * speed;
            Camera.main.backgroundColor = Color.Lerp(startColor, endColor, tick);
            gameObject.transform.position = Vector3.Lerp(new Vector3(0, -200, 875), new Vector3(0, 400, 875), tick);
            yield return null;
        }
        AngelTalker.Instance.CallWinGame();
    }
}
