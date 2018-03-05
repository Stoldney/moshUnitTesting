using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	internal class StackTests
	{
		private Stack<string> _stack;

		[SetUp]
		public void SetUp()
		{
			_stack = new Stack<string>();
		}

		[Test]
		public void Push_ArgumentIsNull_ThrowArgumentNullException()
		{
			Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
		}

		[Test]
		public void Push_ValidArgument_AddArgumentToStack()
		{
			_stack.Push("a");

			Assert.That(_stack.Count, Is.EqualTo(1));
		}

		[Test]
		public void Count_EmptyStack_ReturnZero()
		{
			Assert.That(_stack.Count, Is.EqualTo(0));
		}

		[Test]
		public void Pop_EmptyStack_ThrowInvalidOperationException()
		{
			Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Pop_StackWithFewObjects_ReturnsObjectOnTheTop()
		{
			_stack.Push("1");
			_stack.Push("2");
			_stack.Push("3");

			Assert.That(_stack.Pop(), Is.EqualTo("3"));
		}

		[Test]
		public void Pop_StackWithFewObjects_RemovesObjectOnTheTop()
		{
			_stack.Push("1");
			_stack.Push("2");
			_stack.Push("3");
			_stack.Pop();
			
			Assert.That(_stack.Count, Is.EqualTo(2));
		}

		[Test]
		public void Peek_EmptyStack_ThrowInvalidOperationException()
		{
			Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
		}

		[Test]
		public void Peek_StackWithElements_ReturnObjectOnTop()
		{
			_stack.Push("1");
			_stack.Push("2");
			_stack.Push("3");

			var result = _stack.Peek();

			Assert.That(result, Is.EqualTo("3"));
		}

		[Test]
		public void Peek_StackWithElements_DoesNotRemoveAnyObjects()
		{
			_stack.Push("1");
			_stack.Push("2");
			_stack.Push("3");

			_stack.Peek();

			Assert.That(_stack.Count, Is.EqualTo(3));
		}
	}
}
