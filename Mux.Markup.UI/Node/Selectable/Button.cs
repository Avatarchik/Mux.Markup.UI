
namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Button" />.</summary>
    public class Button : Selectable<UnityEngine.UI.Button>
    {
        private readonly UnityEngine.UI.Button.ButtonClickedEvent _onClick = new UnityEngine.UI.Button.ButtonClickedEvent();

        /// <summary>An event that represents <see cref="P:UnityEngine.UI.Button.onClick" />.</summary>
        public event UnityEngine.Events.UnityAction OnClick
        {
            add
            {
                Forms.mainThread.Send(state => _onClick.AddListener(value), null);
            }

            remove
            {
                Forms.mainThread.Send(state => _onClick.RemoveListener(value), null);
            }
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();
            Body.onClick = _onClick;
        }
    }
}
