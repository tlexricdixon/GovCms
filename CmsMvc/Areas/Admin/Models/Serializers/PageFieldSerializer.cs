/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license. See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using CmsMvc.Areas.Admin.Models.Fields;

namespace CmsMvc.Areas.Admin.Models.Serializers;

/// <summary>
/// Serializer for page fields.
/// </summary>
public class PageFieldSerializer : ISerializer
{
    /// <inheritdoc />
    public string Serialize(object obj)
    {
        if (obj is PageField field)
        {
            return field.Id.ToString();
        }
        throw new ArgumentException("The given object doesn't match the serialization type");
    }

    /// <inheritdoc />
    public object Deserialize(string str)
    {
        return new PageField
        {
            Id = !string.IsNullOrEmpty(str) ? new Guid(str) : (Guid?)null
        };
    }
}
