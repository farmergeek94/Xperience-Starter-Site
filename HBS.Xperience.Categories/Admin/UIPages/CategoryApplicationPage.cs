


using HBS.Xperience.Categories.Admin.UIPages;

[assembly: UIApplication(
    identifier: CategoryApplicationPage.IDENTIFIER,
    type: typeof(CategoryApplicationPage),
    slug: "categories",
    name: "Categories",
    category: BaseApplicationCategories.CONFIGURATION,
    icon: Icons.OrganisationalScheme,
    templateName: TemplateNames.SECTION_LAYOUT)]



namespace HBS.Xperience.Categories.Admin.UIPages
{
    public class CategoryApplicationPage : ApplicationPage
    {
        public const string IDENTIFIER = "HBS.Xperience.CategoryApplication";
    }
}
