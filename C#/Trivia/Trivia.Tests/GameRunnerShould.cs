using System;
using System.IO;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameRunnerShould
    {
        private TextWriter _output;

        [SetUp]
        public void SetUp()
        {
            var input = new StringReader(Environment.NewLine);
            Console.SetIn(input);

            _output = new StringWriter();
            Console.SetOut(_output);
        }

        [Test]
        public void Run_until_there_is_a_winner()
        {
            GameRunner.Main(new[] {"corrent"});

            var expectedOutput = File.ReadAllText(@"C:\Users\matteo.pozzi\Documents\GitHub\trivia\C#\Trivia\Trivia.Tests\trivia.txt");
            Assert.That(_output.ToString(), Is.EqualTo(expectedOutput));
        }
    }
}
