using System;

#if FAKES
using FakeItEasy;
#endif

#if !OLD_MSTEST
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace FluentAssertions.Specs
{
    [TestClass]
    public class ThrowAssertionsSpecs
    {
#if FAKES
        [TestMethod]
        public void When_subject_throws_expected_exception_it_should_not_do_anything()
        {
            IFoo testSubject = A.Fake<IFoo>();
            A.CallTo(() => testSubject.Do()).Throws(new InvalidOperationException());

            testSubject.Invoking(x => x.Do()).ShouldThrow<InvalidOperationException>();
        }
#endif

        [TestMethod]
        public void When_action_throws_expected_exception_it_should_not_do_anything()
        {
            var act = new Action(() => { throw new InvalidOperationException("Some exception"); });

            act.ShouldThrow<InvalidOperationException>();
        }

#if FAKES
        [TestMethod]
        public void When_subject_does_not_throw_exception_but_one_was_expected_it_should_throw_with_clear_description()
        {
            try
            {
                IFoo testSubject = A.Fake<IFoo>();

                testSubject.Invoking(x => x.Do()).ShouldThrow<Exception>();

                Assert.Fail("ShouldThrow() dit not throw");
            }
            catch (AssertFailedException ex)
            {
                ex.Message.Should().Be(
                    "Expected a <System.Exception> to be thrown, but no exception was thrown.");
            }
        }
#endif

        [TestMethod]
        public void When_action_does_not_throw_exception_but_one_was_expected_it_should_throw_with_clear_description()
        {
            try
            {
                var act = new Action(() => { });

                act.ShouldThrow<Exception>();

                Assert.Fail("ShouldThrow() dit not throw");
            }
            catch (AssertFailedException ex)
            {
                ex.Message.Should().Be(
                    "Expected a <System.Exception> to be thrown, but no exception was thrown.");
            }
        }

#if FAKES
        public interface IFoo
        {
            void Do();
        }

        [TestMethod]
        public void When_Project_IFooLambda_subject_throws_generic_expected_exception_with_an_expected_message_it_should_not_arrest_when_debugged()
        {
            var testSubject = A.Fake<IFoo>();
            A.CallTo(() => testSubject.Do()).Throws(new InvalidOperationException("some message"));

            Action act = testSubject.Do;

            act.ShouldThrow<InvalidOperationException>().WithMessage("some message");
        }

        [TestMethod]
        public void When_Project_IFooMethodGroup_subject_throws_generic_expected_exception_with_an_expected_message_it_should_not_arrest_when_debugged()
        {
            var testSubject = A.Fake<IFoo>();
            A.CallTo(() => testSubject.Do()).Throws(new InvalidOperationException("some message"));

            Action act = () => testSubject.Do();

            act.ShouldThrow<InvalidOperationException>().WithMessage("some message");
        }
#endif

        public class Foo
        {
            public void Do()
            {
                throw new InvalidOperationException("some message");
            }
        }

        [TestMethod]
        public void When_Project_FooLambda_subject_throws_generic_expected_exception_with_an_expected_message_it_should_not_arrest_when_debugged()
        {
            var testSubject = new Foo();

            Action act = testSubject.Do;

            act.ShouldThrow<InvalidOperationException>().WithMessage("some message");
        }

        [TestMethod]
        public void When_Project_FooMethodGroup_subject_throws_generic_expected_exception_with_an_expected_message_it_should_not_arrest_when_debugged()
        {
            var testSubject = new Foo();

            Action act = () => testSubject.Do();

            act.ShouldThrow<InvalidOperationException>().WithMessage("some message");
        }
    }
}
