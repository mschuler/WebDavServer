﻿// <copyright file="ILockHandler.cs" company="Fubar Development Junker">
// Copyright (c) Fubar Development Junker. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

using FubarDev.WebDavServer.Model;
using FubarDev.WebDavServer.Model.Headers;

using JetBrains.Annotations;

namespace FubarDev.WebDavServer.Handlers
{
    public interface ILockHandler : IClass2Handler
    {
        [NotNull]
        [ItemNotNull]
        Task<IWebDavResult> LockAsync([NotNull] string path, [NotNull] lockinfo info, CancellationToken cancellationToken);

        [NotNull]
        [ItemNotNull]
        Task<IWebDavResult> RefreshLockAsync([NotNull] string path, [NotNull] IfHeader ifHeader, [CanBeNull] TimeoutHeader timeoutHeader, CancellationToken cancellationToken);
    }
}