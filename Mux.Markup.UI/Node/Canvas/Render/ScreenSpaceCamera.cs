using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="T:UnityEngine.RenderMode.ScreenSpaceCamera" />
    /// and its rendering properties.
    /// </summary>
    public class ScreenSpaceCamera : Render
    {
        /// <summary>Backing store for the <see cref="PixelPerfect" /> property.</summary>
        public static readonly BindableProperty PixelPerfectProperty =
            CreateBindablePixelPerfectProperty(typeof(ScreenSpaceCamera));

        /// <summary>Backing store for the <see cref="OverridePixelPerfect" /> property.</summary>
        public static readonly BindableProperty OverridePixelPerfectProperty =
            CreateBindableOverridePixelPerfectProperty(typeof(ScreenSpaceCamera));

        /// <summary>Backing store for the <see cref="WorldCamera" /> property.</summary>
        public static readonly BindableProperty WorldCameraProperty =
            CreateBindableWorldCameraProperty(typeof(ScreenSpaceCamera));

        /// <summary>Backing store for the <see cref="PlaneDistance" /> property.</summary>
        public static readonly BindableProperty PlaneDistanceProperty =
            CreateBindablePlaneDistanceProperty(typeof(ScreenSpaceCamera));

        /// <summary>Backing store for the <see cref="SortingLayer" /> property.</summary>
        public static readonly BindableProperty SortingLayerProperty =
            CreateBindableSortingLayerProperty(typeof(ScreenSpaceCamera));

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.pixelPerfect" />.
        /// </summary>
        public bool PixelPerfect
        {
            get
            {
                return (bool)GetValue(PixelPerfectProperty);
            }

            set
            {
                SetValue(PixelPerfectProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.overridePixelPerfect" />.
        /// </summary>
        public bool OverridePixelPerfect
        {
            get
            {
                return (bool)GetValue(OverridePixelPerfectProperty);
            }

            set
            {
                SetValue(OverridePixelPerfectProperty, value);
            }
        }

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
        /// A property that represents <see cref="P:UnityEngine.Canvas.planeDistance" />.
        /// </summary>
        public float PlaneDistance
        {
            get
            {
                return (float)GetValue(PlaneDistanceProperty);
            }

            set
            {
                SetValue(PlaneDistanceProperty, value);
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
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
            base.InitializeComponentInMainThread();
            Component.pixelPerfect = PixelPerfect;
            Component.overridePixelPerfect = OverridePixelPerfect;
            Component.worldCamera = WorldCamera;
            Component.planeDistance = PlaneDistance;
            Component.sortingLayerID = SortingLayer;
        }
    }
}
