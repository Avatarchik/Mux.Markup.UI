using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Mux.Markup.Extras
{
    internal sealed class UIMeshItemCollection : TemplatableCollection<UIMeshItem>
    {
        public UIMeshItemCollection(UIMesh container) : base(container)
        {
        }

        public override void ClearList()
        {
            foreach (var item in list)
            {
                item.Dispose();
            }

            base.ClearList();
            ((UIMesh)container).Component?.SetVerticesDirty();
        }

        public override void InsertListRange(int index, IEnumerable<UIMeshItem> enumerable)
        {
            var mesh = (UIMesh)container;
            var oldCount = list.Count;

            base.InsertListRange(index, enumerable);

            foreach (var item in list.Skip(oldCount))
            {
                BindableObject.SetInheritedBindingContext(item, mesh.BindingContext);
                item.mesh = mesh;
            }

            mesh.Component?.SetVerticesDirty();
        }

        public override void MoveListRange(int from, int to, int count)
        {
            base.MoveListRange(from, to, count);
            ((UIMesh)container).Component?.SetVerticesDirty();
        }

        public override void RemoveListRange(int index, int count)
        {
            foreach (var item in list.Skip(index).Take(count))
            {
                item.Dispose();
            }

            base.RemoveListRange(index, count);
            ((UIMesh)container).Component?.SetVerticesDirty();
        }

        public override void ReplaceListRange(int index, int count, IEnumerable<UIMeshItem> enumerable)
        {
            var mesh = (UIMesh)container;

            foreach (var item in list.Skip(index).Take(count))
            {
                item.Dispose();
            }

            base.ReplaceListRange(index, count, enumerable);

            foreach (var item in list.Skip(index).Take(count))
            {
                BindableObject.SetInheritedBindingContext(item, mesh.BindingContext);
                item.mesh = mesh;
            }

            mesh.Component?.SetVerticesDirty();
        }
    }
}
