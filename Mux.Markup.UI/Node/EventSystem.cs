using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.EventSystems.EventSystem" />.</summary>
    public class EventSystem : Object<UnityEngine.EventSystems.EventSystem>
    {
        /// <summary>Backing store for the <see cref="FirstSelectedGameObject" /> property.</summary>
        public static readonly BindableProperty FirstSelectedGameObjectProperty = CreateBindableComponentProperty<UnityEngine.GameObject>(
            "FirstSelectedGameObject",
            typeof(EventSystem),
            (component, value) => component.firstSelectedGameObject = value);

        /// <summary>Backing store for the <see cref="SendNavigationEvents" /> property.</summary>
        public static readonly BindableProperty SendNavigationEventsProperty = CreateBindableComponentProperty<bool>(
            "SendNavigationEvents",
            typeof(EventSystem),
            (component, value) => component.sendNavigationEvents = value,
            true);

        /// <summary>Backing store for the <see cref="PixelDragThreshold" /> property.</summary>
        public static readonly BindableProperty PixelDragThresholdProperty = CreateBindableComponentProperty<int>(
            "PixelDragThreshold",
            typeof(EventSystem),
            (component, value) => component.pixelDragThreshold = value,
            10);

        /// <summary>A property that represents <see cref="P:UnityEngine.EventSystems.EventSystem.firstSelectedGameObject" />.</summary>
        public UnityEngine.GameObject FirstSelectedGameObject
        {
            get
            {
                return (UnityEngine.GameObject)GetValue(FirstSelectedGameObjectProperty);
            }

            set
            {
                SetValue(FirstSelectedGameObjectProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.EventSystems.EventSystem.sendNavigationEvents" />.</summary>
        public bool SendNavigationEvents
        {
            get
            {
                return (bool)GetValue(SendNavigationEventsProperty);
            }

            set
            {
                SetValue(SendNavigationEventsProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.EventSystems.EventSystem.pixelDragThreshold" />.</summary>
        public int PixelDragThreshold
        {
            get
            {
                return (int)GetValue(PixelDragThresholdProperty);
            }

            set
            {
                SetValue(PixelDragThresholdProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.EventSystems.EventSystem>();
            Component.firstSelectedGameObject = FirstSelectedGameObject;
            Component.sendNavigationEvents = SendNavigationEvents;
            Component.pixelDragThreshold = PixelDragThreshold;
        }
    }
}
