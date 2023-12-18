using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine;

public class ScrollWithMouseInput : MonoBehaviour
{
    [SerializeField] private float posY = -1200;
    [SerializeField] private float speed = 2500;
    [SerializeField] private RectTransform startPos;
    
    private async void Start() {
        await Task.Delay(2000);
        startPos.anchoredPosition = new Vector2(startPos.anchoredPosition.x, posY);
    }


    private void Update() {
        if (!startPos.gameObject.activeInHierarchy) return;

        var scrollValue = Mouse.current.scroll.ReadValue();
        if (scrollValue == default) return;
        var value = scrollValue.y > 0 ? -1 : 1;

        startPos.anchoredPosition += new Vector2(0, value * Time.deltaTime * speed);
    }
}
