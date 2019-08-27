using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FontChanger : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProUGUIs;
    public TextMeshProUGUI[] textMeshProsWhite;
    public TextMeshProUGUI[] textMeshProsGrey;
    public TMP_FontAsset asset;
    public Material grey;
    public Material white;
    public void FontChange()
    {
        foreach(TextMeshProUGUI textMesh in textMeshProUGUIs)
        {
            textMesh.font = asset;
            textMesh.fontSize += 4;
        }

        foreach(TextMeshProUGUI textMesh in textMeshProsWhite)
        {
            textMesh.font = asset;
            textMesh.fontMaterial = white;
            textMesh.fontSize += 4;
        }

        foreach(TextMeshProUGUI textMesh in textMeshProsGrey)
        {
            textMesh.font = asset;
            textMesh.fontMaterial = grey;
            textMesh.fontSize += 4;
        }
    }

}
