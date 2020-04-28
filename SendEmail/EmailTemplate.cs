﻿using SendGrid.Helpers.Mail;
using StarApi.SendEmail.Templates;
using System.IO;

namespace StarApi.SendEmail
{
    public abstract class EmailTemplate
    {
        public abstract string TemplateName { get; }
        public abstract string ToName { get; }
        public abstract string ToEmail{ get; }
        public abstract string Subject { get; }
        public abstract string Text { get; }
        public abstract string Html { get; }
        public virtual string BccEmail => "";
        public virtual string BccName => "";

        public EmailAddress To => new EmailAddress(ToEmail, ToName);
        public EmailAddress From => new EmailAddress("support@equal.vote", "STAR Vote Elections");

        public static EmailTemplate Factory(string templateName, dynamic fields)
        {
            EmailTemplate template;
            switch (templateName.ToLower())
            {
                case "ballotlink":
                    template = new BallotLink(fields);
                    break;
                case "votereceipt":
                    template = new VoteReceipt(fields);
                    break;
                case "ballotflagged":
                    template = new BallotFlagged(fields);
                    break;
                default:
                    throw new InvalidDataException($"Unknown template name: {templateName}");
            }
            return template;
        }
    }
}
