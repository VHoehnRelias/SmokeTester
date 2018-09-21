namespace Cmt.Online.Web.TestUi.Selenium.Enums
{
    public enum AgeType
    {
        All,
        Adult,
        Child,
        Elderly
    }

    public struct Age
    {
        public string Name { get; set; }

        public AgeType Agetype { get; set; }

        public int StartAge { get; set; }

        public int EndAge { get; set; }
    }
}