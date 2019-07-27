using SpeechRecognitionThesis.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class RepositoryWrapper : IRespositoryWrapper
    {
        private RepositoryContext   _repositoryContext;
        private IAccountRepository  _accountRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext )
        {
            _repositoryContext = repositoryContext;
        }

        public IAccountRepository Account
        {
            get
            {
                if( _accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repositoryContext);
                }

                return _accountRepository;
            }
        }


        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}