namespace RPL.Core.Settings.SMS
{
    public interface ISmsSettings
    {
        string Url { get; }

        string AuthorizationKey { get; }

        string Sender { get; }

        void Initialize();
    }
}
