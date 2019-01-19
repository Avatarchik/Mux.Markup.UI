using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xamarin.Forms;

namespace Mux.Markup.Extras
{
    internal sealed class UIMeshItemCollection : TemplatableCollection<UIMeshItem>
    {
        private readonly ImmutableList<UIMeshItem>.Builder _builder =
            ImmutableList.CreateBuilder<UIMeshItem>();

        public UIMeshItemCollection(UIMesh container) : base(container)
        {
        }

        protected override IList<UIMeshItem> GetList()
        {
            return _builder;
        }

        public override void ClearList()
        {
            foreach (var item in _builder)
            {
                item.Dispose();
            }

            base.ClearList();
            ((UIMesh)container).Component?.SetVerticesDirty();
        }

        public override void InsertListRange(int index, IEnumerable<UIMeshItem> enumerable)
        {
            var mesh = (UIMesh)container;
            var oldCount = _builder.Count;

            _builder.InsertRange(index, enumerable);

            foreach (var item in _builder.Skip(oldCount))
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
            while (count > 0)
            {
                _builder[index].Dispose();
                _builder.RemoveAt(index);
            }

            ((UIMesh)container).Component?.SetVerticesDirty();
        }

        public override void ReplaceListRange(int index, int count, IEnumerable<UIMeshItem> enumerable)
        {
            var mesh = (UIMesh)container;

            foreach (var item in _builder.Skip(index).Take(count))
            {
                item.Dispose();
            }

            base.ReplaceListRange(index, count, enumerable);

            foreach (var item in _builder.Skip(index).Take(count))
            {
                BindableObject.SetInheritedBindingContext(item, mesh.BindingContext);
                item.mesh = mesh;
            }

            mesh.Component?.SetVerticesDirty();
        }

        public ImmutableList<UIMeshItem> ToImmutable()
        {
            return _builder.ToImmutable();
        }
    }
}
