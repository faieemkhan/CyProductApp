using System;
namespace MVCCoreApplication.UserException
{

    public class UserNotExistsException : ApplicationException
    {
        public UserNotExistsException()
        
        {
        
        }
        public UserNotExistsException(string message) : base(message)
        {

        }
    }
}
