using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public Image fader;
    private static SceneControl instance;

    private GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static void TransitionPlayer(Vector3 pos)
    {
        instance.StartCoroutine(instance.Transition(pos));
    }

    private IEnumerator Transition(Vector3 pos)
    {
        fader.gameObject.SetActive(true);

        for (float i = 0; i < 1; i += Time.deltaTime / 0.25f)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, i));
            yield return null;
        }

        player.transform.position = pos;

        yield return new WaitForSeconds(.5f);

        for (float i = 0; i < 1; i += Time.deltaTime / 0.25f)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, i));
            yield return null;
        }

        fader.gameObject.SetActive(false);
    }
}
