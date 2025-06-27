
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject hoveredImage;

    private void Start()
    {
        image.SetActive(true);
        hoveredImage.SetActive(false);
    }
    private void OnPointerEnter(PointerEventData eventData)
    {
        image.SetActive(false);
        hoveredImage.SetActive(true);
    }

    private void OnPointerExit(PointerEventData eventData)
    {
        image.SetActive(true);
       hoveredImage.SetActive(false);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnter(eventData);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnPointerExit(eventData);
    }
}



