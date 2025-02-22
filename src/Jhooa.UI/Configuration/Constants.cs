namespace Jhooa.UI.Configuration;

public abstract class Constants
{
    public abstract class Cookie
    {
        public const string Culture = "Culture";
        public const string User = "User";
        public const string Xsrf = "X-CSRF-TOKEN";
        public const string XsrfHeader = "X-XSRF-TOKEN";
    }
    
    public abstract class DreamIds
    {
        public const string BigDreamId = "big-dream-id";
        public const string SmallDreamId = "small-dream-id";
        public const string MediumDreamId = "medium-dream-id";

    }
    
    public abstract class Claims
    {
        public const string IsSubActive = "IsSubActive";
        public const string SubStartDate = "SubStartDate";
        public const string SubEndDate = "SubEndDate";
    }
}