namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITemplateBuilder
    {

        /// <summary>
        /// Generates the specified template text.
        /// </summary>
        /// <param name="templateText">The template text.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string Generate(string templateText, string templateName, object model);
    }
}
