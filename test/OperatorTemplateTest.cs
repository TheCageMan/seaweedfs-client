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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace smartbox.SeaweedFs.Client.Test
{
    /// <summary>
    /// Summary description for FileTemplateTest
    /// </summary>
    [TestFixture]
    public class OperatorTemplateTest : BaseFixture
    {        
        [Test]
        public void GetFileStream()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@getFileStream"))).GetAwaiter().GetResult();
            var output = Template.GetFileStream(status.FileId).GetAwaiter().GetResult();
            using (var reader = new StreamReader(output.OutputStream))
            {
                var text = reader.ReadToEnd();
                Assert.IsTrue(text.Equals("@getFileStream"));
            }
        }

        [Test]
        public void GetFileStatus()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@getFileStatusHeader"))).GetAwaiter().GetResult();            
            var output = Template.GetFileStatus(status.FileId).GetAwaiter().GetResult();
            Assert.IsTrue(output.Filename.Equals("test.txt"));
        }

        [Test]
        public void GetFileUrl()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@getFileUrl"))).GetAwaiter().GetResult();
            var output = Template.GetFileUrl(status.FileId).GetAwaiter().GetResult();
            Assert.IsTrue(output.EndsWith(status.FileId));
        }

        [Test]
        public void SaveFileByStream()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@saveFileByStream"))).GetAwaiter().GetResult();
            Assert.IsTrue(status.Size > 0);
        }

        [Test]
        public void SaveFileByStreamMap()
        {
            var fileMap = new Dictionary<string, Stream>(3)
            {
                {"test_1.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_1"))},
                {"test_2.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_2"))},
                {"test_3.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_3"))}
            };
            var statusMap = Template.SaveFilesByStreamMap(fileMap).GetAwaiter().GetResult();
            foreach (var status in statusMap)
            {
                Assert.IsTrue(status.Value.Size > 0);
            }
        }

        [Test]
        public void DeleteFile()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@deleteFile"))).GetAwaiter().GetResult();
            var result = Template.DeleteFile(status.FileId).GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteFiles()
        {
            var fileMap = new Dictionary<string, Stream>(3)
            {
                {"test_1.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_1"))},
                {"test_2.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_2"))},
                {"test_3.txt", new MemoryStream(Encoding.ASCII.GetBytes("@saveFilesByStreamMap_3"))}
            };
            var statusMap = Template.SaveFilesByStreamMap(fileMap).GetAwaiter().GetResult();
            var fIds = new List<string>(statusMap.Count);
            fIds.AddRange(statusMap.Select(status => status.Value.FileId));
            Template.DeleteFiles(fIds).GetAwaiter().GetResult();
            foreach (var status in statusMap)
            {
                var result = Template.CheckFileExists(status.Value.FileId).GetAwaiter().GetResult();
                Assert.IsTrue(result == false);
            }
        }

        [Test]
        public void UpdateFileByStream()
        {
            var status = Template.SaveFileByStream("test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@saveFileByStream"))).GetAwaiter().GetResult();
            var updateStatus = Template.UpdateFileByStream(status.FileId, "test.txt",
                new MemoryStream(Encoding.ASCII.GetBytes("@updateFileByStream"))).GetAwaiter().GetResult();
            var output = Template.GetFileStream(status.FileId).GetAwaiter().GetResult();
            using (var reader = new StreamReader(output.OutputStream))
            {
                var text = reader.ReadToEnd();
                Assert.IsTrue(text.Equals("@updateFileByStream"));
            }
        }
    }
}
