using Mux.Markup.Extras;
using NUnit.Framework;
using System;

namespace Mux.Tests.Markup.Extras
{
    [TestFixture]
    public static class Picker
    {
        [Test]
        public static void OneWay()
        {
            var group = new PickerGroup<int> { Value = 0 };
            var picker = new Picker<int> { PickerGroup = group, Value = 1 };

            Assert.IsFalse(picker.IsOn);

            group.Value = 1;
            Assert.IsTrue(picker.IsOn);

            picker.Value = 0;
            Assert.IsFalse(picker.IsOn);

            group.Value = 0;
            Assert.IsTrue(picker.IsOn);
        }

        [Test]
        public static void OneWayToSource()
        {
            var group = new PickerGroup<int> { Value = 0 };
            var picker = new Picker<int> { PickerGroup = group, Value = 1 };

            picker.IsOn = true;
            Assert.AreEqual(1, group.Value);
        }

        [Test]
        public static void OneWayToNullifiedSource()
        {
            var group = new PickerGroup<int> { Value = 0 };
            var picker = new Picker<int> { PickerGroup = group, Value = 1 };

            picker.PickerGroup = null;
            picker.IsOn = true;
            Assert.AreEqual(0, group.Value);
        }

        [Test]
        public static void GroupChanged()
        {
            var group = new PickerGroup<int> { Value = 0 };
            var picker = new Picker<int> { PickerGroup = group, Value = 1 };
            var gameObject = new UnityEngine.GameObject();

            try
            {
                group.AddTo(gameObject);
                picker.AddTo(gameObject);

                Assert.Throws(
                    Is.TypeOf<Exception>().And.Message.EqualTo("setting Group property of Picker is prohibited"),
                    () => picker.Group = null);
            }
            finally
            {
                UnityEngine.Object.Destroy(gameObject);
            }
        }
    }
}
