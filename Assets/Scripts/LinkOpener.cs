using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    public void GithubLink(string url)
    {
        Application.OpenURL("https://github.com/Wizard-Adi");
    }

    public void ItchLink(string url)
    {
        Application.OpenURL("https://aadityak.itch.io/");
    }
}
