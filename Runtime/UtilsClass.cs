using UnityEngine;

namespace CodeEasyYT.Utils
{
    /// <summary>
    /// This class is for basic things for debuging/gameplay.
    /// Use these to make code easier to read, and easier to manage.
    /// </summary>
    public static class UtilsClass
    {
        //Create world text
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
        {
            if (color == null) color = Color.black;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

        //Create world sprite
        public static SpriteRenderer CreateWorldSprite(Sprite texture, Transform parent = null, Color? color = null, Vector3 localPosition = default(Vector3), Vector3 localScale = default(Vector3), int sortingOrder = 5000)
        {
            if (color == null) color = Color.white;
            if (localScale == default(Vector3)) localScale = Vector3.one;
            return CreateWorldSprite(parent, texture, (Color)color, localPosition, localScale, sortingOrder);
        }
        public static SpriteRenderer CreateWorldSprite(Transform parent, Sprite texture, Color color, Vector3 localPosition, Vector3 localScale, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Sprite", typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localScale = localScale;
            SpriteRenderer spriteRender = gameObject.GetComponent<SpriteRenderer>();
            spriteRender.sprite = texture;
            spriteRender.color = color;
            spriteRender.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            return spriteRender;
        }

        #region Get Mouse Position
        /// <summary>
        /// Use this to get mouse position on your world space
        /// </summary>
        /// <returns>Vector2</returns>
        public static Vector2 GetMouseWorldPosition()
        {
            return new Vector2(GetMouseWorldPositionZ().x, GetMouseWorldPositionZ().y);
        }
        /// <summary>
        /// This is same with GetMouseWorldPosition but it also returns Z.
        /// </summary>
        /// <returns>Vector3</returns>
        public static Vector3 GetMouseWorldPositionZ()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        #endregion
    }
}