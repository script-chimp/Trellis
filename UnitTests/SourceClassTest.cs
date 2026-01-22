using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Trellis.Core;

namespace UnitTests
{
    internal class SourceClassTest
    {
        private Passage TestPassage;

        [Test]
        public void TestPassageCreation_Success()
        {
            // Arrange
            string passageName = "TestPassage";
            List<PassageStep> steps = new List<PassageStep>
            {
                new PassageStep(PassageStepType.display, "null"),
                new PassageStep(PassageStepType.macro, "Step 2")
            };
            List<PassageLink> links = new List<PassageLink>();
            // Act
            TestPassage = new Passage(passageName, steps, links);
            // Assert
            Assert.That(passageName == TestPassage.Name);
            Assert.That(steps.Count == TestPassage.Steps.Count);
            Assert.That(steps[0].Value == TestPassage.Steps[0].Value);
            Assert.That(steps[1].Value == TestPassage.Steps[1].Value);
        }

        [Test]
        public void TestTest()
        {
            Assert.That(true);
        }
}
}
