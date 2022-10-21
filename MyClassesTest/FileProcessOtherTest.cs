using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessOtherTest : TestBase
    {
        private const string BAD_FILE_NAME = @"D:\ArquivoQueNaoExiste.jpg";

        [ClassInitialize()]
        public static void ClassInitialize(TestContext tc)
        {
            // TODO: Initialize for all tests in class
            tc.WriteLine("In ClassInitialize() method");
        }

        [ClassCleanup()]
        public static void CLassCleanup()
        {
            // TODO: Clean up after all tests in class
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("In TestInitialize() Method");

            WriteDescription(this.GetType());

            if (TestContext.TestName.StartsWith("FileNameDoesExist")){
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName)) {
                    TestContext.WriteLine("Creating file: " + _GoodFileName);
                    // Create the 'Good' file.
                    File.AppendAllText(_GoodFileName, "Some text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("In TestCleanup() method");

            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                // Delete file
                if (File.Exists(_GoodFileName)) {
                    TestContext.WriteLine("Deleting file: " + _GoodFileName);
                    File.Delete(_GoodFileName);
                }
            }
        }

        [TestMethod]
        public void FileNameDoesExistSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(_GoodFileName);

            //Assert.IsFalse(fromCall, "File " + _GoodFileName + " Does Not Exist.");
            Assert.IsFalse(fromCall, "File {0} Does Not Exist.", _GoodFileName);
        }

        [TestMethod]
        public void AreEqualTest()
        {
            string str1 = "Roberto";
            string str2 = "rObErTo";

            // Two strings are the same because it's not case sensitive
            Assert.AreEqual(str1, str2, true);
        }

        [TestMethod]
        public void AreNotEqualTest()
        {
            string str1 = "Roberto";
            string str2 = "Moura";

            // Two strings are different
            Assert.AreNotEqual(str1, str2);
        }

        [TestMethod]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            // Fails because they dont both point to the same object
            Assert.AreSame(x, y);
        }

        [TestMethod]
        public void AreSameIndeedTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            // Success because they point to the same object
            Assert.AreSame(x, y);
        }

        [TestMethod]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            // Fails because they dont both point to the same object
            Assert.AreNotSame(x, y);
        }

    }
}
