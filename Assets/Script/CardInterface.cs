using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPointerDragInterface : IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    
}
