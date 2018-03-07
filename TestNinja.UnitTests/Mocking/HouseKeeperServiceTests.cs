using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	[Category("Mocking")]
	internal class HouseKeeperServiceTests
	{
		private HouseKeeperService _service;
		private Mock<IStatementGenerator> _statementGenerator;
		private Mock<IEmailSender> _emailSender;
		private Mock<IXtraMessageBox> _messageBox;
		private Housekeeper _housekeeper;
		private string _statementFileName;

		[SetUp]
		public void SetUp()
		{
			_housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

			var unitOfWork = new Mock<IUnitOfWork>();
			unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
			{
				_housekeeper
			}.AsQueryable());

			_statementFileName = "filename";
			_statementGenerator = new Mock<IStatementGenerator>();
			_statementGenerator
				.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, DateTime.Today))
				.Returns(() => _statementFileName);


			_emailSender = new Mock<IEmailSender>();
			_messageBox = new Mock<IXtraMessageBox>();

			_service = new HouseKeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			_service.SendStatementEmails(DateTime.Today);

			_statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, DateTime.Today));
		}

		[Test]
		public void SendStatementEmails_HousekeeperEmailIsNull_ShouldNotGenerateStatement()
		{
			_housekeeper.Email = null;
			_service.SendStatementEmails(DateTime.Today);

			_statementGenerator.Verify(
				sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, DateTime.Today),
				Times.Never);
		}

		[Test]
		public void SendStatementEmails_HousekeeperEmailIsWhitespace_ShouldNotGenerateStatement()
		{
			_housekeeper.Email = " ";
			_service.SendStatementEmails(DateTime.Today);

			_statementGenerator.Verify(
				sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, DateTime.Today),
				Times.Never);
		}

		[Test]
		public void SendStatementEmails_HousekeeperEmailIsEmpty_ShouldNotGenerateStatement()
		{
			_housekeeper.Email = "";
			_service.SendStatementEmails(DateTime.Today);

			_statementGenerator.Verify(
				sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, DateTime.Today),
				Times.Never);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_ShouldEmailStatement()
		{
			_service.SendStatementEmails(DateTime.Today);

			VerifyEmailWasSent();
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailStatement()
		{
			_statementFileName = null;

			_service.SendStatementEmails(DateTime.Today);

			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsEmpty_ShouldNotEmailStatement()
		{
			_statementFileName = string.Empty;

			_service.SendStatementEmails(DateTime.Today);

			VerifyEmailNotSent();			
		}

		[Test]
		public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailStatement()
		{
			_statementFileName = " ";

			_service.SendStatementEmails(DateTime.Today);

			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
		{
			_emailSender.Setup(es => es.EmailFile(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>()
				)).Throws<Exception>();

			_service.SendStatementEmails(DateTime.Today);

			_messageBox.Verify(mb => mb.Show(
				It.IsAny<string>(),
				It.IsAny<string>(),
				MessageBoxButtons.OK));
		}

		private void VerifyEmailNotSent()
		{
			_emailSender
				.Verify(
					es => es.EmailFile(
						It.IsAny<string>(),
						It.IsAny<string>(),
						It.IsAny<string>(),
						It.IsAny<string>()),
					Times.Never);
		}

		private void VerifyEmailWasSent()
		{
			_emailSender.Verify(
				es => es.EmailFile(
					_housekeeper.Email,
					_housekeeper.StatementEmailBody,
					_statementFileName,
					It.IsAny<string>()));
		}
	}
}
