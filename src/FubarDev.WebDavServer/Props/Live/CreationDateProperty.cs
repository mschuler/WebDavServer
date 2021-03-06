﻿// <copyright file="CreationDateProperty.cs" company="Fubar Development Junker">
// Copyright (c) Fubar Development Junker. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FubarDev.WebDavServer.Model;
using FubarDev.WebDavServer.Props.Generic;

namespace FubarDev.WebDavServer.Props.Live
{
    /// <summary>
    /// The <c>creationdate</c> property
    /// </summary>
    public class CreationDateProperty : GenericDateTimeOffsetIso8601Property, ILiveProperty
    {
        /// <summary>
        /// The XML property name
        /// </summary>
        public static readonly XName PropertyName = WebDavXml.Dav + "creationdate";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreationDateProperty"/> class.
        /// </summary>
        /// <param name="propValue">The initial property value</param>
        /// <param name="setValueAsyncFunc">The delegate to set the value asynchronously</param>
        public CreationDateProperty(DateTimeOffset propValue, SetPropertyValueAsyncDelegate<DateTimeOffset> setValueAsyncFunc)
            : base(PropertyName, 0, ct => Task.FromResult(propValue), setValueAsyncFunc)
        {
        }

        /// <inheritdoc />
        public async Task<bool> IsValidAsync(CancellationToken cancellationToken)
        {
            return Converter.IsValidValue(await GetValueAsync(cancellationToken).ConfigureAwait(false));
        }
    }
}
