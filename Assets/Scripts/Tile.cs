using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Status status;
    MeshRenderer tileMat;
    public bool activated = false;
    public Tile nextSibling;
    public Tile prevSibling;
    public Texture mainTexture;
    public Texture markTexture;


    public event Action<Tile> OnWrongSelected;
    public event Action<Tile> OnGoodSelected;

    private void Start()
    {
        tileMat = GetComponent<MeshRenderer>();
    }

    public void CoverTile()
    {
        ChangeTileMaterial(mainTexture);
        activated = false;
    }

    public void PaintTile()
    {
        switch (status)
        {
            case Status.inactive:
                ChangeTileMaterial(mainTexture);
                break;
            case Status.activeGood:
                ChangeTileMaterial(markTexture);
                OnGoodSelected?.Invoke(this);
                break;
            case Status.activeBad:
                ChangeTileMaterial(mainTexture);
                OnWrongSelected?.Invoke(this);
                break;
        }
    }

    void ChangeTileMaterial(Texture texture)
    {
        tileMat.material.mainTexture = texture;
    }

    private void OnMouseDown()
    {
        PaintTile();
        if (!activated)
        {
            activated = true;
        }
    }
}

public enum Status
{
    inactive,
    activeGood,
    activeBad
}
