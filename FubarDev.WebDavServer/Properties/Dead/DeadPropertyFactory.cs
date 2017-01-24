﻿using System.Collections.Generic;
using System.Xml.Linq;

using FubarDev.WebDavServer.FileSystem;
using FubarDev.WebDavServer.Properties.Store;

using JetBrains.Annotations;

using Microsoft.Extensions.Options;

namespace FubarDev.WebDavServer.Properties.Dead
{
    public class DeadPropertyFactory : IDeadPropertyFactory
    {
        private delegate IDeadProperty CreateDeadPropertyDelegate(IPropertyStore store, IEntry entry, XName name);

        private readonly IReadOnlyDictionary<XName, CreateDeadPropertyDelegate> _defaultCreationMap;

        public DeadPropertyFactory(IOptions<DeadPropertyFactoryOptions> options = null)
            : this(options?.Value ?? new DeadPropertyFactoryOptions())
        {
        }

        public DeadPropertyFactory([NotNull] DeadPropertyFactoryOptions options)
        {
            _defaultCreationMap = new Dictionary<XName, CreateDeadPropertyDelegate>()
            {
                [EntityTag.PropertyName] = (store, entry, name) => new GetETagProperty(store, entry),
                [DisplayNameProperty.PropertyName] = (store, entry, name) => new DisplayNameProperty(entry, store, options.HideExtensionForDisplayName),
            };
        }

        public virtual IDeadProperty Create(IPropertyStore store, IEntry entry, XName name)
        {
            CreateDeadPropertyDelegate createFunc;
            if (_defaultCreationMap.TryGetValue(name, out createFunc))
                return createFunc(store, entry, name);
            return new DeadProperty(store, entry, name);
        }

        public IDeadProperty Create(IPropertyStore store, IEntry entry, XElement element)
        {
            var result = Create(store, entry, element.Name);
            result.Init(element);
            return result;
        }
    }
}
