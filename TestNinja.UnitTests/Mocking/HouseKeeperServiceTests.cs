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

		[SetUp]
		public void SetUp()
		{
			_housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

			var unitOfWork = new Mock<IUnitOfWork>();
			unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
			{
				_housekeeper
			}.AsQueryable());

			_statementGenerator = new Mock<IStatementGenerator>();
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
	}
}
