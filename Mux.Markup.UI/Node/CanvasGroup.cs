using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.CanvasGroup" />.
    /// </summary>
    public class CanvasGroup : Object<UnityEngine.CanvasGroup>
    {
        /// <summary>Backing store for the <see cref="Alpha" /> property.</summary>
        public static readonly BindableProperty AlphaProperty = CreateBindableComponentProperty<float>(
            "Alpha",
            typeof(CanvasGroup),
            (component, value) => component.alpha = value);

        /// <summary>Backing store for the <see cref="Interactable" /> property.</summary>
        public static readonly BindableProperty InteractableProperty = CreateBindableComponentProperty<bool>(
            "Interactable",
            typeof(CanvasGroup),
            (component, value) => component.interactable = value,
            true);

        /// <summary>Backing store for the <see cref="BlocksRaycasts" /> property.</summary>
        public static readonly BindableProperty BlocksRaycastsProperty = CreateBindableComponentProperty<bool>(
            "BlocksRaycasts",
            typeof(CanvasGroup),
            (component, value) => component.blocksRaycasts = value,
            true);

        /// <summary>Backing store for the <see cref="IgnoreParentGroups" /> property.</summary>
        public static readonly BindableProperty IgnoreParentGroupsProperty = CreateBindableComponentProperty<bool>(
            "IgnoreParentGroups",
            typeof(CanvasGroup),
            (component, value) => component.ignoreParentGroups = value,
            false);

        /// <summary>A property that represents <see cref="P:UnityEngine.CanvasGroup.alpha" />.</summary>
        public float Alpha
        {
            get
            {
                return (float)GetValue(AlphaProperty);
            }

            set
            {
                SetValue(AlphaProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.CanvasGroup.interactable" />.</summary>
        public bool Interactable
        {
            get
            {
                return (bool)GetValue(InteractableProperty);
            }

            set
            {
                SetValue(InteractableProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.CanvasGroup.blocksRaycasts" />.</summary>
        public bool BlocksRaycasts
        {
            get
            {
                return (bool)GetValue(BlocksRaycastsProperty);
            }

            set
            {
                SetValue(BlocksRaycastsProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.CanvasGroup.ignoreParentGroups" />.</summary>
        public bool IgnoreParentGroups
        {
            get
            {
                return (bool)GetValue(IgnoreParentGroupsProperty);
            }

            set
            {
                SetValue(IgnoreParentGroupsProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.CanvasGroup>();
            Component.alpha = Alpha;
            Component.interactable = Interactable;
            Component.blocksRaycasts = BlocksRaycasts;
            Component.ignoreParentGroups = IgnoreParentGroups;
        }
    }
}