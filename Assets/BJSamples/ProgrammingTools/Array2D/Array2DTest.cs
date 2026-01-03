using System.Collections;
using System.Collections.Generic;
using BJ;
using Unity.VisualScripting;
using UnityEngine;

public class Array2DTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        ShowcaseDenseArray();
        ShowcaseSparseArray();
    }

    private void ShowcaseDenseArray()
    {
        Array2D<int> array = new Array2D<int>(5, 5, 0);

        // Single index can work for when you know you need to hit every element.
        // Doesn't give you access to X, Y position by default but you can calculate it if you have to.
        for (int x = 0; x < array.Count; x++)
        {
            array[x] = x;
        }

        foreach (KeyValuePair<Vector2Int, int> x in array)
        {
            Debug.Log($"Expensive array loop ({x.Key.x}, {x.Key.y}) = {x.Value}");
        }

        array.FlatForeach((x, y, v) =>
        {
            Debug.Log($"Efficient array loop ({x}, {y}) = {v}");
        });

        // Expand array
        array[7, 7] = 7 * 7;

        Debug.Log(array.ToString());
    }

    private void ShowcaseSparseArray()
    {
        Debug.Log("Sparse Array Showcase");
        SparseArray2D<string> sparseArray = new SparseArray2D<string>("N");

        sparseArray[1, 0] = "Y";
        sparseArray[-1, 1] = "Y";

        Debug.Log("First assignments:");
        Debug.Log(sparseArray.ToString());

        sparseArray[8, 8] = "Y";
        sparseArray[1, 0] = "N";

        Debug.Log(sparseArray.ToString());

        Debug.Log("Demonstrating for loop:");

        // There is no more efficient way to do a for-each for this implementation of a 2D array.
        foreach (KeyValuePair<Vector2Int, string> field in sparseArray)
        {
            Debug.Log($"At position {field.Key.x}, {field.Key.y} = {field.Value}");
        }

        Debug.Log("End loop demo.");

        sparseArray.RemoveAt(8, 8);

        Debug.Log("Array after removing (8, 8)");

        Debug.Log(sparseArray.ToString());

        Debug.Log("Array after dimension trim");

        sparseArray.TrimDimensions();

        Debug.Log(sparseArray.ToString());
    }
}
