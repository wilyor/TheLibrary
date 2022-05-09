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

    public event Action<Tile> OnWrongSelected;
    public event Action<Tile> OnGoodSelected;

    private void Start()
    {
        tileMat = GetComponent<MeshRenderer>();
    }

    public void CoverTile()
    {
        ChangeTileMaterial(Color.gray);
        activated = false;
    }

    public void PaintTile()
    {
        switch (status)
        {
            case Status.inactive:
                ChangeTileMaterial(Color.white);
                break;
            case Status.activeGood:
                ChangeTileMaterial(Color.green);
                OnGoodSelected?.Invoke(this);
                break;
            case Status.activeBad:
                ChangeTileMaterial(Color.red);
                OnWrongSelected?.Invoke(this);
                break;
        }
    }

    void ChangeTileMaterial(Color color)
    {
        tileMat.material.color = color;
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
