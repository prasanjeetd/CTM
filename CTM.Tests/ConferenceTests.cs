using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;

namespace CTM.Tests
{
    [TestClass]
    public class ConferenceTests
    {
        private readonly string _inputFile;

        public ConferenceTests()
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

        private void ProcessOutput(Conference conference,string outputPath)
        {
            OutputProcessor outputProcessor = new OutputProcessor(conference);
            var output = outputProcessor.Process();

            var outputFile = Path.Combine(_inputFile, outputPath);
            outputProcessor.SaveInFile(output, outputFile);
        }

        [TestMethod]
        public void TestConferenceDefaultInput()
        {
            var filePath = Path.Combine(_inputFile, "Input.txt");
            InputProcessor ip = new InputProcessor(filePath);
            var conference = ip.GetConference();
            conference.Schedule();
            Assert.AreEqual(conference.ScheduledTracks.Count, 2);

            Assert.AreEqual(conference.ScheduledTracks[0].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[1].Events.Count, 4);

            ProcessOutput(conference, "Output.txt");
        }

        [TestMethod]
        public void TestConferenceComplexInput1()
        {
            string commonPath =@"ConferenceTests\ComplexInput1\";
            var filePath = Path.Combine(_inputFile, commonPath+ "Input.txt");
            InputProcessor ip = new InputProcessor(filePath);
            var conference = ip.GetConference();
            conference.Schedule();
            Assert.AreEqual(conference.ScheduledTracks.Count, 2);

            Assert.AreEqual(conference.ScheduledTracks[0].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[1].Events.Count, 2);

            ProcessOutput(conference, commonPath + "Output.txt");
        }

        [TestMethod]
        public void TestConferenceComplexInput2()
        {
            string commonPath = @"ConferenceTests\ComplexInput2\";
            var filePath = Path.Combine(_inputFile, commonPath + "Input.txt");
            InputProcessor ip = new InputProcessor(filePath);
            var conference = ip.GetConference();
            conference.Schedule();
            Assert.AreEqual(conference.ScheduledTracks.Count, 2);

            Assert.AreEqual(conference.ScheduledTracks[0].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[1].Events.Count, 4);

            ProcessOutput(conference, commonPath + "Output.txt");
        }

        [TestMethod]
        public void TestConferenceUnevenInput()
        {
            string commonPath = @"ConferenceTests\UnevenInput\";
            var filePath = Path.Combine(_inputFile, commonPath + "Input.txt");
            InputProcessor ip = new InputProcessor(filePath);
            var conference = ip.GetConference();
            conference.Schedule();
            Assert.AreEqual(conference.ScheduledTracks.Count, 4);

            Assert.AreEqual(conference.ScheduledTracks[0].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[1].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[2].Events.Count, 4);
            Assert.AreEqual(conference.ScheduledTracks[3].Events.Count, 4);

            ProcessOutput(conference, commonPath + "Output.txt");
        }
    }
}
