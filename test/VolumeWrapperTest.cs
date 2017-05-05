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

using System.IO;
using System.Text;
using NUnit.Framework;
using smartbox.SeaweedFs.Client.Core.Http;

namespace smartbox.SeaweedFs.Client.Test
{
    /// <summary>
    /// Summary description for VolumeWrapperTest
    /// </summary>
    [TestFixture]
    public class VolumeWrapperTest : BaseFixture
    {
        [Test]
        public void CheckFileExist()
        {
            var values = new AssignFileKeyParams();
            var result = MasterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
            VolumeWrapper.UploadFile(
                result.Url,
                result.FileId,
                "test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@checkFileExist")),
                null).GetAwaiter().GetResult();
            var status = VolumeWrapper.CheckFileExist(result.Url, result.FileId).GetAwaiter().GetResult();
            Assert.IsTrue(status);
        }

        [Test]
        public void GetFileStream()
        {
            var values = new AssignFileKeyParams();
            var result = MasterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
            VolumeWrapper.UploadFile(
                result.Url,
                result.FileId,
                "test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@getFileContent")),
                null).GetAwaiter().GetResult();
            var stream = VolumeWrapper.GetFileStream(result.Url, result.FileId).GetAwaiter().GetResult();
            using (var reader = new StreamReader(stream.OutputStream))
            {
                var text = reader.ReadToEnd();
                Assert.IsTrue(text.Equals("@getFileContent"));
            }
        }

        //[Test]
        //public void GetFileStatusHeader()
        //{
        //    var values = new AssignFileKeyParams();
        //    var result = _masterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
        //    _volumeWrapper.UploadFile(
        //        result.Url,
        //        result.FileId,
        //        "test.txt",
        //        new MemoryStream(Encoding.ASCII.GetBytes("@getFileStatusHeader")),
        //        null).GetAwaiter().GetResult();
        //    var headerResponse = _volumeWrapper.GetFileStatusHeader(result.Url, result.FileId).GetAwaiter().GetResult();
        //    Assert.IsTrue(headerResponse.GetLastHeader("Content-Length").Equals("20"));
        //}

        [Test]
        public void DeleteFile()
        {
            var values = new AssignFileKeyParams();
            var result = MasterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
            VolumeWrapper.UploadFile(
                result.Url,
                result.FileId,
                "test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@deleteFile")),
                null).GetAwaiter().GetResult();
            var fileExists = VolumeWrapper.CheckFileExist(result.Url, result.FileId).GetAwaiter().GetResult();
            VolumeWrapper.DeleteFile(result.Url, result.FileId).GetAwaiter().GetResult();
            var fileExists2 = VolumeWrapper.CheckFileExist(result.Url, result.FileId).GetAwaiter().GetResult();
            Assert.AreNotEqual(fileExists, fileExists2);
        }

        [Test]
        public void UploadFile()
        {
            var values = new AssignFileKeyParams();
            var result = MasterWrapper.AssignFileKey(values).GetAwaiter().GetResult();
            var size = VolumeWrapper.UploadFile(
                result.Url,
                result.FileId,
                "test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@uploadFile")),
                null).GetAwaiter().GetResult();
            Assert.IsTrue(size > 0);
        }
    }
}
