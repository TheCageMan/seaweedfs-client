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
using smartbox.SeaweedFs.Client.Core.Http;

namespace smartbox.SeaweedFs.Client.Test
{
    /// <summary>
    /// Summary description for MasterWrapperTest
    /// </summary>
    [TestFixture]
    public class MasterWrapperTest : BaseFixture
    {
        [Test]
        public void LookupVolume()
        {
            var values = new LookupVolumeParams("1");
            var result = MasterWrapper.LookupVolume(values).GetAwaiter().GetResult();
            Assert.AreEqual(values.VolumeId, result.VolumeId);
        }

        [Test]
        public void ForceGarbageCollection()
        {
            var values = new ForceGarbageCollectionParams(0.4f);
            MasterWrapper.ForceGarbageCollection(values);
        }

        [Test]
        public void PreAllocateVolumes()
        {
            var values = new PreAllocateVolumesParams(1);
            var result = MasterWrapper.PreAllocateVolumes(values).GetAwaiter().GetResult();
            Assert.AreEqual(values.Count, result.Count);
        }

        /// <summary>
        /// This test requires 2 active volumes
        /// </summary>
        [Test]
        public void AssignFileKey()
        {
            var values = new AssignFileKeyParams("000", 1, null, null, "test");
            var result = MasterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
            Assert.AreEqual(values.Count, result.Count);
        }
    }
}
