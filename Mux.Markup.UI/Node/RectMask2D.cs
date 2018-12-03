using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.RectMask2D" />.
    /// </summary>
    public sealed class RectMask2D : Object<UnityEngine.UI.RectMask2D>
    {
        /// <inheritdoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.RectMask2D>();
        }
    }
}
