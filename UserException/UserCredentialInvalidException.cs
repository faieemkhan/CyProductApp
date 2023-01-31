using System;
namespace MVCCoreApplication.UserException
{
    public class UserCredentialInvalidException :ApplicationException
    {
        public UserCredentialInvalidException()
        {

        }
        public UserCredentialInvalidException(string message) : base(message)
        {

        }
       
    }
}
