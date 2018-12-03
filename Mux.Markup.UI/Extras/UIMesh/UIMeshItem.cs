using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace Mux.Markup.Extras
{
    /// <summary>A class that represents vertices and triangles.</summary>
    public abstract class UIMeshItem : BindableObject, IDisposable
    {
        internal UIMesh mesh;
        internal abstract void AddTo(UnityEngine.UI.VertexHelper helper);

        /// <summary>
        /// A method that maps position in <see cref="T:UnityEngine.RectTransform" />
        /// normalized into [0, 1] to local space.
        /// </summary>
        protected UnityEngine.UIVertex Map(UnityEngine.UIVertex vertex)
        {
            var rect = ((UnityEngine.RectTransform)mesh.Component.transform).rect;

            vertex.position.x *= rect.width;
            vertex.position.y *= rect.height;

            return vertex;
        }

        public virtual void Dispose()
        {
            UnapplyBindings();
            mesh = null;
        }
    }

    /// <summary>
    /// A <see cref="UIMeshItem" /> with utilities useful to handle
    /// <see cref="ICollection{T}" />.
    /// </summary>
    public abstract class UIMeshItemWithCollection : UIMeshItem
    {
        /// <summary>
        /// A convenient method to create a new instance of the <see cref="BindableProperty" />
        /// class that represents a field or property of a <see cref="ICollection{T}" />.
        /// </summary>
        protected static BindableProperty CreateBindableCollectionProperty<T>(string name, Type declarer)
        {
            return BindableProperty.Create(
                name,
                typeof(ICollection<T>),
                declarer,
                null,
                BindingMode.OneWay,
                null,
                OnCollectionChanged,
                null,
                null,
                sender => CreateDefaultCollection<T>(sender));
        }

        private static ObservableCollection<T> CreateDefaultCollection<T>(BindableObject sender)
        {
            var collection = new ObservableCollection<T>();
            collection.CollectionChanged += ((UIMeshItemWithCollection)sender)._collectionHandler;
            return collection;
        }

        private static void OnCollectionChanged(BindableObject sender, object oldValue, object newValue)
        {
            var notifyingOld = oldValue as INotifyCollectionChanged;
            var notifyingNew = newValue as INotifyCollectionChanged;
            var item = (UIMeshItemWithCollection)sender;

            if (notifyingOld != null)
            {
                notifyingOld.CollectionChanged -= item._collectionHandler;
            }

            if (notifyingNew != null)
            {
                notifyingNew.CollectionChanged += item._collectionHandler;
            }

            item.mesh?.Component?.SetVerticesDirty();
        }

        private readonly NotifyCollectionChangedEventHandler _collectionHandler;

        public UIMeshItemWithCollection()
        {
            var weak = new WeakReference<UIMeshItemWithCollection>(this);
            NotifyCollectionChangedEventHandler handler = null;

            handler = (sender, args) =>
            {
                UIMeshItemWithCollection strong;

                if (weak.TryGetTarget(out strong))
                {
                    strong.mesh?.Component?.SetVerticesDirty();
                }
                else
                {
                    ((INotifyCollectionChanged)sender).CollectionChanged -= handler;
                }
            };

            _collectionHandler = handler;
        }

        internal void Unsubscribe(object collection)
        {
            var notifying = collection as INotifyCollectionChanged;

            if (notifying != null)
            {
                notifying.CollectionChanged -= _collectionHandler;
            }
        }
    }

    /// <summary>A <see cref="UIMeshItem" /> that represents a quad.</summary>
    /// <seealso cref="M:UnityEngine.UI.VertexHelper.AddUIVertexQuad" />
    [ContentProperty("Verts")]
    public class UIVertexQuad : UIMeshItemWithCollection
    {
        /// <summary>Backing store for <see cref="Verts" />.</summary>
        public static readonly BindableProperty VertsProperty =
            CreateBindableCollectionProperty<UnityEngine.UIVertex>("Verts", typeof(UIVertexQuad));

        /// <summary>4 Vertices representing the quad.</summary>
        public ICollection<UnityEngine.UIVertex> Verts
        {
            get
            {
                return (ICollection<UnityEngine.UIVertex>)GetValue(VertsProperty);
            }

            set
            {
                SetValue(VertsProperty, value);
            }
        }

        internal sealed override void AddTo(UnityEngine.UI.VertexHelper helper)
        {
            if (Verts != null)
            {
                var count = helper.currentVertCount;

                foreach (var vert in Verts)
                {
                    helper.AddVert(Map(vert));
                }

                helper.AddTriangle(count, count + 1, count + 2);
                helper.AddTriangle(count + 2, count + 3, count);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Unsubscribe(Verts);
        }
    }

    /// <summary>
    /// A <see cref="UIMeshItem" /> that represents a stream of custom
    /// <see cref="T:UnityEngine.UIVertex" /> and corresponding indices.
    /// </summary>
    /// <seealso cref="M:UnityEngine.UI.VertexHelper.AddUIVertexStream" />
    public class UIVertexStream : UIMeshItemWithCollection
    {
        /// <summary>Backing store for <see cref="Verts" />.</summary>
        public static readonly BindableProperty VertsProperty =
            CreateBindableCollectionProperty<UnityEngine.UIVertex>("Verts", typeof(UIVertexStream));

        /// <summary>Backing store for <see cref="Indices" />.</summary>
        public static readonly BindableProperty IndicesProperty =
            CreateBindableCollectionProperty<int>("Indices", typeof(UIVertexStream));

        /// <summary>
        /// The custom stream of verts to add to the <see cref="UIMesh" /> internal data.
        /// </summary>
        public ICollection<UnityEngine.UIVertex> Verts
        {
            get
            {
                return (ICollection<UnityEngine.UIVertex>)GetValue(VertsProperty);
            }

            set
            {
                SetValue(VertsProperty, value);
            }
        }

        /// <summary>
        /// The custom stream of indices to add to the <see cref="UIMesh" /> internal data.
        /// </summary>
        public ICollection<int> Indices
        {
            get
            {
                return (ICollection<int>)GetValue(IndicesProperty);
            }

            set
            {
                SetValue(IndicesProperty, value);
            }
        }

        internal sealed override void AddTo(UnityEngine.UI.VertexHelper helper)
        {
            if (Verts != null && Indices != null)
            {
                helper.AddUIVertexStream(Verts.Select(Map).ToList(), Indices.ToList());
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Unsubscribe(Verts);
            Unsubscribe(Indices);
        }
    }

    /// <summary>A <see cref="UIMeshItem" /> that represents a list of triangles.</summary>
    /// <seealso cref="M:UnityEngine.UI.VertexHelper.AddUIVertexTriangleStream" />
    [ContentProperty("Verts")]
    public class UIVertexTriangleStream : UIMeshItemWithCollection
    {
        /// <summary>Backing store for <see cref="Verts" />.</summary>
        public static readonly BindableProperty VertsProperty =
            CreateBindableCollectionProperty<UnityEngine.UIVertex>("Verts", typeof(UIVertexStream));

        /// <summary>Vertices to add. Length should be divisible by 3.</summary>
        public ICollection<UnityEngine.UIVertex> Verts
        {
            get
            {
                return (ICollection<UnityEngine.UIVertex>)GetValue(VertsProperty);
            }

            set
            {
                SetValue(VertsProperty, value);
            }
        }

        internal sealed override void AddTo(UnityEngine.UI.VertexHelper helper)
        {
            if (Verts != null)
            {
                helper.AddUIVertexTriangleStream(Verts.Select(Map).ToList());
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Unsubscribe(Verts);
        }
    }

    /// <summary>A <see cref="UIMeshItem" /> that represents a triangle.</summary>
    /// <seealso cref="M:UnityEngine.UI.VertexHelper.AddTriangle" />
    [ContentProperty("Indices")]
    public class Triangle : UIMeshItem
    {
        /// <summary>Backing store for <see cref="Indices" />.</summary>
        public static readonly BindableProperty IndicesProperty = BindableProperty.Create(
            "Indices",
            typeof(UnityEngine.Vector3Int),
            typeof(Triangle),
            UnityEngine.Vector3Int.zero,
            BindingMode.OneWay,
            null,
            OnIndicesChanged);

        private static void OnIndicesChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Triangle)sender).mesh?.Component?.SetVerticesDirty();
        }

        /// <summary>Indicies into the positions array.</summary>
        public UnityEngine.Vector3Int Indices
        {
            get
            {
                return (UnityEngine.Vector3Int)GetValue(IndicesProperty);
            }

            set
            {
                SetValue(IndicesProperty, value);
            }
        }

        internal sealed override void AddTo(UnityEngine.UI.VertexHelper helper)
        {
            helper.AddTriangle(Indices.x, Indices.y, Indices.z);
        }
    }

    /// <summary>A <see cref="UIMeshItem" /> that represents a single vertex.</summary>
    /// <seealso cref="M:UnityEngine.UI.VertexHelper.AddVert" />
    [ContentProperty("Value")]
    public class Vert : UIMeshItem
    {
        /// <summary>Backing store for <see cref="Value" />.</summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            "Value",
            typeof(UnityEngine.UIVertex),
            typeof(Vert),
            UnityEngine.UIVertex.simpleVert,
            BindingMode.OneWay,
            null,
            OnValueChanged);

        private static void OnValueChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Vert)sender).mesh?.Component?.SetVerticesDirty();
        }

        /// <summary>A property that represents a vertex.</summary>
        public UnityEngine.UIVertex Value
        {
            get
            {
                return (UnityEngine.UIVertex)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        internal sealed override void AddTo(UnityEngine.UI.VertexHelper helper)
        {
            helper.AddVert(Map(Value));
        }
    }
}
