using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="T:UnityEngine.RenderMode.WorldSpace" />
    /// and its rendering properties.
    /// </summary>
    public class WorldSpace : Render
    {
        /// <summary>Backing store for the <see cref="WorldCamera" /> property.</summary>
        public static readonly BindableProperty WorldCameraProperty =
            CreateBindableWorldCameraProperty(typeof(WorldSpace));

        /// <summary>Backing store for the <see cref="SortingLayer" /> property.</summary>
        public static readonly BindableProperty SortingLayerProperty =
            CreateBindableSortingLayerProperty(typeof(WorldSpace));

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.worldCamera" />.
        /// </summary>
        public UnityEngine.Camera WorldCamera
        {
            get
            {
                return (UnityEngine.Camera)GetValue(WorldCameraProperty);
            }

            set
            {
                SetValue(WorldCameraProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.sortingLayerID" />.
        /// </summary>
        [TypeConverter(typeof(LayerTypeConverter))]
        public int SortingLayer
        {
            get
            {
                return (int)GetValue(SortingLayerProperty);
            }

            set
            {
                SetValue(SortingLayerProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
            base.InitializeBodyInMainThread();
            Body.worldCamera = WorldCamera;
            Body.sortingLayerID = SortingLayer;
        }
    }
}
