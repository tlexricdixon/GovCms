using Manager.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Models;

[Serializable]
public class SiteContent<T> : SiteContentBase where T : SiteContent<T>
{
    /// <summary>
    /// Creates a new site content model using the given site type id.
    /// </summary>
    /// <param name="api">The current api</param>
    /// <param name="typeId">The unique site type id</param>
    /// <returns>The new model</returns>
    public static Task<T> CreateAsync(IApi api, string typeId = null)
    {
        return api.Sites.CreateContentAsync<T>(typeId);
    }
}
