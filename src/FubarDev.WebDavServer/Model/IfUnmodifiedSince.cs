﻿// <copyright file="IfUnmodifiedSince.cs" company="Fubar Development Junker">
// Copyright (c) Fubar Development Junker. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using FubarDev.WebDavServer.FileSystem;
using FubarDev.WebDavServer.Props.Converters;

using JetBrains.Annotations;

namespace FubarDev.WebDavServer.Model
{
    public class IfUnmodifiedSince : IIfMatcher
    {
        public IfUnmodifiedSince(DateTime lastWriteTimeUtc)
        {
            LastWriteTimeUtc = lastWriteTimeUtc;
        }

        public DateTime LastWriteTimeUtc { get; }

        public static IfUnmodifiedSince Parse(string s)
        {
            return new IfUnmodifiedSince(DateTimeRfc1123Converter.Parse(s));
        }

        public bool IsMatch(IEntry entry, EntityTag etag, [ItemNotNull, NotNull] IReadOnlyCollection<Uri> stateTokens)
        {
            return entry.LastWriteTimeUtc <= LastWriteTimeUtc;
        }
    }
}