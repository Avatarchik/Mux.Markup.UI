using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Component{T}" /> that represents <see cref="T:UnityEngine.EventSystems.EventSystem" />.</summary>
    public class EventSystem : Component<UnityEngine.EventSystems.EventSystem>
    {
        /// <summary>Backing store for the <see cref="FirstSelectedGameObject" /> property.</summary>
        public static readonly BindableProperty FirstSelectedGameObjectProperty = CreateBindableBodyProperty<UnityEngine.GameObject>(
            "FirstSelectedGameObject",
            typeof(EventSystem),
            (body, value) => body.firstSelectedGameObject = value);

        /// <summary>Backing store for the <see cref="SendNavigationEvents" /> property.</summary>
        public static readonly BindableProperty SendNavigationEventsProperty = CreateBindableBodyProperty<bool>(
            "SendNavigationEvents",
            typeof(EventSystem),
            (body, value) => body.sendNavigationEvents = value,
            true);

        /// <summary>Backing store for the <see cref="PixelDragThreshold" /> property.</summary>
        public static readonly BindableProperty PixelDragThresholdProperty = CreateBindableBodyProperty<int>(
            "PixelDragThreshold",
            typeof(EventSystem),
            (body, value) => body.pixelDragThreshold = value,
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
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();

            Body.firstSelectedGameObject = FirstSelectedGameObject;
            Body.sendNavigationEvents = SendNavigationEvents;
            Body.pixelDragThreshold = PixelDragThreshold;
        }
    }
}
