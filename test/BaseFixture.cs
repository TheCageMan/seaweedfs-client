using NUnit.Framework;
using smartbox.SeaweedFs.Client.Core;

namespace smartbox.SeaweedFs.Client.Test
{
    [TestFixture]
    public class BaseFixture
    {
        protected MasterConnection Manager;
        protected Connection Connection;
        protected MasterWrapper MasterWrapper;
        protected OperationsTemplate Template;
        protected VolumeWrapper VolumeWrapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Manager = new MasterConnection("localhost")
            {
                EnableLookupVolumeCache = true
            };
            Manager.Start().GetAwaiter().GetResult();
            Connection = Manager.GetConnection();
            MasterWrapper = new MasterWrapper(Manager.GetConnection());
            Template = new OperationsTemplate(Manager);
            VolumeWrapper = new VolumeWrapper(Manager.GetConnection());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Manager.Stop().GetAwaiter().GetResult();
            Manager.Dispose();
        }
    }
}