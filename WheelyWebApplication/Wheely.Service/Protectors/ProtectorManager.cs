﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;

namespace Wheely.Service.Protectors
{
    public sealed class ProtectorManager : IProtectorService
    {
        #region Fields
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly string _purpose;
        #endregion

        #region Constructor
        public ProtectorManager(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _purpose = configuration.GetValue<string>("GlobalPurposeKey");
        }
        #endregion

        #region Methods
        public IDataResult<string> Encrypt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var protector = _dataProtectionProvider.CreateProtector(_purpose ?? GetType().FullName);
            ArgumentNullException.ThrowIfNull(protector);

            return new SuccessDataResult<string>(protector.Protect(input), null);
        }

        public IDataResult<string> Encrypt(string input, string purpose)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrWhiteSpace(purpose))
                throw new ArgumentNullException(nameof(purpose));

            var protector = _dataProtectionProvider.CreateProtector(purpose);
            ArgumentNullException.ThrowIfNull(protector);

            return new SuccessDataResult<string>(protector.Protect(input), null);
        }

        public IDataResult<string> Decrypt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var protector = _dataProtectionProvider.CreateProtector(_purpose ?? GetType().FullName);
            ArgumentNullException.ThrowIfNull(protector);

            return new SuccessDataResult<string>(protector.Unprotect(input), null);
        }

        public IDataResult<string> Decrypt(string input, string purpose)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrWhiteSpace(purpose))
                throw new ArgumentNullException(nameof(purpose));

            var protector = _dataProtectionProvider.CreateProtector(purpose);
            ArgumentNullException.ThrowIfNull(protector);

            return new SuccessDataResult<string>(protector.Unprotect(input), null);
        }
        #endregion
    }
}