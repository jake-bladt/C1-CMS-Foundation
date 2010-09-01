﻿using System;
using System.Globalization;
using Composite.Core.Implementation;
using Composite.Core.Implementation.Pages;

namespace Composite.Data
{
#warning MRJ: Delete this file
    /*
    /// <summary>
    /// Provides access to pages, page structure and placeholders content 
    /// </summary>
    /// <example>
    /// <code>
    ///  public static string GetPageTitle(Guid pageId, CultureInfo locale)
    ///  {
    ///      var pageManager = PageManager.Create(locale);
    ///      var page = pageManager.GetPageById(pageId);
    /// 
    ///      return page != null ? page.Title : string.Empty;
    ///  }
    /// 
    ///  public static string GetPagePlaceholderContent(Guid pageId, string placeholderName)
    ///  {
    ///      var pageManager = PageManager.Create();
    /// 
    ///      var placeholder = pageManager.GetPlaceholdersContent(pageId).FirstOrDefault(p => p.PlaceHolderId == placeholderName);
    /// 
    ///      return placeholder != null ? placeholder.Content : string.Empty;
    ///  }
    /// </code>
    /// </example>
    public static class PageManager
    {
        static PageManager()
        {
            ImplementationContainer.SetImplementation<PageManagerBase>(new PageManagerDefaultImplementation());
        }

        public static IPageManager Create()
        {
            return ImplementationContainer.GetImplementation<PageManagerBase>().Create();
        }

        public static IPageManager Create(PublicationScope publicationScope)
        {
            return ImplementationContainer.GetImplementation<PageManagerBase>().Create(publicationScope);
        }

        public static IPageManager Create(PublicationScope publicationScope, CultureInfo locale)
        {
            return ImplementationContainer.GetImplementation<PageManagerBase>().Create(publicationScope, locale);
        }

        public static IPageManager Create(CultureInfo locale)
        {
            return ImplementationContainer.GetImplementation<PageManagerBase>().Create(locale);
        }
    }*/
}
