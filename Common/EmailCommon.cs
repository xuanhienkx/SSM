using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using DotNetOpenAuth.Messaging;
using SSM.Services;
using SSM.ViewModels;

namespace SSM.Common
{
    public  class EmailCommon
    {
        public EmailModel EmailModel { get; set; } 
        public bool SendEmail(out string msg, bool isUserSend = false)
        {
            try
            {
                var isUserSendEmail = true;
                var message = new MailMessage();
                var smtp = new SmtpClient();
                msg = string.Empty;
                if (Helpers.AllowUserSendMail)
                {
                    if (!string.IsNullOrEmpty(EmailModel.User?.Email))
                    {
                        string errorFromAddress = string.Empty;
                        if (EmailModel.User.Email.IsValid(out errorFromAddress))
                        {
                            message.From = new MailAddress(EmailModel.User.Email,
                                string.IsNullOrEmpty(EmailModel.User.EmailNameDisplay)
                                    ? EmailModel.User.FullName
                                    : EmailModel.User.EmailNameDisplay);
                            if (isUserSend && !string.IsNullOrEmpty(EmailModel.User.EmailPassword))
                            {
                                string pss = EmailModel.User.EmailPassword.Decrypt();
                                smtp.Credentials = new NetworkCredential(EmailModel.User.Email, pss);
                            }
                        }
                        else
                        {
                            //smtp.UseDefaultCredentials = true;
                            Logger.LogError("User email {0} Not valid {1}", EmailModel.User.Email, errorFromAddress);
                        }
                    }
                }
                 
                if (!string.IsNullOrEmpty(EmailModel.EmailTo))
                {
                    var to = EmailModel.EmailTo.Split(',').Where(x => !string.IsNullOrEmpty(x) && x.IsValid()).Distinct().Select(x => new MailAddress(x));
                    message.To.AddRange(to);
                }

                if (!string.IsNullOrEmpty(EmailModel.EmailCc))
                {
                    var cc = EmailModel.EmailCc.Split(',').Where(x => !string.IsNullOrEmpty(x) && x.IsValid()).Distinct().Select(x => new MailAddress(x));
                    message.CC.AddRange(cc);
                }
                message.HeadersEncoding = System.Text.Encoding.UTF8;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                var body = EmailModel.Message;
                message.Subject = EmailModel.Subject;
                message.IsBodyHtml = true;
                message.Body = body;
                message.Sender = message.From;

                if (EmailModel.Uploads != null && EmailModel.Uploads.Count > 0)
                {
                    foreach (var upload in EmailModel.Uploads.Where(upload => upload != null && upload.ContentLength > 0))
                    {
                        Attachment attachment = new Attachment(upload.InputStream, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attachment.Name);
                        disposition.ModificationDate = File.GetLastWriteTime(upload.FileName);
                        disposition.ReadDate = File.GetLastAccessTime(upload.FileName);
                        disposition.FileName = Path.GetFileName(upload.FileName);
                        disposition.Size = upload.ContentLength;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        message.Attachments.Add(attachment);  
                    }
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendCompleted += MailDeliveryComplete; 
                Task tasks = Task.Factory.StartNew(() => smtp.SendAsync(message, null));
                tasks.Wait(); 
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                msg = ex.Message;
                return false;
            }
        }
        private static void MailDeliveryComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.LogError(e.Error);
                throw e.Error;
            }
            else if (e.Cancelled)
            {
                Logger.LogError("Cancelled");
            }
            else
            {
                //handle sent email
                MailMessage message = (MailMessage)e.UserState;
            }
        }
        public static Task SendAsync(SmtpClient client, MailMessage message)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Guid sendGuid = Guid.NewGuid();

            SendCompletedEventHandler handler = null;
            handler = (o, ea) =>
            {
                if (ea.UserState is Guid && ((Guid)ea.UserState) == sendGuid)
                {
                    client.SendCompleted -= handler;
                    if (ea.Cancelled)
                    {
                        tcs.SetCanceled();
                    }
                    else if (ea.Error != null)
                    {
                        tcs.SetException(ea.Error);
                    }
                    else
                    {
                        tcs.SetResult(null);
                    }
                }
            };

            client.SendCompleted += handler;
            client.SendAsync(message, sendGuid);
            return tcs.Task;
        }
    }

}