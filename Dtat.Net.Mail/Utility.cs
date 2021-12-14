using System.Linq;

namespace Dtat.Net.Mail
{
	public static class Utility
	{
		static Utility()
		{
		}

		/// <summary>
		/// تبدیل متن نامه به گونه‌ای که برای متن ایمیل مناسب باشد
		/// </summary>
		/// <param name="text">متن نامه</param>
		/// <returns></returns>
		public static string CompatibleTextForMailBody(string text)
		{
			if (text == null)
			{
				return string.Empty;
			}

			text =
				text
				.Replace(System.Convert.ToChar(13).ToString(), "<br />") // Return Key.
				.Replace(System.Convert.ToChar(10).ToString(), string.Empty) // Return Key.
				.Replace(System.Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;"); // TAB Key.

			return text;
		}

		/// <summary>
		/// ارسال نامه
		/// </summary>
		/// <param name="subject">موضوع</param>
		/// <param name="body">متن</param>
		/// <param name="mailSetting">تنظیمات</param>
		/// <returns></returns>
		public static Results.Result Send
			(
				string subject,
				string body,
				IMailSetting mailSetting = null
			)
		{
			var result =
				Send(sender: null,
				recipients: null,
				subject: subject,
				body: body,
				priority: System.Net.Mail.MailPriority.High,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting);

			return result;
		}

		/// <summary>
		/// ارسال نامه
		/// </summary>
		/// <param name="recipient">دریافت کننده</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">متن</param>
		/// <param name="priority">اهمیت</param>
		/// <param name="mailSetting">تنظیمات</param>
		/// <returns></returns>
		public static Results.Result Send
			(
				System.Net.Mail.MailAddress recipient,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				IMailSetting mailSetting = null
			)
		{
			// **************************************************
			var recipients =
				new System.Net.Mail.MailAddressCollection();

			if (recipient != null)
			{
				recipients.Add(recipient);
			}
			// **************************************************

			var result =
				Send(sender: null,
				recipients: recipients,
				subject: subject,
				body: body,
				priority: priority,
				attachmentPathNames: null,
				deliveryNotification: System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSetting: mailSetting);

			return result;
		}

		/// <summary>
		/// ارسال نامه
		/// </summary>
		/// <param name="sender">فرستنده</param>
		/// <param name="recipients">گیرندگان</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">متن</param>
		/// <param name="priority">اهمیت</param>
		/// <param name="attachmentPathNames">فایل‌های الصاقی</param>
		/// <param name="deliveryNotification">اطلاع‌رسانی از ارسال</param>
		/// <param name="mailSetting">تنظیمات</param>
		/// <returns></returns>
		public static Results.Result Send
			(
				System.Net.Mail.MailAddress sender,
				System.Net.Mail.MailAddressCollection recipients,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				System.Collections.Generic.List<string> attachmentPathNames,
				System.Net.Mail.DeliveryNotificationOptions deliveryNotification,
				IMailSetting mailSetting = null
			)
		{
			// **************************************************
			var result =
				new Results.Result();
			// **************************************************

			// **************************************************
			System.Net.Mail.SmtpClient smtpClient = null;
			System.Net.Mail.MailMessage mailMessage = null;
			// **************************************************

			try
			{
				// **************************************************
				if (mailSetting == null)
				{
					string errorMessage =
						$"{nameof(mailSetting)} is null!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}
				else
				{
					if(mailSetting.Enabled == false)
					{
						result.AddErrorMessage
							(message: "Mail setting is disabled!");

						return result;
					}
				}

				mailSetting.FixValues();
				// **************************************************

				// **************************************************
				// *** Mail Message Configuration *******************
				// **************************************************
				mailMessage =
					new System.Net.Mail.MailMessage();

				// **************************************************
				mailMessage.To.Clear();
				mailMessage.CC.Clear();
				mailMessage.Bcc.Clear();
				mailMessage.Attachments.Clear();
				mailMessage.ReplyToList.Clear();
				// **************************************************

				// **************************************************
				if (sender == null)
				{
					if (string.IsNullOrWhiteSpace(mailSetting.SenderEmailAddress))
					{
						string errorMessage =
							$"{nameof(mailSetting.SenderEmailAddress)} is null!";

						result.AddErrorMessage(message: errorMessage);

						return result;
					}

					if (string.IsNullOrEmpty(mailSetting.SenderDisplayName))
					{
						sender =
							new System.Net.Mail.MailAddress
								(address: mailSetting.SenderEmailAddress,
								displayName: mailSetting.SenderEmailAddress,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
					else
					{
						sender =
							new System.Net.Mail.MailAddress
								(address: mailSetting.SenderEmailAddress,
								displayName: mailSetting.SenderDisplayName,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
				}

				mailMessage.From = sender;
				mailMessage.Sender = sender;

				// Note: Below Code Obsoleted!
				//mailMessage.ReplyTo = sender;

				mailMessage.ReplyToList.Add(sender);
				// **************************************************

				// **************************************************
				if (recipients != null)
				{
					// Note: Wrong Usage!
					//mailMessage.To = recipients;

					foreach (var mailAddress in recipients)
					{
						mailMessage.To.Add(mailAddress);
					}
				}
				else
				{
					if (string.IsNullOrWhiteSpace(mailSetting.SupportEmailAddress))
					{
						string errorMessage =
							$"{nameof(mailSetting.SupportEmailAddress)} is null!";

						result.AddErrorMessage(message: errorMessage);

						return result;
					}

					System.Net.Mail.MailAddress mailAddress = null;

					if (string.IsNullOrEmpty(mailSetting.SupportDisplayName))
					{
						mailAddress =
							new System.Net.Mail.MailAddress
								(address: mailSetting.SupportEmailAddress,
								displayName: mailSetting.SupportEmailAddress,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
					else
					{
						mailAddress =
							new System.Net.Mail.MailAddress
								(address: mailSetting.SupportEmailAddress,
								displayName: mailSetting.SupportDisplayName,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}

					mailMessage.To.Add(mailAddress);
				}
				// **************************************************

				// **************************************************
				if (string.IsNullOrEmpty(mailSetting.BccAddresses) == false)
				{
					mailSetting.BccAddresses =
						mailSetting.BccAddresses
						.Replace(" ", ",")
						.Replace(";", ",")
						.Replace("|", ",")
						.Replace("،", ",");

					while (mailSetting.BccAddresses.Contains(",,"))
					{
						mailSetting.BccAddresses =
							mailSetting.BccAddresses.Replace(",,", ",");
					}

					var bccAddresses =
						mailSetting.BccAddresses.Split(',').Distinct();

					foreach (var bccAddress in bccAddresses)
					{
						bool found =
							mailMessage.To
							.Where(current => string.Compare(current.Address, bccAddress, true) == 0)
							.Any();

						if (found == false)
						{
							var mailAddress =
								new System.Net.Mail.MailAddress(address: bccAddress);

							mailMessage.Bcc.Add(item: mailAddress);
						}
					}

					// Note: [BccAddresses] must be separated with comma character (",")
					//mailMessage.Bcc.Add(mailSetting.BccAddresses);
				}
				// **************************************************

				// **************************************************
				if (string.IsNullOrEmpty(subject))
				{
					string errorMessage = $"Subject is null!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}

				mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

				if (string.IsNullOrEmpty(mailSetting.EmailSubjectTemplate))
				{
					mailMessage.Subject = subject;
				}
				else
				{
					mailMessage.Subject =
						string.Format(mailSetting.EmailSubjectTemplate, subject);
				}
				// **************************************************

				// **************************************************
				if (string.IsNullOrEmpty(body))
				{
					string errorMessage = $"Body is null!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}

				mailMessage.Body = body;
				mailMessage.IsBodyHtml = true;
				mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				mailMessage.Priority = priority;
				mailMessage.DeliveryNotificationOptions = deliveryNotification;
				// **************************************************

				// **************************************************
				if ((attachmentPathNames != null) && (attachmentPathNames.Count > 0))
				{
					foreach (var attachmentPathName in attachmentPathNames)
					{
						if (System.IO.File.Exists(attachmentPathName))
						{
							var attachment =
								new System.Net.Mail.Attachment(attachmentPathName);

							mailMessage.Attachments.Add(attachment);
						}
					}
				}
				// **************************************************

				// **************************************************
				mailMessage.Headers.Add("Dtat.Net.Mail_Version", "5.1.0");
				mailMessage.Headers.Add("Dtat.Net.Mail_Url", "https://DTAT.ir");
				mailMessage.Headers.Add("Dtat.Net.Mail_Author", "Mr. Dariush Tasdighi");
				mailMessage.Headers.Add("Dtat.Net.Mail_Date", "1400/06/30 - 2021/09/21");
				// **************************************************
				// *** /Mail Message Configuration ******************
				// **************************************************

				// **************************************************
				// *** Smtp Client Configuration ********************
				// **************************************************
				if (string.IsNullOrEmpty(mailSetting.SmtpClientHostAddress))
				{
					string errorMessage =
						$"{nameof(mailSetting.SmtpClientHostAddress)} is null!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}

				if (mailSetting.SmtpClientPortNumber <= 0)
				{
					string errorMessage =
						$"{nameof(mailSetting.SmtpClientPortNumber)} should be greater than zero!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}

				if (mailSetting.SmtpClientTimeout < 0)
				{
					string errorMessage =
						$"{nameof(mailSetting.SmtpClientTimeout)} should be greater than or equal to zero!";

					result.AddErrorMessage(message: errorMessage);

					return result;
				}

				smtpClient =
					new System.Net.Mail.SmtpClient
					{
						Timeout =
							mailSetting.SmtpClientTimeout,

						Port =
							mailSetting.SmtpClientPortNumber,

						EnableSsl =
							mailSetting.SmtpClientSslEnabled,

						Host =
							mailSetting.SmtpClientHostAddress,

						UseDefaultCredentials =
							mailSetting.UseDefaultCredentials,

						DeliveryMethod =
							System.Net.Mail.SmtpDeliveryMethod.Network,
					};

				smtpClient.DeliveryMethod =
					System.Net.Mail.SmtpDeliveryMethod.Network;

				if (mailSetting.UseDefaultCredentials == false)
				{
					if (string.IsNullOrEmpty(mailSetting.SmtpUsername))
					{
						string errorMessage =
							$"{nameof(mailSetting.SmtpUsername)} is null!";

						result.AddErrorMessage(message: errorMessage);

						return result;
					}

					// Note: SmtpPassword can be null!

					var networkCredential =
						new System.Net.NetworkCredential
							(userName: mailSetting.SmtpUsername, password: mailSetting.SmtpPassword);

					smtpClient.Credentials = networkCredential;
				}
				// **************************************************
				// *** /Smtp Client Configuration *******************
				// **************************************************

				smtpClient.Send(message: mailMessage);
			}
			catch (System.Exception ex)
			{
				result.AddErrorMessage(message: ex.Message);
			}
			finally
			{
				if (mailMessage != null)
				{
					mailMessage.Dispose();
					mailMessage = null;
				}

				if (smtpClient != null)
				{
					smtpClient.Dispose();
					smtpClient = null;
				}
			}

			return result;
		}
	}
}
