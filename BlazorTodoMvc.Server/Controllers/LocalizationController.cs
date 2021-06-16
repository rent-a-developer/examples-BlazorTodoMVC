using BlazorTodoMvc.Shared.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BlazorTodoMvc.Server.Controllers
{
    /// <summary>
    /// Provides loclization related operations.
    /// </summary>
    [ApiController]
    public class LocalizationController : ControllerBase
    {
        /// <summary>
        /// Gets the cultures the application supports.
        /// </summary>
        /// <returns>A list of cultures the application supports.</returns>
        [HttpGet]
        [Route("api/Localization/Cultures")]
        public List<Culture> Cultures()
        {
            return Localization.Cultures;
        }

        /// <summary>
        /// Gets the translations for the given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to get the translations for.</param>
        /// <returns>A list of translations for the given culture.</returns>
        [HttpGet]
        [Route("api/Localization/Translations/{cultureName}")]
        public List<Translation> Translations(String cultureName)
        {
            return Localization.Translations.Where(a => a.Culture == cultureName).ToList();
        }

        private static Localization LoadLocalization()
        {
            var json = System.IO.File.ReadAllText(@"wwwroot\\data\localization.json");
            return JsonConvert.DeserializeObject<Localization>(json);
        }

        private static Localization Localization => localization.Value;
        private static readonly Lazy<Localization> localization = new Lazy<Localization>(LoadLocalization);
    }
}
