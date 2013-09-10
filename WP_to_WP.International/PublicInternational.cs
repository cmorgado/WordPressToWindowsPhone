
namespace WP_to_WP.International
{
    public class PublicInternational
    {
        public International.Translations Translation { get; set; }

        public PublicInternational()
        {
            Translation = new Translations();
        }
    }
}
