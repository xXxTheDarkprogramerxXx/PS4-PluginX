using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Assets.Wrapper
{
    public class Util
    {
        /// <summary>
        /// Use this to hide the loading dialog 
        /// </summary>
        /// <returns></returns>
        [DllImport("universal")]
        public static extern int HideDialog();

        /// <summary>
        /// This shows a message dialog with an ok button on the screen
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        [DllImport("universal")]
        public static extern int ShowMessageDialog(string Message);

        /// <summary>
        /// If you call this you need to remember to terminate the dialogbox somehow
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        [DllImport("universal")]
        public static extern int ShowLoadingDialog(string Message);

        [DllImport("universal")]
        public static extern int ShowMessageYesNoDialog(string Message);


        [DllImport("universal")]
        public static extern int SendMessageToPS4(string Message);

    }

    public class FileUtil
    {
        public static bool FilesContentsAreEqual(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            bool result;

            if (fileInfo1.Length != fileInfo2.Length)
            {
                result = false;
            }
            else
            {
                using (var file1 = fileInfo1.OpenRead())
                {
                    using (var file2 = fileInfo2.OpenRead())
                    {
                        result = StreamsContentsAreEqual(file1, file2);
                    }
                }
            }

            return result;
        }

        private static bool StreamsContentsAreEqual(Stream stream1, Stream stream2)
        {
            const int bufferSize = 1024 * sizeof(Int64);
            var buffer1 = new byte[bufferSize];
            var buffer2 = new byte[bufferSize];

            while (true)
            {
                int count1 = stream1.Read(buffer1, 0, bufferSize);
                int count2 = stream2.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                {
                    return false;
                }

                if (count1 == 0)
                {
                    return true;
                }

                int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
                for (int i = 0; i < iterations; i++)
                {
                    if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) != BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
                    {
                        return false;
                    }
                }
            }
        }
    }

}
