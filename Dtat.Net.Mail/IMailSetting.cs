namespace Dtat.Net.Mail
{
	public interface IMailSetting
	{
		int SmtpClientTimeout { get; set; }

		int SmtpClientPortNumber { get; set; }

		bool SmtpClientSslEnabled { get; set; }

		string SmtpClientHostAddress { get; set; }



		string SmtpUsername { get; set; }

		string SmtpPassword { get; set; }

		bool UseDefaultCredentials { get; set; }



		string SenderDisplayName { get; set; }

		string SenderEmailAddress { get; set; }



		string SupportDisplayName { get; set; }

		string SupportEmailAddress { get; set; }



		string BccAddresses { get; set; }

		string EmailSubjectTemplate { get; set; }



		void FixValues();
	}
}
