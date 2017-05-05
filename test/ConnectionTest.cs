/*
 * Copyright 2017 smartbox software solutions
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using NUnit.Framework;

namespace smartbox.SeaweedFs.Client.Test
{
    /// <summary>
    /// Summary description for ConnectionTest
    /// </summary>
    [TestFixture]
    public class ConnectionTest : BaseFixture
    {
        [Test]
        public void ForceGarbageCollection()
        {
            Connection.ForceGarbageCollection();
        }

        [Test]
        public void ForceGarbageCollectionWithThreshold()
        {
            Connection.ForceGarbageCollection(0.4f);
        }

        [Test]
        public void PreAllocateVolumes()
        {
            Connection.PreAllocateVolumes(0, 0, 0, 2, null, null).GetAwaiter();
        }

        [Test]
        public void GetSystemClusterStatus()
        {
            Assert.IsTrue(Connection.SystemClusterStatus.Leader.IsActive);
        }

        [Test]
        public void GetSystemTopologyStatus()
        {
            Assert.IsTrue(Connection.SystemTopologyStatus.Max > 0);
        }

        [Test]
        public void IsConnectionClose()
        {
            Assert.IsFalse(Connection.IsConnectionClose);
        }

        [Test]
        public void GetLeaderUrl()
        {
            Assert.IsNotNull(Connection.LeaderUrl);
        }

        [Test]
        public void GetVolumeStatus()
        {
            var dataNodeUrl = Connection.SystemTopologyStatus
                        .DataCenters[0].Racks[0].DataNodes[0].Url;
            var volumeStatus = Connection.GetVolumeStatus(dataNodeUrl).GetAwaiter().GetResult();
            Assert.AreEqual(dataNodeUrl, volumeStatus.Url);
        }
    }
}
