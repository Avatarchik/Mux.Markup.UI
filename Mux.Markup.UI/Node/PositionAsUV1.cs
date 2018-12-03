
namespace Mux.Markup
{
    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.PositionAsUV1" />.
    /// </summary>
    public class PositionAsUV1 : Object<UnityEngine.UI.PositionAsUV1>
    {
        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.PositionAsUV1>();
        }
    }
}
