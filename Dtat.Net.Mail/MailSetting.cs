namespace Dtat.Net.Mail
{
	[System.Serializable]
	public class MailSetting : object, IMailSetting
	{
		public const string KeyName = "MailSetting";

		public MailSetting() : base()
		{
			Enabled = true;
			SmtpClientPortNumber = 25;
			SmtpClientTimeout = 100_000;
		}

		public bool Enabled { get; set; }



		public int SmtpClientTimeout { get; set; }

		public int SmtpClientPortNumber { get; set; }

		public bool SmtpClientSslEnabled { get; set; }

		public string SmtpClientHostAddress { get; set; }



		public string SmtpUsername { get; set; }

		public string SmtpPassword { get; set; }

		public bool UseDefaultCredentials { get; set; }



		public string SenderDisplayName { get; set; }

		public string SenderEmailAddress { get; set; }



		public string SupportDisplayName { get; set; }

		public string SupportEmailAddress { get; set; }



		public string BccAddresses { get; set; }

		public string EmailSubjectTemplate { get; set; }

		/// <summary>
		/// اصلاح تمام فیلدهای متنی تنظیمات ایمیل
		/// </summary>
		/// <param name="mailSetting">تنظیمات ایمیل</param>
		public void FixValues()
		{
			SenderDisplayName = String.Fix(SenderDisplayName);
			SenderEmailAddress = String.Fix(SenderEmailAddress);

			SupportDisplayName = String.Fix(SupportDisplayName);
			SupportEmailAddress = String.Fix(SupportEmailAddress);

			BccAddresses = String.Fix(BccAddresses);
			EmailSubjectTemplate = String.Fix(EmailSubjectTemplate);

			SmtpPassword = String.Fix(SmtpPassword);
			SmtpUsername = String.Fix(SmtpUsername);
			SmtpClientHostAddress = String.Fix(SmtpClientHostAddress);
		}
	}
}
