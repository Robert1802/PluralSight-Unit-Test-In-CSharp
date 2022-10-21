using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest : TestBase
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
        [DataRow(1, 1, DisplayName = "First Test (1,1)")]
        [DataRow(42, 42, DisplayName = "Second Test (42, 42)")]
        public void AreNumbersEqual(int num1, int num2)
        {
            Assert.AreEqual(num1, num2);
        }

        [TestMethod]
        [Description("Check to see if file exists.")]
        [Owner("RobertoMoura")]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();
            if (!string.IsNullOrEmpty(_GoodFileName)) {
                // Create the 'Good' file.
                File.AppendAllText(_GoodFileName, "Some Text");
            }

            TestContext.WriteLine(@"Checking File: " + _GoodFileName);

            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(3000)]
        public void SimulateTimeout()
        {
            // Always fails, because Sleep is 4000 and Timeout is 3000
            //System.Threading.Thread.Sleep(4000);
            // Always pass, because Sleep is 2000 and Timeout is 3000
            System.Threading.Thread.Sleep(2000);
        }

        [TestMethod]
        [Description("Check to see if file does not exists.")]
        [Owner("RobertoMoura")]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine(@"Checking File: " + BAD_FILE_NAME);

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [Description("Check for thrown ArgumentNullException using ExpectedException.")]
        [Owner("RobertoMoura")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null File");

            fp.FileExists("");
        }

        [TestMethod]
        [Description("Check for a thrown ArgumentNullException using try...catch.")]
        [Owner("RobertoMoura")]
        public void FileNameNullOrEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                TestContext.WriteLine("Checking for a null File");

                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                // Test was a success
                return;
            }

            // Fail the test
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException.");
        }
    }
}
