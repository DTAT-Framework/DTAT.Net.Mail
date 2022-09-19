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

		public string? SmtpClientHostAddress { get; set; }



		public string? SmtpUsername { get; set; }

		public string? SmtpPassword { get; set; }

		public bool UseDefaultCredentials { get; set; }



		public string? SenderDisplayName { get; set; }

		public string? SenderEmailAddress { get; set; }



		public string? SupportDisplayName { get; set; }

		public string? SupportEmailAddress { get; set; }



		public string? BccAddresses { get; set; }

		public string? EmailSubjectTemplate { get; set; }

		/// <summary>
		/// اصلاح تمام فیلدهای متنی تنظیمات ایمیل
		/// </summary>
		/// <param name="mailSetting">تنظیمات ایمیل</param>
		public void FixValues()
		{
			SenderDisplayName =
				String.Fix(text: SenderDisplayName);

			SenderEmailAddress =
				String.Fix(text: SenderEmailAddress);

			SupportDisplayName =
				String.Fix(text: SupportDisplayName);

			SupportEmailAddress =
				String.Fix(text: SupportEmailAddress);

			BccAddresses =
				String.Fix(text: BccAddresses);

			EmailSubjectTemplate =
				String.Fix(text: EmailSubjectTemplate);

			SmtpPassword =
				String.Fix(text: SmtpPassword);

			SmtpUsername =
				String.Fix(text: SmtpUsername);

			SmtpClientHostAddress =
				String.Fix(text: SmtpClientHostAddress);
		}
	}
}
