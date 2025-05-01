using UnityEngine;

public static class RectTransformExtensions
{
    public static Rect GetWorldRect(this RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0], corners[2] - corners[0]);
    }
}
