using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Generic.InputManager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Slider _powerSlider;

        private void Awake()
        {
            _powerSlider.onValueChanged.AddListener(OnPowerSliderValueChanged);

            MessageBroker.Default
                .Publish(new PowerSliderData(_powerSlider.value));
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                MessageBroker.Default
                    .Publish(new MouseClickData(true));
            }
            else
            {
                MessageBroker.Default
                    .Publish(new MouseClickData(false));
            }
        }

        private void OnPowerSliderValueChanged(float value)
        {
            MessageBroker.Default
                .Publish(new PowerSliderData(value));
        }

        private static bool IsPointerOverUIElement()
        {
            List<RaycastResult> eventSystemRaysastResults = GetEventSystemRaycastResults();

            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }

        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}