using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CTM.Tests
{
    [TestClass]
    public class InputProcessorTests
    {
        private string _inputFile;

        public InputProcessorTests()
        {
            _inputFile = Path.Combine(
                            Path.GetDirectoryName(
                            Path.GetDirectoryName(
                            Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))),
                            @"InputOutput");

        }

        private TestContext testContextInstance;
       
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Invalid Time or Unit")]
        public void TestInputFileForInvalidUnit()
        {
            try
            {
                var filePath = Path.Combine(_inputFile, "InputTests\\InvalidTime.txt");
                InputProcessor ip = new InputProcessor(filePath);
                var conference = ip.GetConference();

                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                if (ex.Message != ExceptionMessage.InvalidTime)
                    Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestInputFileForTitleContainsNumber()
        {
            try
            {
                var filePath = Path.Combine(_inputFile, "InputTests\\TitleContainsNumber.txt");
                InputProcessor ip = new InputProcessor(filePath);
                var conference = ip.GetConference();

                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                if (ex.Message != ExceptionMessage.TitleContainsNumber)
                    Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestInputFileForTalkDurationExceedsMaxSessionDuration()
        {
            try
            {
                var filePath = Path.Combine(_inputFile, "InputTests\\TalkDurationExceedsMaxSessionDuration.txt");
                InputProcessor ip = new InputProcessor(filePath);
                var conference = ip.GetConference();

                Assert.Fail("No Exception thrown");
            }
            catch (Exception ex)
            {
                if (ex.Message != ExceptionMessage.TalkDurationExceedsMaxEventDuration +"240")
                    Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestInputFileForValidInput()
        {

            var filePath = Path.Combine(_inputFile, "Input.txt");
            InputProcessor ip = new InputProcessor(filePath);
            var conference = ip.GetConference();

        }
    }

}
