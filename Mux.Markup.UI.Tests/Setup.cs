using NUnit.Framework;

namespace Mux.Tests
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            Mux.Forms.mainThread = System.Threading.SynchronizationContext.Current;
            Mux.Forms.Init();
            System.Diagnostics.Debug.Listeners.Add(new Mux.TraceListener());
        }
    }
}
