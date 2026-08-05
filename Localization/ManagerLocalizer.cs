using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Localization
{
    public class ManagerLocalizer
    {
        /// <summary>
        /// Gets/sets alias string resources.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Alias> Alias { get; private set; }

        /// <summary>
        /// Gets/sets comment string resources.
        /// </summary>
        /// <value></value>
        public IStringLocalizer<Localization.Resources.Comment> Comment { get; private set; }

        /// <summary>
        /// Gets/sets content string resources.
        /// </summary>
        /// <value></value>
        public IStringLocalizer<Localization.Resources.Content> Content { get; private set; }

        /// <summary>
        /// Gets/sets config string resources.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Config> Config { get; private set; }

        /// <summary>
        /// Gets/sets general string resources.
        /// </summary>
        public IStringLocalizer<Localization.Resources.General> General { get; private set; }

        /// <summary>
        /// Gets/sets security string resources.
        /// </summary>
        /// <value></value>
        public IStringLocalizer<Localization.Resources.Security> Security { get; private set; }

        /// <summary>
        /// Gets/sets language string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Language> Language { get; private set; }

        /// <summary>
        /// Gets/sets media string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Media> Media { get; private set; }

        /// <summary>
        /// Gets/sets menu string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Menu> Menu { get; private set; }

        /// <summary>
        /// Gets/sets module string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Module> Module { get; private set; }

        /// <summary>
        /// Gets/sets page string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Page> Page { get; private set; }

        /// <summary>
        /// Gets/sets post string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Post> Post { get; private set; }

        /// <summary>
        /// Gets/sets site string localization.
        /// </summary>
        public IStringLocalizer<Localization.Resources.Site> Site { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagerLocalizer(
            IStringLocalizer<Localization.Resources.Alias> alias,
            IStringLocalizer<Localization.Resources.Comment> comment,
            IStringLocalizer<Localization.Resources.Content> content,
            IStringLocalizer<Localization.Resources.Config> config,
            IStringLocalizer<Localization.Resources.General> general,
            IStringLocalizer<Localization.Resources.Security> security,
            IStringLocalizer<Localization.Resources.Language> language,
            IStringLocalizer<Localization.Resources.Media> media,
            IStringLocalizer<Localization.Resources.Menu> menu,
            IStringLocalizer<Localization.Resources.Module> module,
            IStringLocalizer<Localization.Resources.Page> page,
            IStringLocalizer<Localization.Resources.Post> post,
            IStringLocalizer<Localization.Resources.Site> site)
        {
            Alias = alias;
            Comment = comment;
            Content = content;
            Config = config;
            General = general;
            Security = security;
            Language = language;
            Media = media;
            Menu = menu;
            Module = module;
            Page = page;
            Post = post;
            Site = site;

        }
    }
}
