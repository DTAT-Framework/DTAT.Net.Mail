namespace Dtat.Net.Mail.Tests
{
	public class UnitTest1 : object
	{
		public UnitTest1() : base()
		{
		}

		[Xunit.Fact]
		public void Test01()
		{
			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: null,
				body: null,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: null
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "mailSetting is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test02()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting();

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: null,
				body: null,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SenderEmailAddress is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test03()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SenderEmailAddress = "DariushT@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: null,
				body: null,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SupportEmailAddress is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test04()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: null,
				body: null,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "Subject is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test05()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: null,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "Body is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test06()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: "Body 1",
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SmtpClientHostAddress is null!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test07()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SmtpClientPortNumber = 0,
					SmtpClientHostAddress = "smtp.gmail.com",
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: "Body 1",
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SmtpClientPortNumber should be greater than zero!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test08()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SmtpClientTimeout = -1,
					SmtpClientPortNumber = 1,
					SmtpClientHostAddress = "smtp.gmail.com",
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: "Body 1",
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SmtpClientTimeout should be greater than or equal to zero!", actual: result.Errors[0]);
		}

		[Xunit.Fact]
		public void Test09()
		{
			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					SmtpClientPortNumber = 465,
					SmtpClientHostAddress = "smtp.gmail.com",
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: "Body 1",
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.True(condition: result.IsFailed);
			Xunit.Assert.False(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 1, actual: result.Errors.Count);
			Xunit.Assert.Equal(expected: "SmtpUsername is null!", actual: result.Errors[0]);
		}



		[Xunit.Fact]
		public void Test20()
		{
			string password = "[YOUR_GMAIL_PASSWORD]";

			var mailSetting =
				new Dtat.Net.Mail.MailSetting
				{
					BccAddresses = null,
					SenderDisplayName = null,
					SupportDisplayName = null,
					EmailSubjectTemplate = null,
					UseDefaultCredentials = false,

					SmtpPassword = password,
					SmtpClientTimeout = 20000,
					SmtpClientPortNumber = 587,
					SmtpClientSslEnabled = true,
					SmtpUsername = "dariusht@gmail.com",
					SmtpClientHostAddress = "smtp.gmail.com",
					SenderEmailAddress = "DariushT@GMail.com",
					SupportEmailAddress = "Dariush.Tasdighi@GMail.com",
				};

			// Note:
			// https://stackoverflow.com/questions/20906077/gmail-error-the-smtp-server-requires-a-secure-connection-or-the-client-was-not

			var result =
				Utility.Send(
				sender: null,
				recipients: null,
				subject: "Subject 1",
				body: "Body 1",
				priority: System.Net.Mail.MailPriority.Normal,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting
				);

			Xunit.Assert.False(condition: result.IsFailed);
			Xunit.Assert.True(condition: result.IsSuccess);
			Xunit.Assert.Equal(expected: 0, actual: result.Errors.Count);
		}
	}
}
