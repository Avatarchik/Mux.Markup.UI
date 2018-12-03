using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A <see cref="BindableObject" /> that represents <see cref="T:UnityEngine.EventSystems.EventTrigger.Entry" />.
    /// </summary>
    public class EventTriggerEntry : BindableObject
    {
        /// <summary>Backing store for the <see cref="EventID" /> property.</summary>
        public static readonly BindableProperty EventIDProperty = BindableProperty.Create(
            "EventID",
            typeof(UnityEngine.EventSystems.EventTriggerType),
            typeof(EventTriggerEntry),
            UnityEngine.EventSystems.EventTriggerType.PointerEnter,
            BindingMode.OneWay,
            null,
            OnEventIDChanged);

        private static void OnEventIDChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((EventTriggerEntry)sender).entry.eventID = (UnityEngine.EventSystems.EventTriggerType)newValue;
        }

        internal readonly UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry
        {
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
        };

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.EventSystems.EventTrigger.Entry.eventID" />.
        /// </summary>
        public UnityEngine.EventSystems.EventTriggerType EventID
        {
            get
            {
                return (UnityEngine.EventSystems.EventTriggerType)GetValue(EventIDProperty);
            }

            set
            {
                SetValue(EventIDProperty, value);
            }
        }

        /// <summary>
        /// An event that represents <see cref="P:UnityEngine.EventSystems.EventTrigger.Entry.callback" />.
        /// </summary>
        public event UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData> Callback
        {
            add
            {
                Forms.mainThread.Post(state => entry.callback.AddListener(value), null);
            }

            remove
            {
                Forms.mainThread.Post(state => entry.callback.RemoveListener(value), null);
            }
        }
    }

    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.EventSystems.EventTrigger" />.
    /// </summary>
    [ContentProperty("Triggers")]
    public class EventTrigger : Object<UnityEngine.EventSystems.EventTrigger>
    {
        private sealed class TriggerCollection : TemplatableCollection<EventTriggerEntry>
        {
            public readonly TemplatableCollectionList<UnityEngine.EventSystems.EventTrigger.Entry> _entries =
                new TemplatableCollectionList<UnityEngine.EventSystems.EventTrigger.Entry>();

            private static UnityEngine.EventSystems.EventTrigger.Entry EntrySelector(EventTriggerEntry source)
            {
                return source.entry;
            }

            public TriggerCollection(BindableObject container) : base(container)
            {
            }

            public override void ClearList()
            {
                base.ClearList();
                Forms.mainThread.Post(state => _entries.ClearList(), null);
            }

            public override void InsertListRange(int index, IEnumerable<EventTriggerEntry> enumerable)
            {
                base.InsertListRange(index, enumerable);
                Forms.mainThread.Post(state => _entries.InsertListRange(index, enumerable.Select(EntrySelector)), null);
            }

            public override void MoveListRange(int from, int to, int count)
            {
                base.MoveListRange(from, to, count);
                Forms.mainThread.Post(state => _entries.MoveListRange(from, to, count), null);
            }

            public override void RemoveListRange(int index, int count)
            {
                base.RemoveListRange(index, count);
                Forms.mainThread.Post(state => _entries.RemoveListRange(index, count), null);
            }

            public override void ReplaceListRange(int index, int count, IEnumerable<EventTriggerEntry> enumerable)
            {
                base.ReplaceListRange(index, count, enumerable);
                Forms.mainThread.Post(state => _entries.ReplaceListRange(index, count, enumerable.Select(EntrySelector)), null);
            }
        }

        /// <summary>Backing store for the <see cref="Triggers" /> property.</summary>
        public static readonly BindableProperty TriggersProperty = BindableProperty.CreateReadOnly(
            "Triggers",
            typeof(ICollection<EventTriggerEntry>),
            typeof(EventTrigger),
            null,
            BindingMode.OneWayToSource,
            null,
            null,
            null,
            null,
            CreateDefaultTriggers).BindableProperty;

        /// <summary>Backing store for the <see cref="TriggersSource" /> property.</summary>
        public static readonly BindableProperty TriggersSourceProperty = BindableProperty.Create(
            "TriggersSource",
            typeof(IEnumerable),
            typeof(EventTrigger),
            null,
            BindingMode.OneWay,
            null,
            OnTriggersSourceChanged);

        /// <summary>Backing store for the <see cref="TriggerTemplate" /> property.</summary>
        public static readonly BindableProperty TriggerTemplateProperty = BindableProperty.Create(
            "TriggerTemplate",
            typeof(DataTemplate),
            typeof(EventTrigger),
            null,
            BindingMode.OneWay,
            null,
            OnTriggerTemplateChanged);

        private static object CreateDefaultTriggers(BindableObject sender)
        {
            return ((EventTrigger)sender)._triggers;
        }

        private static void OnTriggersSourceChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((EventTrigger)sender)._triggers.ChangeSource((IEnumerable)newValue);
        }

        private static void OnTriggerTemplateChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((EventTrigger)sender)._triggers.ChangeTemplate((DataTemplate)newValue);
        }

        private readonly TriggerCollection _triggers;

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.EventSystems.EventTrigger.triggers" />.
        /// </summary>
        /// <remarks>This is the content property; you can write as child elements in XAML.</remarks>
        public ICollection<EventTriggerEntry> Triggers => (ICollection<EventTriggerEntry>)GetValue(TriggersProperty);

        /// <summary>Gets or sets the source of entries to template and display.</summary>
        public IEnumerable TriggersSource
        {
            get
            {
                return (IEnumerable)GetValue(TriggersSourceProperty);
            }

            set
            {
                SetValue(TriggersSourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:DataTemplate" /> to apply to the <see cref="TriggersSource" />.
        /// </summary>
        public DataTemplate TriggerTemplate
        {
            get
            {
                return (DataTemplate)GetValue(TriggerTemplateProperty);
            }

            set
            {
                SetValue(TriggerTemplateProperty, value);
            }
        }

        public EventTrigger()
        {
            _triggers = new TriggerCollection(this);
            _triggers.ChangeSource(TriggersSource);
            _triggers.ChangeTemplate(TriggerTemplate);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
            Component.triggers = _triggers._entries.list;
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            foreach (var trigger in _triggers)
            {
                SetInheritedBindingContext(trigger, BindingContext);
            }
        }
    }
}
