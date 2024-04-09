using System.Collections.Generic;
using UnityEngine;

public class FractalLogic : MonoBehaviour
{
    public SpriteRenderer GO;
    public SpriteRenderer imageRenderer;
    List<SpriteRenderer> parents = new List<SpriteRenderer>();

    float size = 1f;
    public float scalingFactor = .67f;
    public float offset;

    int step = 0;

    Color color = Color.black;

    List<SpriteRenderer> fractalUnits = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        // rescale size
        //size = .5f;
        //// used rescaled size to instantiate renderer on previous shape, make image color black
        //var rendererGO = Instantiate(imageRenderer, new Vector3(imageRenderer.transform.position.x, imageRenderer.transform.position.y - (imageRenderer.size.y / 3f) + offset), Quaternion.AngleAxis(180f, Vector3.forward), imageRenderer.transform);
        ////   rendererGO.transform.position = new Vector3(rendererGO.transform.position.x, rendererGO.transform.position.y + (2 / 3));
        //rendererGO.transform.localScale = new Vector3(size, size, size);
        //rendererGO.color = color;
        //rendererGO.sortingOrder = step + 1;

        fractalUnits.Add(imageRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        GenerateFractal();
    }

    void GenerateFractal()
    {
        if (CanContinue())
        {
            size *= scalingFactor;
            // rescale object and more of the same rescale object

            for (int i = 0; i < fractalUnits.Count; i++)
            {
                // create children
                SpawnNewGeneration(fractalUnits[i]);
            }
            for (int i = 0; i < fractalUnits.Count; i++)
            {
                Destroy(fractalUnits[i].gameObject);
            }
            fractalUnits.Clear();

            for (int i = 0; i < parents.Count; i++)
            {
                fractalUnits.AddRange(parents[i].transform.GetComponentsInChildren<SpriteRenderer>());
                fractalUnits.Remove(parents[i]);
            }

            parents.Clear();

            // repeat process

            step++;
        }
    }
    bool CanContinue()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    void SpawnNewGeneration(SpriteRenderer unit)
    {
        var newParent = Instantiate(GO, transform.position, Quaternion.identity, transform);
        newParent.enabled = false;
        newParent.transform.localScale = new Vector3(size, size, size);
        parents.Add(newParent);

        Instantiate(GO, new Vector3(unit.transform.position.x - (1f / 4f * unit.size.x * (size / scalingFactor)), unit.transform.position.y - (1f / 4f * unit.size.y * (size / scalingFactor))), Quaternion.identity, newParent.transform);
        Instantiate(GO, new Vector3(unit.transform.position.x + (1f / 4f * unit.size.x * (size / scalingFactor)), unit.transform.position.y - (1f / 4f * unit.size.y * (size / scalingFactor))), Quaternion.identity, newParent.transform);
        Instantiate(GO, new Vector3(unit.transform.position.x, unit.transform.position.y + (1f / 4f * unit.size.y * (size / scalingFactor))), Quaternion.identity, newParent.transform);

        //  unit.transform.SetParent(newParent.transform);

    }
    #region Code for Square
    //size *= .5f;

    //    var rendererGO = Instantiate(imageRenderer, imageRenderer.transform.position, Quaternion.identity, imageRenderer.transform);
    //rendererGO.transform.localScale = new Vector3(size, size, size);
    //rendererGO.color = color;

    //    size *= 2;
    #endregion

}
