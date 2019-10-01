using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        RepositoryContext _repositoryContext;

        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<User> Authenticate(string usernameString, string passwordString)
        {
            string encodedPassword = UserTools.ConvertInputTextToSha512( passwordString );
            User user = await Task.Run( () => _repositoryContext.Users.SingleOrDefault( x => x.NickName == usernameString && x.Password == encodedPassword) );

            return user;
        }

        public User GetAnonymousUser()
        {
            return FindAll().FirstOrDefault( user => user.NickName == UserTools.ANONYMOUS_USER_NICKNAME );
        }

        public User GetUser( long lUserId )
        {
            return FindAll().FirstOrDefault(user => user.Id == lUserId);
        }

        public bool UpdateLoggedUserData(User loggedUser)
        {
            if( loggedUser == null)
            {
                return false;
            }

            loggedUser.LastLoggedAccountDate = DateTime.Now.ToString();
            
            Update( loggedUser );

            return true;
        }

        public bool UpdateUserData( long lUserId, User user, bool bIsChangeAvatar )
        {
            User userFromDb = GetUser( lUserId );
            string userPassword = user.Password;
            string userNewPasswordString = ( userPassword != null ) ? 
                UserTools.ConvertInputTextToSha512( userPassword.Trim() ) : string.Empty;
            string userEmailString = ( user.Email != null ) ? user.Email.Trim() : string.Empty;
            bool bUpdateUserDb = false;

            if( userFromDb != null )
            {
                bUpdateUserDb = true;

                if( userNewPasswordString != null 
                    && userNewPasswordString.Length > 0
                    && userNewPasswordString != userFromDb.Password )
                {
                    userFromDb.Password = userNewPasswordString;
                    userFromDb.LastUpdateAccountDate = DateTime.Now.ToString();
                }

                if( userEmailString != null 
                    && userEmailString.Length > 0
                    && userEmailString != userFromDb.Email )
                {
                    userFromDb.Email = userEmailString;
                }

                if( bIsChangeAvatar
                    && user.AvatarId > -1 
                    && userFromDb.AvatarId != user.AvatarId )
                {
                    userFromDb.AvatarId = user.AvatarId;
                }

                Update( userFromDb );
            }

            return bUpdateUserDb;
        }
    }
}